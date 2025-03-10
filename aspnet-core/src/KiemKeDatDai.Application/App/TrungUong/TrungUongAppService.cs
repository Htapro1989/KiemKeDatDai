using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using KiemKeDatDai;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Abp.Threading;
using KiemKeDatDai.Sessions;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using KiemKeDatDai.RisApplication;
using static KiemKeDatDai.CommonEnum;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Transactions;

namespace KiemKeDatDai.App.DMBieuMau
{
    public class TrungUongAppService : KiemKeDatDaiAppServiceBase, ITrungUongAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Tinh, long> _bieu01TKKK_TinhRepos;
        private readonly IRepository<Bieu01TKKK_Vung, long> _bieu01TKKK_VungRepos;
        private readonly IRepository<Bieu01TKKK, long> _bieu01TKKKRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public TrungUongAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Tinh, long> bieu01TKKK_TinhRepos,
            IRepository<Bieu01TKKK_Vung, long> bieu01TKKK_VungRepos,
            IRepository<Bieu01TKKK, long> bieu01TKKKRepos,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _dvhcRepos = dvhcRepos;
            _dmKyThongKeKiemKeRepos = dmKyThongKeKiemKeRepos;
            _bieu01TKKK_TinhRepos = bieu01TKKK_TinhRepos;
            _bieu01TKKK_VungRepos = bieu01TKKK_VungRepos;
            _bieu01TKKKRepos = bieu01TKKKRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize]
        public async Task<CommonResponseDto> DuyetBaoCaoTinh(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(currentUser.DonViHanhChinhId.Value);
                    if (objdata != null)
                    {
                        var tinh = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                        var vung = await _dvhcRepos.FirstOrDefaultAsync(x=>x.MaVung == tinh.MaVung && x.Year == year && x.CapDVHCId == (int)CAP_DVHC.Vung);
                        if (vung == null)
                        {
                            commonResponseDto.Message = "Tỉnh này không nằm trong vùng nào";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        }
                        if (tinh != null)
                        {
                            //gọi hàm update biểu tỉnh
                            commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, vung.Id, vung.MaVung, year, (int)HAM_DUYET.DUYET);

                            #region cập nhật DVHC tỉnh sau khi duyệt tỉnh
                            tinh.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                            tinh.NgayDuyet = DateTime.Now;
                            await _dvhcRepos.UpdateAsync(tinh);
                            #endregion
                        }
                        else
                        {
                            commonResponseDto.Message = "Tỉnh này không tồn tại";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        }

                        #region cập nhật DVHC TW sau khi duyệt Tỉnh
                        if (objdata.SoDVHCDaDuyet == null)
                        {
                            objdata.SoDVHCDaDuyet = 1;
                        }
                        else
                        {
                            objdata.SoDVHCDaDuyet++;
                        }
                        if (objdata.SoDVHCCon == null)
                        {
                            objdata.SoDVHCCon = await _dvhcRepos.CountAsync(x => x.Parent_id == currentUser.DonViHanhChinhId.Value);
                        }
                        await _dvhcRepos.UpdateAsync(objdata);
                        #endregion
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    }
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> HuyDuyetBaoCaoTinh(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(currentUser.DonViHanhChinhId.Value);
                    if (objdata != null)
                    {
                        var tinh = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                        var vung = await _dvhcRepos.FirstOrDefaultAsync(x => x.MaVung == tinh.MaVung && x.Year == year && x.CapDVHCId == (int)CAP_DVHC.Vung);
                        if (tinh != null)
                        {
                            //gọi hàm update biểu tỉnh
                            commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, vung.Id, vung.MaVung, year, (int)HAM_DUYET.HUY);

                            #region cập nhật DVHC tỉnh sau khi duyệt tinh
                            tinh.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                            tinh.NgayDuyet = DateTime.Now;
                            await _dvhcRepos.UpdateAsync(tinh);
                            #endregion
                        }
                        else
                        {
                            commonResponseDto.Message = "Tỉnh này không tồn tại";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        }

                        #region cập nhật DVHC TW sau khi duyệt tỉnh
                        objdata.SoDVHCDaDuyet--;
                        await _dvhcRepos.UpdateAsync(objdata);
                        #endregion
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    }
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }

        private async Task<CommonResponseDto> CreateOrUpdateBieuTinh(DonViHanhChinh tinh, string ma, long vungId, string maVung, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            var data_tinh = await _bieu01TKKK_TinhRepos.GetAllListAsync(x => x.Ma == ma && x.Year == year);
            if (data_tinh != null)
            {
                await CreateOrUpdateBieu01TKKK_Tinh(data_tinh, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 01TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
            }

            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        #region Biểu 01TKKK
        private async Task CreateOrUpdateBieu01TKKK_Tinh(List<Bieu01TKKK_Tinh> tinh, long vungId, string maVung, long year, int hamduyet)
        {
            var data_TW = await _bieu01TKKKRepos.GetAllListAsync(x => x.Year == year);
            if (data_TW.Count == 0)
            {
                foreach (var item in tinh)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi tỉnh
                    await CreateBieu01TKKK_Tinh(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi tỉnh
                    await UpdateBieu01TKKK_Tinh(item, vungId, maVung, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu01TKKK_Tinh(Bieu01TKKK_Tinh tinh, long vungId, string maVung)
        {
            try
            {
                //Thêm mới bản ghi trung ương
                var objTW = new Bieu01TKKK()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongDienTichDVHC = tinh.TongDienTichDVHC,
                    TongSoTheoDoiTuongSuDung = tinh.TongSoTheoDoiTuongSuDung,
                    CaNhanTrongNuoc_CNV = tinh.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = tinh.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = tinh.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = tinh.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = tinh.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = tinh.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = tinh.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = tinh.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = tinh.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = tinh.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = tinh.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = tinh.ToChucKinhTeVonNuocNgoai_TVN,
                    TongSoTheoDoiTuongDuocGiaoQuanLy = tinh.TongSoTheoDoiTuongDuocGiaoQuanLy,
                    CoQuanNhaNuoc_TCQ = tinh.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = tinh.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = tinh.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = tinh.CongDongDanCu_CDQ,
                    Year = tinh.Year,
                    Active = true,
                };
                await _bieu01TKKKRepos.InsertAsync(objTW);
                //Thêm mới bản ghi vùng
                var objVung = new Bieu01TKKK_Vung()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongDienTichDVHC = tinh.TongDienTichDVHC,
                    TongSoTheoDoiTuongSuDung = tinh.TongSoTheoDoiTuongSuDung,
                    CaNhanTrongNuoc_CNV = tinh.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = tinh.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = tinh.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = tinh.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = tinh.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = tinh.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = tinh.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = tinh.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = tinh.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = tinh.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = tinh.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = tinh.ToChucKinhTeVonNuocNgoai_TVN,
                    TongSoTheoDoiTuongDuocGiaoQuanLy = tinh.TongSoTheoDoiTuongDuocGiaoQuanLy,
                    CoQuanNhaNuoc_TCQ = tinh.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = tinh.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = tinh.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = tinh.CongDongDanCu_CDQ,
                    Year = tinh.Year,
                    VungId = vungId,
                    MaVung = maVung,
                    Active = true,
                };
                await _bieu01TKKKRepos.InsertAsync(objTW);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu01TKKK_Tinh(Bieu01TKKK_Tinh tinh, long vungId, string maVung, long year, int hamduyet)
        {
            try
            {
                var objTW = await _bieu01TKKKRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year);
                var objVung = await _bieu01TKKK_VungRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year && x.MaVung == maVung);
                if (objTW.Id > 0)
                {
                    //update duyệt tỉnh
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objTW.TongDienTichDVHC += tinh.TongDienTichDVHC;
                        objTW.TongSoTheoDoiTuongSuDung += tinh.TongSoTheoDoiTuongSuDung;
                        objTW.CaNhanTrongNuoc_CNV += tinh.CaNhanTrongNuoc_CNV;
                        objTW.NguoiVietNamONuocNgoai_CNN += tinh.NguoiVietNamONuocNgoai_CNN;
                        objTW.CoQuanNhaNuoc_TCN += tinh.CoQuanNhaNuoc_TCN;
                        objTW.DonViSuNghiep_TSN += tinh.DonViSuNghiep_TSN;
                        objTW.ToChucXaHoi_TXH += tinh.ToChucXaHoi_TXH;
                        objTW.ToChucKinhTe_TKT += tinh.ToChucKinhTe_TKT;
                        objTW.ToChucKhac_TKH += tinh.ToChucKhac_TKH;
                        objTW.ToChucTonGiao_TTG += tinh.ToChucTonGiao_TTG;
                        objTW.CongDongDanCu_CDS += tinh.CongDongDanCu_CDS;
                        objTW.ToChucNuocNgoai_TNG += tinh.ToChucNuocNgoai_TNG;
                        objTW.NguoiGocVietNamONuocNgoai_NGV += tinh.NguoiGocVietNamONuocNgoai_NGV;
                        objTW.ToChucKinhTeVonNuocNgoai_TVN += tinh.ToChucKinhTeVonNuocNgoai_TVN;
                        objTW.TongSoTheoDoiTuongDuocGiaoQuanLy += tinh.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objTW.CoQuanNhaNuoc_TCQ += tinh.CoQuanNhaNuoc_TCQ;
                        objTW.DonViSuNghiep_TSQ += tinh.DonViSuNghiep_TSQ;
                        objTW.ToChucKinhTe_KTQ += tinh.ToChucKinhTe_KTQ;
                        objTW.CongDongDanCu_CDQ += tinh.CongDongDanCu_CDQ;

                        objVung.TongDienTichDVHC += tinh.TongDienTichDVHC;
                        objVung.TongSoTheoDoiTuongSuDung += tinh.TongSoTheoDoiTuongSuDung;
                        objVung.CaNhanTrongNuoc_CNV += tinh.CaNhanTrongNuoc_CNV;
                        objVung.NguoiVietNamONuocNgoai_CNN += tinh.NguoiVietNamONuocNgoai_CNN;
                        objVung.CoQuanNhaNuoc_TCN += tinh.CoQuanNhaNuoc_TCN;
                        objVung.DonViSuNghiep_TSN += tinh.DonViSuNghiep_TSN;
                        objVung.ToChucXaHoi_TXH += tinh.ToChucXaHoi_TXH;
                        objVung.ToChucKinhTe_TKT += tinh.ToChucKinhTe_TKT;
                        objVung.ToChucKhac_TKH += tinh.ToChucKhac_TKH;
                        objVung.ToChucTonGiao_TTG += tinh.ToChucTonGiao_TTG;
                        objVung.CongDongDanCu_CDS += tinh.CongDongDanCu_CDS;
                        objVung.ToChucNuocNgoai_TNG += tinh.ToChucNuocNgoai_TNG;
                        objVung.NguoiGocVietNamONuocNgoai_NGV += tinh.NguoiGocVietNamONuocNgoai_NGV;
                        objVung.ToChucKinhTeVonNuocNgoai_TVN += tinh.ToChucKinhTeVonNuocNgoai_TVN;
                        objVung.TongSoTheoDoiTuongDuocGiaoQuanLy += tinh.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objVung.CoQuanNhaNuoc_TCQ += tinh.CoQuanNhaNuoc_TCQ;
                        objVung.DonViSuNghiep_TSQ += tinh.DonViSuNghiep_TSQ;
                        objVung.ToChucKinhTe_KTQ += tinh.ToChucKinhTe_KTQ;
                        objVung.CongDongDanCu_CDQ += tinh.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt tỉnh
                    else
                    {
                        objTW.TongDienTichDVHC -= tinh.TongDienTichDVHC;
                        objTW.TongSoTheoDoiTuongSuDung -= tinh.TongSoTheoDoiTuongSuDung;
                        objTW.CaNhanTrongNuoc_CNV -= tinh.CaNhanTrongNuoc_CNV;
                        objTW.NguoiVietNamONuocNgoai_CNN -= tinh.NguoiVietNamONuocNgoai_CNN;
                        objTW.CoQuanNhaNuoc_TCN -= tinh.CoQuanNhaNuoc_TCN;
                        objTW.DonViSuNghiep_TSN -= tinh.DonViSuNghiep_TSN;
                        objTW.ToChucXaHoi_TXH -= tinh.ToChucXaHoi_TXH;
                        objTW.ToChucKinhTe_TKT -= tinh.ToChucKinhTe_TKT;
                        objTW.ToChucKhac_TKH -= tinh.ToChucKhac_TKH;
                        objTW.ToChucTonGiao_TTG -= tinh.ToChucTonGiao_TTG;
                        objTW.CongDongDanCu_CDS -= tinh.CongDongDanCu_CDS;
                        objTW.ToChucNuocNgoai_TNG -= tinh.ToChucNuocNgoai_TNG;
                        objTW.NguoiGocVietNamONuocNgoai_NGV -= tinh.NguoiGocVietNamONuocNgoai_NGV;
                        objTW.ToChucKinhTeVonNuocNgoai_TVN -= tinh.ToChucKinhTeVonNuocNgoai_TVN;
                        objTW.TongSoTheoDoiTuongDuocGiaoQuanLy -= tinh.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objTW.CoQuanNhaNuoc_TCQ -= tinh.CoQuanNhaNuoc_TCQ;
                        objTW.DonViSuNghiep_TSQ -= tinh.DonViSuNghiep_TSQ;
                        objTW.ToChucKinhTe_KTQ -= tinh.ToChucKinhTe_KTQ;
                        objTW.CongDongDanCu_CDQ -= tinh.CongDongDanCu_CDQ;

                        objVung.TongDienTichDVHC -= tinh.TongDienTichDVHC;
                        objVung.TongSoTheoDoiTuongSuDung -= tinh.TongSoTheoDoiTuongSuDung;
                        objVung.CaNhanTrongNuoc_CNV -= tinh.CaNhanTrongNuoc_CNV;
                        objVung.NguoiVietNamONuocNgoai_CNN -= tinh.NguoiVietNamONuocNgoai_CNN;
                        objVung.CoQuanNhaNuoc_TCN -= tinh.CoQuanNhaNuoc_TCN;
                        objVung.DonViSuNghiep_TSN -= tinh.DonViSuNghiep_TSN;
                        objVung.ToChucXaHoi_TXH -= tinh.ToChucXaHoi_TXH;
                        objVung.ToChucKinhTe_TKT -= tinh.ToChucKinhTe_TKT;
                        objVung.ToChucKhac_TKH -= tinh.ToChucKhac_TKH;
                        objVung.ToChucTonGiao_TTG -= tinh.ToChucTonGiao_TTG;
                        objVung.CongDongDanCu_CDS -= tinh.CongDongDanCu_CDS;
                        objVung.ToChucNuocNgoai_TNG -= tinh.ToChucNuocNgoai_TNG;
                        objVung.NguoiGocVietNamONuocNgoai_NGV -= tinh.NguoiGocVietNamONuocNgoai_NGV;
                        objVung.ToChucKinhTeVonNuocNgoai_TVN -= tinh.ToChucKinhTeVonNuocNgoai_TVN;
                        objVung.TongSoTheoDoiTuongDuocGiaoQuanLy -= tinh.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objVung.CoQuanNhaNuoc_TCQ -= tinh.CoQuanNhaNuoc_TCQ;
                        objVung.DonViSuNghiep_TSQ -= tinh.DonViSuNghiep_TSQ;
                        objVung.ToChucKinhTe_KTQ -= tinh.ToChucKinhTe_KTQ;
                        objVung.CongDongDanCu_CDQ -= tinh.CongDongDanCu_CDQ;
                    }
                    await _bieu01TKKKRepos.UpdateAsync(objTW);
                    await _bieu01TKKK_VungRepos.UpdateAsync(objVung);
                }
                else
                {
                    await CreateBieu01TKKK_Tinh(tinh, vungId, maVung);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion
    }
}
