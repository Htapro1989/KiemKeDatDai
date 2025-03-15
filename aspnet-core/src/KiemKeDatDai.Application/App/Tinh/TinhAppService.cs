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
using KiemKeDatDai.App.Huyen.Dto;
using NuGet.Protocol;

namespace KiemKeDatDai.App.DMBieuMau
{
    public class TinhAppService : KiemKeDatDaiAppServiceBase, ITinhAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Tinh, long> _bieu01TKKK_TinhRepos;

        private readonly IRepository<Bieu02TKKK_Huyen, long> _bieu02TKKK_HuyenRepos;
        private readonly IRepository<Bieu02TKKK_Tinh, long> _bieu02TKKK_TinhRepos;

        private readonly IRepository<Bieu03TKKK_Huyen, long> _bieu03TKKK_HuyenRepos;
        private readonly IRepository<Bieu03TKKK_Tinh, long> _bieu03TKKK_TinhRepos;

        private readonly IRepository<Bieu04TKKK_Huyen, long> _bieu04TKKK_HuyenRepos;
        private readonly IRepository<Bieu04TKKK_Tinh, long> _bieu04TKKK_TinhRepos;

        private readonly IRepository<Bieu05TKKK_Huyen, long> _bieu05TKKK_HuyenRepos;
        private readonly IRepository<Bieu05TKKK_Tinh, long> _bieu05TKKK_TinhRepos;

        private readonly IRepository<Bieu01KKSL_Huyen, long> _bieu01KKSL_HuyenRepos;
        private readonly IRepository<Bieu01KKSL_Tinh, long> _bieu01KKSL_TinhRepos;

        private readonly IRepository<Bieu02KKSL_Huyen, long> _bieu02KKSL_HuyenRepos;
        private readonly IRepository<Bieu02KKSL_Tinh, long> _bieu02KKSL_TinhRepos;

        private readonly IRepository<Bieu01aKKNLT_Huyen, long> _bieu01aKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01aKKNLT_Tinh, long> _bieu01aKKNLT_TinhRepos;

        private readonly IRepository<Bieu01bKKNLT_Huyen, long> _bieu01bKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01bKKNLT_Tinh, long> _bieu01bKKNLT_TinhRepos;

        private readonly IRepository<Bieu01cKKNLT_Huyen, long> _bieu01cKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01cKKNLT_Tinh, long> _bieu01cKKNLT_TinhRepos;

        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public TinhAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Tinh, long> bieu01TKKK_TinhRepos,

            IRepository<Bieu02TKKK_Tinh, long> bieu02TKKK_TinhRepos,
            IRepository<Bieu02TKKK_Huyen, long> bieu02TKKK_HuyenRepos,

            IRepository<Bieu03TKKK_Tinh, long> bieu03TKKK_TinhRepos,
            IRepository<Bieu03TKKK_Huyen, long> bieu03TKKK_HuyenRepos,

            IRepository<Bieu04TKKK_Tinh, long> bieu04TKKK_TinhRepos,
            IRepository<Bieu04TKKK_Huyen, long> bieu04TKKK_HuyenRepos,

            IRepository<Bieu05TKKK_Tinh, long> bieu05TKKK_TinhRepos,
            IRepository<Bieu05TKKK_Huyen, long> bieu05TKKK_HuyenRepos,

            IRepository<Bieu01KKSL_Tinh, long> bieu01KKSL_TinhRepos,
            IRepository<Bieu01KKSL_Huyen, long> bieu01KKSL_HuyenRepos,

            IRepository<Bieu02KKSL_Tinh, long> bieu02KKSL_TinhRepos,
            IRepository<Bieu02KKSL_Huyen, long> bieu02KKSL_HuyenRepos,

            IRepository<Bieu01aKKNLT_Tinh, long> bieu01aKKNLT_TinhRepos,
            IRepository<Bieu01aKKNLT_Huyen, long> bieu01aKKNLT_HuyenRepos,

            IRepository<Bieu01cKKNLT_Tinh, long> bieu01cKKNLT_TinhRepos,
            IRepository<Bieu01cKKNLT_Huyen, long> bieu01cKKNLT_HuyenRepos,

            IRepository<Bieu01bKKNLT_Tinh, long> bieu01bKKNLT_TinhRepos,
            IRepository<Bieu01bKKNLT_Huyen, long> bieu01bKKNLT_HuyenRepos,

            IRepository<BieuPhuLucIII, long> bieuPhuLucIIIRepos,
            IRepository<BieuPhuLucIV, long> bieuPhuLucIVRepos,
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
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_TinhRepos = bieu01TKKK_TinhRepos;

            _bieu02TKKK_TinhRepos = bieu02TKKK_TinhRepos;
            _bieu02TKKK_HuyenRepos = bieu02TKKK_HuyenRepos;

            _bieu03TKKK_TinhRepos = bieu03TKKK_TinhRepos;
            _bieu03TKKK_HuyenRepos = bieu03TKKK_HuyenRepos;

            _bieu04TKKK_TinhRepos = bieu04TKKK_TinhRepos;
            _bieu04TKKK_HuyenRepos = bieu04TKKK_HuyenRepos;

            _bieu05TKKK_TinhRepos = bieu05TKKK_TinhRepos;
            _bieu05TKKK_HuyenRepos = bieu05TKKK_HuyenRepos;

            _bieu01KKSL_TinhRepos = bieu01KKSL_TinhRepos;
            _bieu01KKSL_HuyenRepos = bieu01KKSL_HuyenRepos;

            _bieu02KKSL_TinhRepos = bieu02KKSL_TinhRepos;
            _bieu02KKSL_HuyenRepos = bieu02KKSL_HuyenRepos;

            _bieu01aKKNLT_TinhRepos = bieu01aKKNLT_TinhRepos;
            _bieu01aKKNLT_HuyenRepos = bieu01aKKNLT_HuyenRepos;

            _bieu01cKKNLT_TinhRepos = bieu01cKKNLT_TinhRepos;
            _bieu01cKKNLT_HuyenRepos = bieu01cKKNLT_HuyenRepos;

            _bieu01bKKNLT_TinhRepos = bieu01bKKNLT_TinhRepos;
            _bieu01bKKNLT_HuyenRepos = bieu01bKKNLT_HuyenRepos;

            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize]
        public async Task<CommonResponseDto> DuyetBaoCaoHuyen(string ma, long year)
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
                        if (objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                        {
                            commonResponseDto.Message = "Tỉnh đã được duyệt, không thể duyệt huyện";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var huyen = await _dvhcRepos.FirstOrDefaultAsync(x=>x.Ma == ma);
                            if (huyen != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, year, (int)HAM_DUYET.DUYET);

                                #region cập nhật DVHC huyện sau khi duyệt huyện
                                huyen.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                huyen.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(huyen);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Huyện này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC tỉnh sau khi duyệt huyện
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
                                objdata.SoDVHCCon = await _dvhcRepos.CountAsync(x => x.MaTinh == currentUser.DonViHanhChinhCode);
                            }
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Tỉnh này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
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
            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> HuyDuyetBaoCaoHuyen(string ma, long year)
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
                        if (objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                        {
                            commonResponseDto.Message = "Tỉnh đã được duyệt, không thể hủy duyệt huyện";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var huyen = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                            if (huyen != null)
                            {
                                //gọi hàm update biểu huyện
                                if (huyen.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                                {
                                    commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, year, (int)HAM_DUYET.HUY);
                                }

                                #region cập nhật DVHC xã sau khi duyệt xã
                                huyen.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHUA_GUI;
                                huyen.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(huyen);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Huyện này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC tỉnh sau khi duyệt huyện
                            objdata.SoDVHCDaDuyet--;
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Tỉnh này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
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
            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        private async Task<CommonResponseDto> CreateOrUpdateBieuTinh(DonViHanhChinh tinh, string maHuyen, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            var data_bieu01TKKK = await _bieu01TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_bieu01TKKK != null)
            {
                await CreateOrUpdateBieu01TKKK_Tinh(data_bieu01TKKK, tinh.Id, tinh.MaTinh, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu huyện biểu 01TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            var data_bieu02TKKK = await _bieu02TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_bieu02TKKK != null)
            {
                await CreateOrUpdateBieu02TKKK_Tinh(data_bieu02TKKK, tinh.Id, tinh.MaTinh, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu huyện biểu 02TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            var data_bieu03TKKK = await _bieu03TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_bieu03TKKK != null)
            {
                await CreateOrUpdateBieu03TKKK_Tinh(data_bieu03TKKK, tinh.Id, tinh.MaTinh, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu huyện biểu 03TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            var data_bieu04TKKK = await _bieu04TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_bieu04TKKK != null)
            {
                await CreateOrUpdateBieu04TKKK_Tinh(data_bieu04TKKK, tinh.Id, tinh.MaTinh, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu huyện biểu 04TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            var data_bieu05TKKK = await _bieu05TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_bieu05TKKK != null)
            {
                await CreateOrUpdateBieu05TKKK_Tinh(data_bieu05TKKK, tinh.Id, tinh.MaTinh, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu huyện biểu 05TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        #region Biểu 01TKKK
        private async Task CreateOrUpdateBieu01TKKK_Tinh(List<Bieu01TKKK_Huyen> huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            var data_tinh = await _bieu01TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_tinh.Count == 0)
            {
                foreach (var item in huyen)
                {
                    //Tạo các bản ghi tỉnh tương ứng với bản ghi huyện
                    await CreateBieu01TKKK_Tinh(item, tinhId, maTinh);
                }
            }
            else
            {
                foreach (var item in huyen)
                {
                    //Cập nhật các bản ghi tỉnh tương ứng với bản ghi huyện
                    await UpdateBieu01TKKK_Tinh(item, tinhId, maTinh, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu01TKKK_Tinh(Bieu01TKKK_Huyen huyen, long tinhId, string maTinh)
        {
            try
            {
                var objtinh = new Bieu01TKKK_Tinh()
                {
                    STT = huyen.STT,
                    LoaiDat = huyen.LoaiDat,
                    Ma = huyen.Ma,
                    TongDienTichDVHC = huyen.TongDienTichDVHC,
                    TongSoTheoDoiTuongSuDung = huyen.TongSoTheoDoiTuongSuDung,
                    CaNhanTrongNuoc_CNV = huyen.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = huyen.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = huyen.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = huyen.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = huyen.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = huyen.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = huyen.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = huyen.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = huyen.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = huyen.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = huyen.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = huyen.ToChucKinhTeVonNuocNgoai_TVN,
                    TongSoTheoDoiTuongDuocGiaoQuanLy = huyen.TongSoTheoDoiTuongDuocGiaoQuanLy,
                    CoQuanNhaNuoc_TCQ = huyen.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = huyen.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = huyen.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = huyen.CongDongDanCu_CDQ,
                    TinhId = tinhId,
                    MaTinh = maTinh,
                    Year = huyen.Year,
                    Active = true,
                };
                await _bieu01TKKK_TinhRepos.InsertAsync(objtinh);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu01TKKK_Tinh(Bieu01TKKK_Huyen huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            try
            {
                var objtinh = await _bieu01TKKK_TinhRepos.FirstOrDefaultAsync(x => x.MaTinh == maTinh && x.Year == year);
                if (objtinh.Id > 0)
                {
                    //update duyệt huyện
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objtinh.TongDienTichDVHC += huyen.TongDienTichDVHC;
                        objtinh.TongSoTheoDoiTuongSuDung += huyen.TongSoTheoDoiTuongSuDung;
                        objtinh.CaNhanTrongNuoc_CNV += huyen.CaNhanTrongNuoc_CNV;
                        objtinh.NguoiVietNamONuocNgoai_CNN += huyen.NguoiVietNamONuocNgoai_CNN;
                        objtinh.CoQuanNhaNuoc_TCN += huyen.CoQuanNhaNuoc_TCN;
                        objtinh.DonViSuNghiep_TSN += huyen.DonViSuNghiep_TSN;
                        objtinh.ToChucXaHoi_TXH += huyen.ToChucXaHoi_TXH;
                        objtinh.ToChucKinhTe_TKT += huyen.ToChucKinhTe_TKT;
                        objtinh.ToChucKhac_TKH += huyen.ToChucKhac_TKH;
                        objtinh.ToChucTonGiao_TTG += huyen.ToChucTonGiao_TTG;
                        objtinh.CongDongDanCu_CDS += huyen.CongDongDanCu_CDS;
                        objtinh.ToChucNuocNgoai_TNG += huyen.ToChucNuocNgoai_TNG;
                        objtinh.NguoiGocVietNamONuocNgoai_NGV += huyen.NguoiGocVietNamONuocNgoai_NGV;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN += huyen.ToChucKinhTeVonNuocNgoai_TVN;
                        objtinh.TongSoTheoDoiTuongDuocGiaoQuanLy += huyen.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objtinh.CoQuanNhaNuoc_TCQ += huyen.CoQuanNhaNuoc_TCQ;
                        objtinh.DonViSuNghiep_TSQ += huyen.DonViSuNghiep_TSQ;
                        objtinh.ToChucKinhTe_KTQ += huyen.ToChucKinhTe_KTQ;
                        objtinh.CongDongDanCu_CDQ += huyen.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt huyện
                    else
                    {
                        objtinh.TongDienTichDVHC -= huyen.TongDienTichDVHC;
                        objtinh.TongSoTheoDoiTuongSuDung -= huyen.TongSoTheoDoiTuongSuDung;
                        objtinh.CaNhanTrongNuoc_CNV -= huyen.CaNhanTrongNuoc_CNV;
                        objtinh.NguoiVietNamONuocNgoai_CNN -= huyen.NguoiVietNamONuocNgoai_CNN;
                        objtinh.CoQuanNhaNuoc_TCN -= huyen.CoQuanNhaNuoc_TCN;
                        objtinh.DonViSuNghiep_TSN -= huyen.DonViSuNghiep_TSN;
                        objtinh.ToChucXaHoi_TXH -= huyen.ToChucXaHoi_TXH;
                        objtinh.ToChucKinhTe_TKT -= huyen.ToChucKinhTe_TKT;
                        objtinh.ToChucKhac_TKH -= huyen.ToChucKhac_TKH;
                        objtinh.ToChucTonGiao_TTG -= huyen.ToChucTonGiao_TTG;
                        objtinh.CongDongDanCu_CDS -= huyen.CongDongDanCu_CDS;
                        objtinh.ToChucNuocNgoai_TNG -= huyen.ToChucNuocNgoai_TNG;
                        objtinh.NguoiGocVietNamONuocNgoai_NGV -= huyen.NguoiGocVietNamONuocNgoai_NGV;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN -= huyen.ToChucKinhTeVonNuocNgoai_TVN;
                        objtinh.TongSoTheoDoiTuongDuocGiaoQuanLy -= huyen.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objtinh.CoQuanNhaNuoc_TCQ -= huyen.CoQuanNhaNuoc_TCQ;
                        objtinh.DonViSuNghiep_TSQ -= huyen.DonViSuNghiep_TSQ;
                        objtinh.ToChucKinhTe_KTQ -= huyen.ToChucKinhTe_KTQ;
                        objtinh.CongDongDanCu_CDQ -= huyen.CongDongDanCu_CDQ;
                    }
                    await _bieu01TKKK_TinhRepos.UpdateAsync(objtinh);
                }
                else
                {
                    await CreateBieu01TKKK_Tinh(huyen, tinhId, maTinh);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 02TKKK
        private async Task CreateOrUpdateBieu02TKKK_Tinh(List<Bieu02TKKK_Huyen> huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            var data_tinh = await _bieu02TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_tinh.Count == 0)
            {
                foreach (var item in huyen)
                {
                    //Tạo các bản ghi tỉnh tương ứng với bản ghi huyện
                    await CreateBieu02TKKK_Tinh(item, tinhId, maTinh);
                }
            }
            else
            {
                foreach (var item in huyen)
                {
                    //Cập nhật các bản ghi tỉnh tương ứng với bản ghi huyện
                    await UpdateBieu02TKKK_Tinh(item, tinhId, maTinh, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu02TKKK_Tinh(Bieu02TKKK_Huyen huyen, long tinhId, string maTinh)
        {
            try
            {
                var objtinh = new Bieu02TKKK_Tinh()
                {
                    STT = huyen.STT,
                    LoaiDat = huyen.LoaiDat,
                    Ma = huyen.Ma,
                    TongSo = huyen.TongSo,
                    CaNhanTrongNuoc_CNV = huyen.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = huyen.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = huyen.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = huyen.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = huyen.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = huyen.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = huyen.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = huyen.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = huyen.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = huyen.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = huyen.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = huyen.ToChucKinhTeVonNuocNgoai_TVN,
                    CoQuanNhaNuoc_TCQ = huyen.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = huyen.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = huyen.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = huyen.CongDongDanCu_CDQ,
                    TinhId = tinhId,
                    MaTinh = maTinh,
                    Year = huyen.Year,
                    Active = true,
                };
                await _bieu02TKKK_TinhRepos.InsertAsync(objtinh);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu02TKKK_Tinh(Bieu02TKKK_Huyen huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            try
            {
                var objtinh = await _bieu02TKKK_TinhRepos.FirstOrDefaultAsync(x => x.MaTinh == maTinh && x.Year == year);
                if (objtinh.Id > 0)
                {
                    //update duyệt huyện
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objtinh.TongSo += huyen.TongSo;
                        objtinh.CaNhanTrongNuoc_CNV += huyen.CaNhanTrongNuoc_CNV;
                        objtinh.NguoiVietNamONuocNgoai_CNN += huyen.NguoiVietNamONuocNgoai_CNN;
                        objtinh.CoQuanNhaNuoc_TCN += huyen.CoQuanNhaNuoc_TCN;
                        objtinh.DonViSuNghiep_TSN += huyen.DonViSuNghiep_TSN;
                        objtinh.ToChucXaHoi_TXH += huyen.ToChucXaHoi_TXH;
                        objtinh.ToChucKinhTe_TKT += huyen.ToChucKinhTe_TKT;
                        objtinh.ToChucKhac_TKH += huyen.ToChucKhac_TKH;
                        objtinh.ToChucTonGiao_TTG += huyen.ToChucTonGiao_TTG;
                        objtinh.CongDongDanCu_CDS += huyen.CongDongDanCu_CDS;
                        objtinh.ToChucNuocNgoai_TNG += huyen.ToChucNuocNgoai_TNG;
                        objtinh.NguoiGocVietNamONuocNgoai_NGV += huyen.NguoiGocVietNamONuocNgoai_NGV;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN += huyen.ToChucKinhTeVonNuocNgoai_TVN;
                        objtinh.CoQuanNhaNuoc_TCQ += huyen.CoQuanNhaNuoc_TCQ;
                        objtinh.DonViSuNghiep_TSQ += huyen.DonViSuNghiep_TSQ;
                        objtinh.ToChucKinhTe_KTQ += huyen.ToChucKinhTe_KTQ;
                        objtinh.CongDongDanCu_CDQ += huyen.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt huyện
                    else
                    {
                        objtinh.TongSo -= huyen.TongSo;
                        objtinh.CaNhanTrongNuoc_CNV -= huyen.CaNhanTrongNuoc_CNV;
                        objtinh.NguoiVietNamONuocNgoai_CNN -= huyen.NguoiVietNamONuocNgoai_CNN;
                        objtinh.CoQuanNhaNuoc_TCN -= huyen.CoQuanNhaNuoc_TCN;
                        objtinh.DonViSuNghiep_TSN -= huyen.DonViSuNghiep_TSN;
                        objtinh.ToChucXaHoi_TXH -= huyen.ToChucXaHoi_TXH;
                        objtinh.ToChucKinhTe_TKT -= huyen.ToChucKinhTe_TKT;
                        objtinh.ToChucKhac_TKH -= huyen.ToChucKhac_TKH;
                        objtinh.ToChucTonGiao_TTG -= huyen.ToChucTonGiao_TTG;
                        objtinh.CongDongDanCu_CDS -= huyen.CongDongDanCu_CDS;
                        objtinh.ToChucNuocNgoai_TNG -= huyen.ToChucNuocNgoai_TNG;
                        objtinh.NguoiGocVietNamONuocNgoai_NGV -= huyen.NguoiGocVietNamONuocNgoai_NGV;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN -= huyen.ToChucKinhTeVonNuocNgoai_TVN;
                        objtinh.CoQuanNhaNuoc_TCQ -= huyen.CoQuanNhaNuoc_TCQ;
                        objtinh.DonViSuNghiep_TSQ -= huyen.DonViSuNghiep_TSQ;
                        objtinh.ToChucKinhTe_KTQ -= huyen.ToChucKinhTe_KTQ;
                        objtinh.CongDongDanCu_CDQ -= huyen.CongDongDanCu_CDQ;
                    }
                    await _bieu02TKKK_TinhRepos.UpdateAsync(objtinh);
                }
                else
                {
                    await CreateBieu02TKKK_Tinh(huyen, tinhId, maTinh);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 03TKKK
        private async Task CreateOrUpdateBieu03TKKK_Tinh(List<Bieu03TKKK_Huyen> huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            var data_tinh = await _bieu03TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_tinh.Count == 0)
            {
                foreach (var item in huyen)
                {
                    //Tạo các bản ghi tỉnh tương ứng với bản ghi huyện
                    await CreateBieu03TKKK_Tinh(item, tinhId, maTinh);
                }
            }
            else
            {
                foreach (var item in huyen)
                {
                    //Cập nhật các bản ghi tỉnh tương ứng với bản ghi huyện
                    await UpdateBieu03TKKK_Tinh(item, tinhId, maTinh, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu03TKKK_Tinh(Bieu03TKKK_Huyen huyen, long tinhId, string maTinh)
        {
            try
            {
                var bieu03Tkkk_huyen = new DVHCBieu03TKKKDto
                {
                    TenDVHC = _dvhcRepos.Single(x => x.Ma == huyen.MaHuyen).Name,
                    MaDVHC = huyen.MaHuyen,
                    TenLoaiDat = huyen.LoaiDat,
                    MaLoaiDat = huyen.Ma,
                    DienTich = huyen.TongDienTich,
                };
                var lstBieu03Tkkk_huyen = new List<DVHCBieu03TKKKDto>();
                lstBieu03Tkkk_huyen.Add(bieu03Tkkk_huyen);
                var objtinh = new Bieu03TKKK_Tinh()
                {
                    STT = huyen.STT,
                    LoaiDat = huyen.LoaiDat,
                    Ma = huyen.Ma,
                    TongDienTich = huyen.TongDienTich,
                    DienTichTheoDVHC = lstBieu03Tkkk_huyen.ToJson(),
                    TinhId = tinhId,
                    MaTinh = maTinh,
                    Year = huyen.Year,
                    Active = true,
                };
                await _bieu03TKKK_TinhRepos.InsertAsync(objtinh);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu03TKKK_Tinh(Bieu03TKKK_Huyen huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            try
            {
                var objtinh = await _bieu03TKKK_TinhRepos.FirstOrDefaultAsync(x => x.MaTinh == maTinh && x.Year == year);
                if (objtinh.Id > 0)
                {
                    var dientichtheoDVHC = objtinh.DienTichTheoDVHC.FromJson<List<DVHCBieu03TKKKDto>>();
                    //update duyệt huyện
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        var bieu03Tkkk_huyen = new DVHCBieu03TKKKDto
                        {
                            TenDVHC = _dvhcRepos.Single(x => x.Ma == huyen.MaHuyen).Name,
                            MaDVHC = huyen.MaHuyen,
                            TenLoaiDat = huyen.LoaiDat,
                            MaLoaiDat = huyen.Ma,
                            DienTich = huyen.TongDienTich,
                        };
                        objtinh.TongDienTich += huyen.TongDienTich;
                        dientichtheoDVHC.Add(bieu03Tkkk_huyen);
                        objtinh.DienTichTheoDVHC = dientichtheoDVHC.ToJson();
                    }
                    //update huỷ duyệt huyện
                    else
                    {
                        objtinh.TongDienTich -= huyen.TongDienTich;
                        if (dientichtheoDVHC.FirstOrDefault(x => x.MaDVHC == huyen.MaHuyen && x.MaLoaiDat == huyen.Ma) != null)
                            dientichtheoDVHC.Remove(dientichtheoDVHC.FirstOrDefault(x => x.MaDVHC == huyen.MaHuyen && x.MaLoaiDat == huyen.Ma));
                        objtinh.DienTichTheoDVHC = dientichtheoDVHC.ToJson();
                    }
                    await _bieu03TKKK_TinhRepos.UpdateAsync(objtinh);
                }
                else
                {
                    await CreateBieu03TKKK_Tinh(huyen, tinhId, maTinh);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 04TKKK
        private async Task CreateOrUpdateBieu04TKKK_Tinh(List<Bieu04TKKK_Huyen> huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            var data_tinh = await _bieu04TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_tinh.Count == 0)
            {
                foreach (var item in huyen)
                {
                    //Tạo các bản ghi tỉnh tương ứng với bản ghi huyện
                    await CreateBieu04TKKK_Tinh(item, tinhId, maTinh);
                }
            }
            else
            {
                foreach (var item in huyen)
                {
                    //Cập nhật các bản ghi tỉnh tương ứng với bản ghi huyện
                    await UpdateBieu04TKKK_Tinh(item, tinhId, maTinh, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu04TKKK_Tinh(Bieu04TKKK_Huyen huyen, long tinhId, string maTinh)
        {
            try
            {
                var objtinh = new Bieu04TKKK_Tinh()
                {
                    STT = huyen.STT,
                    LoaiDat = huyen.LoaiDat,
                    Ma = huyen.Ma,
                    TongSo_DT = huyen.TongSo_DT,
                    TongSo_CC = huyen.TongSo_CC,
                    CaNhanTrongNuoc_CNV_DT = huyen.CaNhanTrongNuoc_CNV_DT,
                    CaNhanTrongNuoc_CNV_CC = huyen.CaNhanTrongNuoc_CNV_CC,
                    NguoiVietNamONuocNgoai_CNN_DT = huyen.NguoiVietNamONuocNgoai_CNN_DT,
                    NguoiVietNamONuocNgoai_CNN_CC = huyen.NguoiVietNamONuocNgoai_CNN_CC,
                    CoQuanNhaNuoc_TCN_DT = huyen.CoQuanNhaNuoc_TCN_DT,
                    CoQuanNhaNuoc_TCN_CC = huyen.CoQuanNhaNuoc_TCN_CC,
                    DonViSuNghiep_TSN_DT = huyen.NguoiVietNamONuocNgoai_CNN_DT,
                    DonViSuNghiep_TSN_CC = huyen.DonViSuNghiep_TSN_CC,
                    ToChucXaHoi_TXH_DT = huyen.ToChucXaHoi_TXH_DT,
                    ToChucXaHoi_TXH_CC = huyen.ToChucXaHoi_TXH_CC,
                    ToChucKinhTe_TKT_DT = huyen.ToChucKinhTe_TKT_DT,
                    ToChucKinhTe_TKT_CC = huyen.ToChucKinhTe_TKT_CC,
                    ToChucKhac_TKH_DT = huyen.ToChucKhac_TKH_DT,
                    ToChucKhac_TKH_CC = huyen.ToChucKhac_TKH_CC,
                    ToChucTonGiao_TTG_DT = huyen.ToChucTonGiao_TTG_DT,
                    ToChucTonGiao_TTG_CC = huyen.ToChucTonGiao_TTG_CC,
                    CongDongDanCu_CDS_DT = huyen.CongDongDanCu_CDS_DT,
                    CongDongDanCu_CDS_CC = huyen.CongDongDanCu_CDS_CC,
                    ToChucNuocNgoai_TNG_DT = huyen.ToChucNuocNgoai_TNG_DT,
                    ToChucNuocNgoai_TNG_CC = huyen.ToChucNuocNgoai_TNG_CC,
                    NguoiGocVietNamONuocNgoai_CNN_DT = huyen.NguoiGocVietNamONuocNgoai_CNN_DT,
                    NguoiGocVietNamONuocNgoai_CNN_CC = huyen.NguoiGocVietNamONuocNgoai_CNN_CC,
                    ToChucKinhTeVonNuocNgoai_TVN_DT = huyen.ToChucKinhTeVonNuocNgoai_TVN_DT,
                    ToChucKinhTeVonNuocNgoai_TVN_CC = huyen.ToChucKinhTeVonNuocNgoai_TVN_CC,
                    CoQuanNhaNuoc_TCQ_DT = huyen.CoQuanNhaNuoc_TCQ_DT,
                    CoQuanNhaNuoc_TCQ_CC = huyen.CoQuanNhaNuoc_TCQ_CC,
                    DonViSuNghiep_TSQ_DT = huyen.DonViSuNghiep_TSQ_DT,
                    DonViSuNghiep_TSQ_CC = huyen.DonViSuNghiep_TSQ_CC,
                    ToChucKinhTe_KTQ_DT = huyen.ToChucKinhTe_KTQ_DT,
                    ToChucKinhTe_KTQ_CC = huyen.ToChucKinhTe_KTQ_CC,
                    CongDongDanCu_CDQ_DT = huyen.CongDongDanCu_CDQ_DT,
                    CongDongDanCu_CDQ_CC = huyen.CongDongDanCu_CDQ_CC,
                    TinhId = tinhId,
                    MaTinh = maTinh,
                    Year = huyen.Year,
                    Active = true,
                };
                await _bieu04TKKK_TinhRepos.InsertAsync(objtinh);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu04TKKK_Tinh(Bieu04TKKK_Huyen huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            try
            {
                var objtinh = await _bieu04TKKK_TinhRepos.FirstOrDefaultAsync(x => x.MaTinh == maTinh && x.Year == year);
                if (objtinh.Id > 0)
                {
                    //update duyệt huyện
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objtinh.TongSo_DT += huyen.TongSo_DT;
                        objtinh.TongSo_CC += huyen.TongSo_CC;
                        objtinh.CaNhanTrongNuoc_CNV_DT += huyen.CaNhanTrongNuoc_CNV_DT;
                        objtinh.CaNhanTrongNuoc_CNV_CC += huyen.CaNhanTrongNuoc_CNV_CC;
                        objtinh.NguoiVietNamONuocNgoai_CNN_DT += huyen.NguoiVietNamONuocNgoai_CNN_DT;
                        objtinh.NguoiVietNamONuocNgoai_CNN_CC += huyen.NguoiVietNamONuocNgoai_CNN_CC;
                        objtinh.CoQuanNhaNuoc_TCN_DT += huyen.CoQuanNhaNuoc_TCN_DT;
                        objtinh.CoQuanNhaNuoc_TCN_CC += huyen.CoQuanNhaNuoc_TCN_CC;
                        objtinh.DonViSuNghiep_TSN_DT += huyen.DonViSuNghiep_TSN_DT;
                        objtinh.DonViSuNghiep_TSN_CC += huyen.DonViSuNghiep_TSN_CC;
                        objtinh.ToChucXaHoi_TXH_DT += huyen.ToChucXaHoi_TXH_DT;
                        objtinh.ToChucXaHoi_TXH_CC += huyen.ToChucXaHoi_TXH_CC;
                        objtinh.ToChucKinhTe_TKT_DT += huyen.ToChucKinhTe_TKT_DT;
                        objtinh.ToChucKinhTe_TKT_CC += huyen.ToChucKinhTe_TKT_CC;
                        objtinh.ToChucKhac_TKH_DT += huyen.ToChucKhac_TKH_DT;
                        objtinh.ToChucKhac_TKH_CC += huyen.ToChucKhac_TKH_CC;
                        objtinh.ToChucTonGiao_TTG_DT += huyen.ToChucTonGiao_TTG_DT;
                        objtinh.ToChucTonGiao_TTG_CC += huyen.ToChucTonGiao_TTG_CC;
                        objtinh.CongDongDanCu_CDS_DT += huyen.CongDongDanCu_CDS_DT;
                        objtinh.CongDongDanCu_CDS_CC += huyen.CongDongDanCu_CDS_CC;
                        objtinh.ToChucNuocNgoai_TNG_DT += huyen.ToChucNuocNgoai_TNG_DT;
                        objtinh.ToChucNuocNgoai_TNG_CC += huyen.ToChucNuocNgoai_TNG_CC;
                        objtinh.NguoiGocVietNamONuocNgoai_CNN_DT += huyen.NguoiGocVietNamONuocNgoai_CNN_DT;
                        objtinh.NguoiGocVietNamONuocNgoai_CNN_CC += huyen.NguoiGocVietNamONuocNgoai_CNN_CC;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN_DT += huyen.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN_CC += huyen.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        objtinh.CoQuanNhaNuoc_TCQ_DT += huyen.CoQuanNhaNuoc_TCQ_DT;
                        objtinh.CoQuanNhaNuoc_TCQ_CC += huyen.CoQuanNhaNuoc_TCQ_CC;
                        objtinh.DonViSuNghiep_TSQ_DT += huyen.DonViSuNghiep_TSQ_DT;
                        objtinh.DonViSuNghiep_TSQ_CC += huyen.DonViSuNghiep_TSQ_CC;
                        objtinh.ToChucKinhTe_KTQ_DT += huyen.ToChucKinhTe_KTQ_DT;
                        objtinh.ToChucKinhTe_KTQ_CC += huyen.ToChucKinhTe_KTQ_CC;
                        objtinh.CongDongDanCu_CDQ_DT += huyen.CongDongDanCu_CDQ_DT;
                        objtinh.CongDongDanCu_CDQ_CC += huyen.CongDongDanCu_CDQ_CC;
                    }
                    //update huỷ duyệt huyện
                    else
                    {
                        objtinh.TongSo_DT -= huyen.TongSo_DT;
                        objtinh.TongSo_CC -= huyen.TongSo_CC;
                        objtinh.CaNhanTrongNuoc_CNV_DT -= huyen.CaNhanTrongNuoc_CNV_DT;
                        objtinh.CaNhanTrongNuoc_CNV_CC -= huyen.CaNhanTrongNuoc_CNV_CC;
                        objtinh.NguoiVietNamONuocNgoai_CNN_DT -= huyen.NguoiVietNamONuocNgoai_CNN_DT;
                        objtinh.NguoiVietNamONuocNgoai_CNN_CC -= huyen.NguoiVietNamONuocNgoai_CNN_CC;
                        objtinh.CoQuanNhaNuoc_TCN_DT -= huyen.CoQuanNhaNuoc_TCN_DT;
                        objtinh.CoQuanNhaNuoc_TCN_CC -= huyen.CoQuanNhaNuoc_TCN_CC;
                        objtinh.DonViSuNghiep_TSN_DT -= huyen.DonViSuNghiep_TSN_DT;
                        objtinh.DonViSuNghiep_TSN_CC -= huyen.DonViSuNghiep_TSN_CC;
                        objtinh.ToChucXaHoi_TXH_DT -= huyen.ToChucXaHoi_TXH_DT;
                        objtinh.ToChucXaHoi_TXH_CC -= huyen.ToChucXaHoi_TXH_CC;
                        objtinh.ToChucKinhTe_TKT_DT -= huyen.ToChucKinhTe_TKT_DT;
                        objtinh.ToChucKinhTe_TKT_CC -= huyen.ToChucKinhTe_TKT_CC;
                        objtinh.ToChucKhac_TKH_DT -= huyen.ToChucKhac_TKH_DT;
                        objtinh.ToChucKhac_TKH_CC -= huyen.ToChucKhac_TKH_CC;
                        objtinh.ToChucTonGiao_TTG_DT -= huyen.ToChucTonGiao_TTG_DT;
                        objtinh.ToChucTonGiao_TTG_CC -= huyen.ToChucTonGiao_TTG_CC;
                        objtinh.CongDongDanCu_CDS_DT -= huyen.CongDongDanCu_CDS_DT;
                        objtinh.CongDongDanCu_CDS_CC -= huyen.CongDongDanCu_CDS_CC;
                        objtinh.ToChucNuocNgoai_TNG_DT -= huyen.ToChucNuocNgoai_TNG_DT;
                        objtinh.ToChucNuocNgoai_TNG_CC -= huyen.ToChucNuocNgoai_TNG_CC;
                        objtinh.NguoiGocVietNamONuocNgoai_CNN_DT -= huyen.NguoiGocVietNamONuocNgoai_CNN_DT;
                        objtinh.NguoiGocVietNamONuocNgoai_CNN_CC -= huyen.NguoiGocVietNamONuocNgoai_CNN_CC;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN_DT -= huyen.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN_CC -= huyen.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        objtinh.CoQuanNhaNuoc_TCQ_DT -= huyen.CoQuanNhaNuoc_TCQ_DT;
                        objtinh.CoQuanNhaNuoc_TCQ_CC -= huyen.CoQuanNhaNuoc_TCQ_CC;
                        objtinh.DonViSuNghiep_TSQ_DT -= huyen.DonViSuNghiep_TSQ_DT;
                        objtinh.DonViSuNghiep_TSQ_CC -= huyen.DonViSuNghiep_TSQ_CC;
                        objtinh.ToChucKinhTe_KTQ_DT -= huyen.ToChucKinhTe_KTQ_DT;
                        objtinh.ToChucKinhTe_KTQ_CC -= huyen.ToChucKinhTe_KTQ_CC;
                        objtinh.CongDongDanCu_CDQ_DT -= huyen.CongDongDanCu_CDQ_DT;
                        objtinh.CongDongDanCu_CDQ_CC -= huyen.CongDongDanCu_CDQ_CC;
                    }
                    await _bieu04TKKK_TinhRepos.UpdateAsync(objtinh);
                }
                else
                {
                    await CreateBieu04TKKK_Tinh(huyen, tinhId, maTinh);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 05TKKK
        private async Task CreateOrUpdateBieu05TKKK_Tinh(List<Bieu05TKKK_Huyen> huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            var data_tinh = await _bieu05TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_tinh.Count == 0)
            {
                foreach (var item in huyen)
                {
                    //Tạo các bản ghi tỉnh tương ứng với bản ghi huyện
                    await CreateBieu05TKKK_Tinh(item, tinhId, maTinh);
                }
            }
            else
            {
                foreach (var item in huyen)
                {
                    //Cập nhật các bản ghi tỉnh tương ứng với bản ghi huyện
                    await UpdateBieu05TKKK_Tinh(item, tinhId, maTinh, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu05TKKK_Tinh(Bieu05TKKK_Huyen huyen, long tinhId, string maTinh)
        {
            try
            {
                var objtinh = new Bieu05TKKK_Tinh()
                {
                    STT = huyen.STT,
                    LoaiDat = huyen.LoaiDat,
                    Ma = huyen.Ma,
                    Nam = huyen.Nam,
                    LUA = huyen.LUA,
                    HNK = huyen.HNK,
                    CLN = huyen.CLN,
                    RDD = huyen.RDD,
                    RPH = huyen.RPH,
                    RSX = huyen.RSX,
                    NTS = huyen.NTS,
                    CNT = huyen.CNT,
                    LMU = huyen.LMU,
                    NKH = huyen.NKH,
                    ONT = huyen.ONT,
                    ODT = huyen.ODT,
                    TSC = huyen.TSC,
                    CQP = huyen.CQP,
                    CAN = huyen.CAN,
                    DVH = huyen.DVH,
                    DXH = huyen.DXH,
                    DYT = huyen.DYT,
                    DGD = huyen.DGD,
                    DTT = huyen.DTT,
                    DKH = huyen.DKH,
                    DMT = huyen.DMT,
                    DKT = huyen.DKT,
                    DNG = huyen.DNG,
                    DSK = huyen.DSK,
                    SKK = huyen.SKK,
                    SKN = huyen.SKN,
                    SCT = huyen.SCT,
                    TMD = huyen.TMD,
                    SKC = huyen.SKC,
                    SKS = huyen.SKS,
                    DGT = huyen.DGT,
                    DTL = huyen.DTL,
                    DCT = huyen.DCT,
                    DPC = huyen.DPC,
                    DDD = huyen.DDD,
                    DRA = huyen.DRA,
                    DNL = huyen.DNL,
                    DBV = huyen.DBV,
                    DCH = huyen.DCH,
                    DKV = huyen.DKV,
                    TON = huyen.TON,
                    TIN = huyen.TIN,
                    NTD = huyen.NTD,
                    MNC = huyen.MNC,
                    SON = huyen.SON,
                    PNK = huyen.PNK,
                    CGT = huyen.CGT,
                    BCS = huyen.BCS,
                    DCS = huyen.DCS,
                    NCS = huyen.NCS,
                    MCS = huyen.MCS,
                    GiamKhac = huyen.GiamKhac,
                    TinhId = tinhId,
                    MaTinh = maTinh,
                    Year = huyen.Year,
                    Active = true,
                };
                await _bieu05TKKK_TinhRepos.InsertAsync(objtinh);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu05TKKK_Tinh(Bieu05TKKK_Huyen huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            try
            {
                var objtinh = await _bieu05TKKK_TinhRepos.FirstOrDefaultAsync(x => x.MaTinh == maTinh && x.Year == year);
                if (objtinh.Id > 0)
                {
                    //update duyệt huyện
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objtinh.Nam += huyen.Nam;
                        objtinh.LUA += huyen.LUA;
                        objtinh.HNK += huyen.HNK;
                        objtinh.CLN += huyen.CLN;
                        objtinh.RDD += huyen.RDD;
                        objtinh.RPH += huyen.RPH;
                        objtinh.RSX += huyen.RSX;
                        objtinh.NTS += huyen.NTS;
                        objtinh.CNT += huyen.CNT;
                        objtinh.LMU += huyen.LMU;
                        objtinh.NKH += huyen.NKH;
                        objtinh.ONT += huyen.ONT;
                        objtinh.ODT += huyen.ODT;
                        objtinh.TSC += huyen.TSC;
                        objtinh.CQP += huyen.CQP;
                        objtinh.CAN += huyen.CAN;
                        objtinh.DVH += huyen.DVH;
                        objtinh.DXH += huyen.DXH;
                        objtinh.DYT += huyen.DYT;
                        objtinh.DGD += huyen.DGD;
                        objtinh.DTT += huyen.DTT;
                        objtinh.DKH += huyen.DKH;
                        objtinh.DMT += huyen.DMT;
                        objtinh.DKT += huyen.DKT;
                        objtinh.DNG += huyen.DNG;
                        objtinh.DSK += huyen.DSK;
                        objtinh.SKK += huyen.SKK;
                        objtinh.SKN += huyen.SKN;
                        objtinh.SCT += huyen.SCT;
                        objtinh.TMD += huyen.TMD;
                        objtinh.SKC += huyen.SKC;
                        objtinh.SKS += huyen.SKS;
                        objtinh.DGT += huyen.DGT;
                        objtinh.DTL += huyen.DTL;
                        objtinh.DCT += huyen.DCT;
                        objtinh.DPC += huyen.DPC;
                        objtinh.DDD += huyen.DDD;
                        objtinh.DRA += huyen.DRA;
                        objtinh.DNL += huyen.DNL;
                        objtinh.DBV += huyen.DBV;
                        objtinh.DCH += huyen.DCH;
                        objtinh.DKV += huyen.DKV;
                        objtinh.TON += huyen.TON;
                        objtinh.TIN += huyen.TIN;
                        objtinh.NTD += huyen.NTD;
                        objtinh.MNC += huyen.MNC;
                        objtinh.SON += huyen.SON;
                        objtinh.PNK += huyen.PNK;
                        objtinh.CGT += huyen.CGT;
                        objtinh.BCS += huyen.BCS;
                        objtinh.DCS += huyen.DCS;
                        objtinh.NCS += huyen.NCS;
                        objtinh.MCS += huyen.MCS;
                        objtinh.GiamKhac += huyen.GiamKhac;
                    }
                    //update huỷ duyệt huyện
                    else
                    {
                        objtinh.Nam -= huyen.Nam;
                        objtinh.LUA -= huyen.LUA;
                        objtinh.HNK -= huyen.HNK;
                        objtinh.CLN -= huyen.CLN;
                        objtinh.RDD -= huyen.RDD;
                        objtinh.RPH -= huyen.RPH;
                        objtinh.RSX -= huyen.RSX;
                        objtinh.NTS -= huyen.NTS;
                        objtinh.CNT -= huyen.CNT;
                        objtinh.LMU -= huyen.LMU;
                        objtinh.NKH -= huyen.NKH;
                        objtinh.ONT -= huyen.ONT;
                        objtinh.ODT -= huyen.ODT;
                        objtinh.TSC -= huyen.TSC;
                        objtinh.CQP -= huyen.CQP;
                        objtinh.CAN -= huyen.CAN;
                        objtinh.DVH -= huyen.DVH;
                        objtinh.DXH -= huyen.DXH;
                        objtinh.DYT -= huyen.DYT;
                        objtinh.DGD -= huyen.DGD;
                        objtinh.DTT -= huyen.DTT;
                        objtinh.DKH -= huyen.DKH;
                        objtinh.DMT -= huyen.DMT;
                        objtinh.DKT -= huyen.DKT;
                        objtinh.DNG -= huyen.DNG;
                        objtinh.DSK -= huyen.DSK;
                        objtinh.SKK -= huyen.SKK;
                        objtinh.SKN -= huyen.SKN;
                        objtinh.SCT -= huyen.SCT;
                        objtinh.TMD -= huyen.TMD;
                        objtinh.SKC -= huyen.SKC;
                        objtinh.SKS -= huyen.SKS;
                        objtinh.DGT -= huyen.DGT;
                        objtinh.DTL -= huyen.DTL;
                        objtinh.DCT -= huyen.DCT;
                        objtinh.DPC -= huyen.DPC;
                        objtinh.DDD -= huyen.DDD;
                        objtinh.DRA -= huyen.DRA;
                        objtinh.DNL -= huyen.DNL;
                        objtinh.DBV -= huyen.DBV;
                        objtinh.DCH -= huyen.DCH;
                        objtinh.DKV -= huyen.DKV;
                        objtinh.TON -= huyen.TON;
                        objtinh.TIN -= huyen.TIN;
                        objtinh.NTD -= huyen.NTD;
                        objtinh.MNC -= huyen.MNC;
                        objtinh.SON -= huyen.SON;
                        objtinh.PNK -= huyen.PNK;
                        objtinh.CGT -= huyen.CGT;
                        objtinh.BCS -= huyen.BCS;
                        objtinh.DCS -= huyen.DCS;
                        objtinh.NCS -= huyen.NCS;
                        objtinh.MCS -= huyen.MCS;
                        objtinh.GiamKhac -= huyen.GiamKhac;
                    }
                    await _bieu05TKKK_TinhRepos.UpdateAsync(objtinh);
                }
                else
                {
                    await CreateBieu05TKKK_Tinh(huyen, tinhId, maTinh);
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
