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
    public class HuyenAppService : KiemKeDatDaiAppServiceBase, IHuyenAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;

        private readonly IRepository<Bieu02TKKK, long> _bieu02TKKKRepos;
        private readonly IRepository<Bieu02TKKK_Xa, long> _bieu02TKKK_XaRepos;
        private readonly IRepository<Bieu02TKKK_Huyen, long> _bieu02TKKK_HuyenRepos;
        private readonly IRepository<Bieu02TKKK_Tinh, long> _bieu02TKKK_TinhRepos;
        private readonly IRepository<Bieu02TKKK_Vung, long> _bieu02TKKK_VungRepos;

        private readonly IRepository<Bieu03TKKK, long> _bieu03TKKKRepos;
        private readonly IRepository<Bieu03TKKK_Huyen, long> _bieu03TKKK_HuyenRepos;
        private readonly IRepository<Bieu03TKKK_Tinh, long> _bieu03TKKK_TinhRepos;
        private readonly IRepository<Bieu03TKKK_Vung, long> _bieu03TKKK_VungRepos;

        private readonly IRepository<Bieu04TKKK, long> _bieu04TKKKRepos;
        private readonly IRepository<Bieu04TKKK_Xa, long> _bieu04TKKK_XaRepos;
        private readonly IRepository<Bieu04TKKK_Huyen, long> _bieu04TKKK_HuyenRepos;
        private readonly IRepository<Bieu04TKKK_Tinh, long> _bieu04TKKK_TinhRepos;
        private readonly IRepository<Bieu04TKKK_Vung, long> _bieu04TKKK_VungRepos;

        private readonly IRepository<Bieu05TKKK, long> _bieu05TKKKRepos;
        private readonly IRepository<Bieu05TKKK_Xa, long> _bieu05TKKK_XaRepos;
        private readonly IRepository<Bieu05TKKK_Huyen, long> _bieu05TKKK_HuyenRepos;
        private readonly IRepository<Bieu05TKKK_Tinh, long> _bieu05TKKK_TinhRepos;
        private readonly IRepository<Bieu05TKKK_Vung, long> _bieu05TKKK_VungRepos;

        private readonly IRepository<Bieu01KKSL, long> _bieu01KKSLRepos;
        private readonly IRepository<Bieu01KKSL_Xa, long> _bieu01KKSL_XaRepos;
        private readonly IRepository<Bieu01KKSL_Huyen, long> _bieu01KKSL_HuyenRepos;
        private readonly IRepository<Bieu01KKSL_Tinh, long> _bieu01KKSL_TinhRepos;
        private readonly IRepository<Bieu01KKSL_Vung, long> _bieu01KKSL_VungRepos;

        private readonly IRepository<Bieu02KKSL, long> _bieu02KKSLRepos;
        private readonly IRepository<Bieu02KKSL_Xa, long> _bieu02KKSL_XaRepos;
        private readonly IRepository<Bieu02KKSL_Huyen, long> _bieu02KKSL_HuyenRepos;
        private readonly IRepository<Bieu02KKSL_Tinh, long> _bieu02KKSL_TinhRepos;
        private readonly IRepository<Bieu02KKSL_Vung, long> _bieu02KKSL_VungRepos;

        private readonly IRepository<Bieu01aKKNLT, long> _bieu01aKKNLTRepos;
        private readonly IRepository<Bieu01aKKNLT_Xa, long> _bieu01aKKNLT_XaRepos;
        private readonly IRepository<Bieu01aKKNLT_Huyen, long> _bieu01aKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01aKKNLT_Tinh, long> _bieu01aKKNLT_TinhRepos;
        private readonly IRepository<Bieu01aKKNLT_Vung, long> _bieu01aKKNLT_VungRepos;

        private readonly IRepository<Bieu01bKKNLT, long> _bieu01bKKNLTRepos;
        private readonly IRepository<Bieu01bKKNLT_Xa, long> _bieu01bKKNLT_XaRepos;
        private readonly IRepository<Bieu01bKKNLT_Huyen, long> _bieu01bKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01bKKNLT_Tinh, long> _bieu01bKKNLT_TinhRepos;
        private readonly IRepository<Bieu01bKKNLT_Vung, long> _bieu01bKKNLT_VungRepos;

        private readonly IRepository<Bieu01cKKNLT, long> _bieu01cKKNLTRepos;
        private readonly IRepository<Bieu01cKKNLT_Xa, long> _bieu01cKKNLT_XaRepos;
        private readonly IRepository<Bieu01cKKNLT_Huyen, long> _bieu01cKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01cKKNLT_Tinh, long> _bieu01cKKNLT_TinhRepos;
        private readonly IRepository<Bieu01cKKNLT_Vung, long> _bieu01cKKNLT_VungRepos;

        private readonly IRepository<Bieu06TKKKQPAN, long> _bieu06TKKKQPANRepos;
        private readonly IRepository<Bieu06TKKKQPAN_Tinh, long> _bieu06TKKKQPAN_TinhRepos;
        private readonly IRepository<BieuPhuLucIII, long> _bieuPhuLucIIIRepos;
        private readonly IRepository<BieuPhuLucIV, long> _bieuPhuLucIVRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public HuyenAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,

            IRepository<Bieu02TKKK_Xa, long> bieu02TKKK_XaRepos,
            IRepository<Bieu02TKKK_Huyen, long> bieu02TKKK_HuyenRepos,

            IRepository<Bieu04TKKK_Xa, long> bieu04TKKK_XaRepos,
            IRepository<Bieu04TKKK_Huyen, long> bieu04TKKK_HuyenRepos,

            IRepository<Bieu05TKKK_Xa, long> bieu05TKKK_XaRepos,
            IRepository<Bieu05TKKK_Huyen, long> bieu05TKKK_HuyenRepos,

            IRepository<Bieu01KKSL_Xa, long> bieu01KKSL_XaRepos,
            IRepository<Bieu01KKSL_Huyen, long> bieu01KKSL_HuyenRepos,

            IRepository<Bieu02KKSL_Xa, long> bieu02KKSL_XaRepos,
            IRepository<Bieu02KKSL_Huyen, long> bieu02KKSL_HuyenRepos,

            IRepository<Bieu01aKKNLT_Xa, long> bieu01aKKNLT_XaRepos,
            IRepository<Bieu01aKKNLT_Huyen, long> bieu01aKKNLT_HuyenRepos,

            IRepository<Bieu01cKKNLT_Xa, long> bieu01cKKNLT_XaRepos,
            IRepository<Bieu01cKKNLT_Huyen, long> bieu01cKKNLT_HuyenRepos,

            IRepository<Bieu01bKKNLT_Xa, long> bieu01bKKNLT_XaRepos,
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
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;

            _bieu02TKKK_XaRepos = bieu02TKKK_XaRepos;
            _bieu02TKKK_HuyenRepos = bieu02TKKK_HuyenRepos;

            _bieu04TKKK_XaRepos = bieu04TKKK_XaRepos;
            _bieu04TKKK_HuyenRepos = bieu04TKKK_HuyenRepos;

            _bieu05TKKK_XaRepos = bieu05TKKK_XaRepos;
            _bieu05TKKK_HuyenRepos = bieu05TKKK_HuyenRepos;

            _bieu01KKSL_XaRepos = bieu01KKSL_XaRepos;
            _bieu01KKSL_HuyenRepos = bieu01KKSL_HuyenRepos;

            _bieu02KKSL_XaRepos = bieu02KKSL_XaRepos;
            _bieu02KKSL_HuyenRepos = bieu02KKSL_HuyenRepos;

            _bieu01aKKNLT_XaRepos = bieu01aKKNLT_XaRepos;
            _bieu01aKKNLT_HuyenRepos = bieu01aKKNLT_HuyenRepos;

            _bieu01cKKNLT_XaRepos = bieu01cKKNLT_XaRepos;
            _bieu01cKKNLT_HuyenRepos = bieu01cKKNLT_HuyenRepos;

            _bieu01bKKNLT_XaRepos = bieu01bKKNLT_XaRepos;
            _bieu01bKKNLT_HuyenRepos = bieu01bKKNLT_HuyenRepos;

            _bieuPhuLucIIIRepos = bieuPhuLucIIIRepos;
            _bieuPhuLucIVRepos = bieuPhuLucIVRepos;

            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize]
        public async Task<CommonResponseDto> DuyetBaoCaoXa(string ma, long year)
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
                            commonResponseDto.Message = "Huyện đã được duyệt, không thể duyệt xã";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var xa = await _dvhcRepos.FirstOrDefaultAsync(x=>x.Ma == ma);
                            if (xa != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuHuyen(objdata, ma, year, (int)HAM_DUYET.DUYET);

                                #region cập nhật DVHC xã sau khi duyệt xã
                                xa.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                xa.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(xa);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Xã này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC huyện sau khi duyệt xã
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
                    }
                    else
                    {
                        commonResponseDto.Message = "Huyện này không tồn tại";
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
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> HuyDuyetBaoCaoXa(string ma, long year)
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
                            commonResponseDto.Message = "Huyện đã được duyệt, không thể hủy duyệt xã";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var xa = await _dvhcRepos.FirstOrDefaultAsync(x=>x.Ma==ma);
                            if (xa != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuHuyen(objdata, ma, year, (int)HAM_DUYET.HUY);

                                #region cập nhật DVHC xã sau khi duyệt xã
                                xa.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                xa.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(xa);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Xã này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC huyện sau khi duyệt xã
                            objdata.SoDVHCDaDuyet--;
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Huyện này không tồn tại";
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
            return commonResponseDto;
        }


        private async Task<CommonResponseDto> CreateOrUpdateBieuHuyen(DonViHanhChinh huyen, string maXa, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            var data_bieu01TKKK = await _bieu01TKKK_XaRepos.GetAllListAsync(x => x.MaXa == maXa && x.Year == year);
            if (data_bieu01TKKK.Count > 0)
            {
                await CreateOrUpdateBieu01TKKK_Huyen(data_bieu01TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu xã biểu 01TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            //var data_bieu02TKKK = await _bieu02TKKK_XaRepos.GetAllListAsync(x => x.MaXa == ma && x.Year == year);
            //if (data_bieu02TKKK.Count > 0)
            //{
            //    await CreateOrUpdateBieu02TKKK_Huyen(data_bieu02TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
            //}
            //else
            //{
            //    commonResponseDto.Message = "Dữ liệu xã biểu 01TKKK không tồn tại";
            //    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
            //}

            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        #region Biểu 01TKKK
        private async Task CreateOrUpdateBieu01TKKK_Huyen(List<Bieu01TKKK_Xa> xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            var data_huyen = await _bieu01TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_huyen.Count == 0)
            {
                foreach (var item in xa)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi xã
                    await CreateBieu01TKKK_Huyen(item, huyenId, maHuyen);
                }
            }
            else
            {
                foreach (var item in xa)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi xã
                    await UpdateBieu01TKKK_Huyen(item, huyenId, maHuyen, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu01TKKK_Huyen(Bieu01TKKK_Xa xa, long huyenId, string maHuyen)
        {
            try
            {
                var objhuyen = new Bieu01TKKK_Huyen()
                {
                    STT = xa.STT,
                    LoaiDat = xa.LoaiDat,
                    Ma = xa.Ma,
                    TongDienTichDVHC = xa.TongDienTichDVHC,
                    TongSoTheoDoiTuongSuDung = xa.TongSoTheoDoiTuongSuDung,
                    CaNhanTrongNuoc_CNV = xa.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = xa.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = xa.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = xa.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = xa.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = xa.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = xa.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = xa.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = xa.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = xa.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = xa.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = xa.ToChucKinhTeVonNuocNgoai_TVN,
                    TongSoTheoDoiTuongDuocGiaoQuanLy = xa.TongSoTheoDoiTuongDuocGiaoQuanLy,
                    CoQuanNhaNuoc_TCQ = xa.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = xa.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = xa.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = xa.CongDongDanCu_CDQ,
                    HuyenId = huyenId,
                    MaHuyen = maHuyen,
                    Year = xa.Year,
                    Active = true,
                };
                await _bieu01TKKK_HuyenRepos.InsertAsync(objhuyen);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu01TKKK_Huyen(Bieu01TKKK_Xa xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            try
            {
                var objhuyen = await _bieu01TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.MaHuyen == maHuyen && x.Ma == xa.Ma && x.Year == year);
                if (objhuyen.Id > 0)
                {
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objhuyen.TongDienTichDVHC += xa.TongDienTichDVHC;
                        objhuyen.TongSoTheoDoiTuongSuDung += xa.TongSoTheoDoiTuongSuDung;
                        objhuyen.CaNhanTrongNuoc_CNV += xa.CaNhanTrongNuoc_CNV;
                        objhuyen.NguoiVietNamONuocNgoai_CNN += xa.NguoiVietNamONuocNgoai_CNN;
                        objhuyen.CoQuanNhaNuoc_TCN += xa.CoQuanNhaNuoc_TCN;
                        objhuyen.DonViSuNghiep_TSN += xa.DonViSuNghiep_TSN;
                        objhuyen.ToChucXaHoi_TXH += xa.ToChucXaHoi_TXH;
                        objhuyen.ToChucKinhTe_TKT += xa.ToChucKinhTe_TKT;
                        objhuyen.ToChucKhac_TKH += xa.ToChucKhac_TKH;
                        objhuyen.ToChucTonGiao_TTG += xa.ToChucTonGiao_TTG;
                        objhuyen.CongDongDanCu_CDS += xa.CongDongDanCu_CDS;
                        objhuyen.ToChucNuocNgoai_TNG += xa.ToChucNuocNgoai_TNG;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV += xa.NguoiGocVietNamONuocNgoai_NGV;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN += xa.ToChucKinhTeVonNuocNgoai_TVN;
                        objhuyen.TongSoTheoDoiTuongDuocGiaoQuanLy += xa.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objhuyen.CoQuanNhaNuoc_TCQ += xa.CoQuanNhaNuoc_TCQ;
                        objhuyen.DonViSuNghiep_TSQ += xa.DonViSuNghiep_TSQ;
                        objhuyen.ToChucKinhTe_KTQ += xa.ToChucKinhTe_KTQ;
                        objhuyen.CongDongDanCu_CDQ += xa.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objhuyen.TongDienTichDVHC -= xa.TongDienTichDVHC;
                        objhuyen.TongSoTheoDoiTuongSuDung -= xa.TongSoTheoDoiTuongSuDung;
                        objhuyen.CaNhanTrongNuoc_CNV -= xa.CaNhanTrongNuoc_CNV;
                        objhuyen.NguoiVietNamONuocNgoai_CNN -= xa.NguoiVietNamONuocNgoai_CNN;
                        objhuyen.CoQuanNhaNuoc_TCN -= xa.CoQuanNhaNuoc_TCN;
                        objhuyen.DonViSuNghiep_TSN -= xa.DonViSuNghiep_TSN;
                        objhuyen.ToChucXaHoi_TXH -= xa.ToChucXaHoi_TXH;
                        objhuyen.ToChucKinhTe_TKT -= xa.ToChucKinhTe_TKT;
                        objhuyen.ToChucKhac_TKH -= xa.ToChucKhac_TKH;
                        objhuyen.ToChucTonGiao_TTG -= xa.ToChucTonGiao_TTG;
                        objhuyen.CongDongDanCu_CDS -= xa.CongDongDanCu_CDS;
                        objhuyen.ToChucNuocNgoai_TNG -= xa.ToChucNuocNgoai_TNG;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV -= xa.NguoiGocVietNamONuocNgoai_NGV;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN -= xa.ToChucKinhTeVonNuocNgoai_TVN;
                        objhuyen.TongSoTheoDoiTuongDuocGiaoQuanLy -= xa.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objhuyen.CoQuanNhaNuoc_TCQ -= xa.CoQuanNhaNuoc_TCQ;
                        objhuyen.DonViSuNghiep_TSQ -= xa.DonViSuNghiep_TSQ;
                        objhuyen.ToChucKinhTe_KTQ -= xa.ToChucKinhTe_KTQ;
                        objhuyen.CongDongDanCu_CDQ -= xa.CongDongDanCu_CDQ;
                    }
                    await _bieu01TKKK_HuyenRepos.UpdateAsync(objhuyen);
                }
                else
                {
                    await CreateBieu01TKKK_Huyen(xa, huyenId, maHuyen);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 02TKKK
        private async Task CreateOrUpdateBieu02TKKK_Huyen(List<Bieu02TKKK_Xa> xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            var data_huyen = await _bieu02TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_huyen.Count == 0)
            {
                foreach (var item in xa)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi xã
                    await CreateBieu02TKKK_Huyen(item, huyenId, maHuyen);
                }
            }
            else
            {
                foreach (var item in xa)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi xã
                    await UpdateBieu02TKKK_Huyen(item, huyenId, maHuyen, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu02TKKK_Huyen(Bieu02TKKK_Xa xa, long huyenId, string maHuyen)
        {
            try
            {
                var objhuyen = new Bieu02TKKK_Huyen()
                {
                    STT = xa.STT,
                    LoaiDat = xa.LoaiDat,
                    Ma = xa.Ma,
                    TongSo = xa.TongSo,
                    CaNhanTrongNuoc_CNV = xa.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = xa.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = xa.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = xa.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = xa.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = xa.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = xa.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = xa.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = xa.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = xa.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = xa.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = xa.ToChucKinhTeVonNuocNgoai_TVN,
                    CoQuanNhaNuoc_TCQ = xa.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = xa.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = xa.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = xa.CongDongDanCu_CDQ,
                    HuyenId = huyenId,
                    MaHuyen = maHuyen,
                    Year = xa.Year,
                    Active = true,
                };
                await _bieu02TKKK_HuyenRepos.InsertAsync(objhuyen);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu02TKKK_Huyen(Bieu02TKKK_Xa xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            try
            {
                var objhuyen = await _bieu02TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.MaHuyen == maHuyen && x.Ma == xa.Ma && x.Year == year);
                if (objhuyen.Id > 0)
                {
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objhuyen.TongSo += xa.TongSo;
                        objhuyen.CaNhanTrongNuoc_CNV += xa.CaNhanTrongNuoc_CNV;
                        objhuyen.NguoiVietNamONuocNgoai_CNN += xa.NguoiVietNamONuocNgoai_CNN;
                        objhuyen.CoQuanNhaNuoc_TCN += xa.CoQuanNhaNuoc_TCN;
                        objhuyen.DonViSuNghiep_TSN += xa.DonViSuNghiep_TSN;
                        objhuyen.ToChucXaHoi_TXH += xa.ToChucXaHoi_TXH;
                        objhuyen.ToChucKinhTe_TKT += xa.ToChucKinhTe_TKT;
                        objhuyen.ToChucKhac_TKH += xa.ToChucKhac_TKH;
                        objhuyen.ToChucTonGiao_TTG += xa.ToChucTonGiao_TTG;
                        objhuyen.CongDongDanCu_CDS += xa.CongDongDanCu_CDS;
                        objhuyen.ToChucNuocNgoai_TNG += xa.ToChucNuocNgoai_TNG;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV += xa.NguoiGocVietNamONuocNgoai_NGV;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN += xa.ToChucKinhTeVonNuocNgoai_TVN;
                        objhuyen.CoQuanNhaNuoc_TCQ += xa.CoQuanNhaNuoc_TCQ;
                        objhuyen.DonViSuNghiep_TSQ += xa.DonViSuNghiep_TSQ;
                        objhuyen.ToChucKinhTe_KTQ += xa.ToChucKinhTe_KTQ;
                        objhuyen.CongDongDanCu_CDQ += xa.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objhuyen.TongSo -= xa.TongSo;
                        objhuyen.CaNhanTrongNuoc_CNV -= xa.CaNhanTrongNuoc_CNV;
                        objhuyen.NguoiVietNamONuocNgoai_CNN -= xa.NguoiVietNamONuocNgoai_CNN;
                        objhuyen.CoQuanNhaNuoc_TCN -= xa.CoQuanNhaNuoc_TCN;
                        objhuyen.DonViSuNghiep_TSN -= xa.DonViSuNghiep_TSN;
                        objhuyen.ToChucXaHoi_TXH -= xa.ToChucXaHoi_TXH;
                        objhuyen.ToChucKinhTe_TKT -= xa.ToChucKinhTe_TKT;
                        objhuyen.ToChucKhac_TKH -= xa.ToChucKhac_TKH;
                        objhuyen.ToChucTonGiao_TTG -= xa.ToChucTonGiao_TTG;
                        objhuyen.CongDongDanCu_CDS -= xa.CongDongDanCu_CDS;
                        objhuyen.ToChucNuocNgoai_TNG -= xa.ToChucNuocNgoai_TNG;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV -= xa.NguoiGocVietNamONuocNgoai_NGV;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN -= xa.ToChucKinhTeVonNuocNgoai_TVN;
                        objhuyen.CoQuanNhaNuoc_TCQ -= xa.CoQuanNhaNuoc_TCQ;
                        objhuyen.DonViSuNghiep_TSQ -= xa.DonViSuNghiep_TSQ;
                        objhuyen.ToChucKinhTe_KTQ -= xa.ToChucKinhTe_KTQ;
                        objhuyen.CongDongDanCu_CDQ -= xa.CongDongDanCu_CDQ;
                    }
                    await _bieu02TKKK_HuyenRepos.UpdateAsync(objhuyen);
                }
                else
                {
                    await CreateBieu02TKKK_Huyen(xa, huyenId, maHuyen);
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
