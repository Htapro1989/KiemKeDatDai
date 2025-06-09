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
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai.RisApplication
{
    public class TrungUongAppService : KiemKeDatDaiAppServiceBase, ITrungUongAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Tinh, long> _bieu01TKKK_TinhRepos;
        private readonly IRepository<Bieu01TKKK_Vung, long> _bieu01TKKK_VungRepos;
        private readonly IRepository<Bieu01TKKK, long> _bieu01TKKKRepos;

        private readonly IRepository<Bieu02TKKK, long> _bieu02TKKKRepos;
        private readonly IRepository<Bieu02TKKK_Tinh, long> _bieu02TKKK_TinhRepos;
        private readonly IRepository<Bieu02TKKK_Vung, long> _bieu02TKKK_VungRepos;

        private readonly IRepository<Bieu03TKKK, long> _bieu03TKKKRepos;
        private readonly IRepository<Bieu03TKKK_Tinh, long> _bieu03TKKK_TinhRepos;
        private readonly IRepository<Bieu03TKKK_Vung, long> _bieu03TKKK_VungRepos;

        private readonly IRepository<Bieu04TKKK, long> _bieu04TKKKRepos;
        private readonly IRepository<Bieu04TKKK_Tinh, long> _bieu04TKKK_TinhRepos;
        private readonly IRepository<Bieu04TKKK_Vung, long> _bieu04TKKK_VungRepos;

        private readonly IRepository<Bieu05TKKK, long> _bieu05TKKKRepos;
        private readonly IRepository<Bieu05TKKK_Tinh, long> _bieu05TKKK_TinhRepos;
        private readonly IRepository<Bieu05TKKK_Vung, long> _bieu05TKKK_VungRepos;

        private readonly IRepository<Bieu06TKKKQPAN, long> _bieu06TKKKQPANRepos;
        private readonly IRepository<Bieu06TKKKQPAN_Tinh, long> _bieu06TKKKQPAN_TinhRepos;
        private readonly IRepository<Bieu06TKKKQPAN_Vung, long> _bieu06TKKKQPAN_VungRepos;

        private readonly IRepository<Bieu01KKSL, long> _bieu01KKSLRepos;
        private readonly IRepository<Bieu01KKSL_Tinh, long> _bieu01KKSL_TinhRepos;
        private readonly IRepository<Bieu01KKSL_Vung, long> _bieu01KKSL_VungRepos;

        private readonly IRepository<Bieu02KKSL, long> _bieu02KKSLRepos;
        private readonly IRepository<Bieu02KKSL_Tinh, long> _bieu02KKSL_TinhRepos;
        private readonly IRepository<Bieu02KKSL_Vung, long> _bieu02KKSL_VungRepos;

        private readonly IRepository<Bieu01aKKNLT, long> _bieu01aKKNLTRepos;
        private readonly IRepository<Bieu01aKKNLT_Tinh, long> _bieu01aKKNLT_TinhRepos;
        private readonly IRepository<Bieu01aKKNLT_Vung, long> _bieu01aKKNLT_VungRepos;

        private readonly IRepository<Bieu01bKKNLT, long> _bieu01bKKNLTRepos;
        private readonly IRepository<Bieu01bKKNLT_Tinh, long> _bieu01bKKNLT_TinhRepos;
        private readonly IRepository<Bieu01bKKNLT_Vung, long> _bieu01bKKNLT_VungRepos;

        private readonly IRepository<Bieu01cKKNLT, long> _bieu01cKKNLTRepos;
        private readonly IRepository<Bieu01cKKNLT_Tinh, long> _bieu01cKKNLT_TinhRepos;
        private readonly IRepository<Bieu01cKKNLT_Vung, long> _bieu01cKKNLT_VungRepos;
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

            IRepository<Bieu02TKKK, long> bieu02TKKKRepos,
            IRepository<Bieu02TKKK_Tinh, long> bieu02TKKK_TinhRepos,
            IRepository<Bieu02TKKK_Vung, long> bieu02TKKK_VungRepos,

            IRepository<Bieu03TKKK, long> bieu03TKKKRepos,
            IRepository<Bieu03TKKK_Tinh, long> bieu03TKKK_TinhRepos,
            IRepository<Bieu03TKKK_Vung, long> bieu03TKKK_VungRepos,

            IRepository<Bieu04TKKK, long> bieu04TKKKRepos,
            IRepository<Bieu04TKKK_Tinh, long> bieu04TKKK_TinhRepos,
            IRepository<Bieu04TKKK_Vung, long> bieu04TKKK_VungRepos,

            IRepository<Bieu05TKKK, long> bieu05TKKKRepos,
            IRepository<Bieu05TKKK_Tinh, long> bieu05TKKK_TinhRepos,
            IRepository<Bieu05TKKK_Vung, long> bieu05TKKK_VungRepos,

            IRepository<Bieu06TKKKQPAN, long> bieu06TKKKQPANRepos,
            IRepository<Bieu06TKKKQPAN_Tinh, long> bieu06TKKKQPAN_TinhRepos,
            IRepository<Bieu06TKKKQPAN_Vung, long> bieu06TKKKQPAN_VungRepos,

            IRepository<Bieu01KKSL, long> bieu01KKSLRepos,
            IRepository<Bieu01KKSL_Tinh, long> bieu01KKSL_TinhRepos,
            IRepository<Bieu01KKSL_Vung, long> bieu01KKSL_VungRepos,

            IRepository<Bieu02KKSL, long> bieu02KKSLRepos,
            IRepository<Bieu02KKSL_Tinh, long> bieu02KKSL_TinhRepos,
            IRepository<Bieu02KKSL_Vung, long> bieu02KKSL_VungRepos,

            IRepository<Bieu01aKKNLT, long> bieu01aKKNLTRepos,
            IRepository<Bieu01aKKNLT_Tinh, long> bieu01aKKNLT_TinhRepos,
            IRepository<Bieu01aKKNLT_Vung, long> bieu01aKKNLT_VungRepos,

            IRepository<Bieu01cKKNLT, long> bieu01cKKNLTRepos,
            IRepository<Bieu01cKKNLT_Tinh, long> bieu01cKKNLT_TinhRepos,
            IRepository<Bieu01cKKNLT_Vung, long> bieu01cKKNLT_VungRepos,

            IRepository<Bieu01bKKNLT, long> bieu01bKKNLTRepos,
            IRepository<Bieu01bKKNLT_Tinh, long> bieu01bKKNLT_TinhRepos,
            IRepository<Bieu01bKKNLT_Vung, long> bieu01bKKNLT_VungRepos,
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

            _bieu01TKKK_TinhRepos = bieu01TKKK_TinhRepos;
            _bieu01TKKK_VungRepos = bieu01TKKK_VungRepos;
            _bieu01TKKKRepos = bieu01TKKKRepos;

            _bieu02TKKKRepos = bieu02TKKKRepos;
            _bieu02TKKK_TinhRepos = bieu02TKKK_TinhRepos;
            _bieu02TKKK_VungRepos = bieu02TKKK_VungRepos;

            _bieu03TKKKRepos = bieu03TKKKRepos;
            _bieu03TKKK_TinhRepos = bieu03TKKK_TinhRepos;
            _bieu03TKKK_VungRepos = bieu03TKKK_VungRepos;

            _bieu04TKKKRepos = bieu04TKKKRepos;
            _bieu04TKKK_TinhRepos = bieu04TKKK_TinhRepos;
            _bieu04TKKK_VungRepos = bieu04TKKK_VungRepos;

            _bieu05TKKKRepos = bieu05TKKKRepos;
            _bieu05TKKK_TinhRepos = bieu05TKKK_TinhRepos;
            _bieu05TKKK_VungRepos = bieu05TKKK_VungRepos;

            _bieu06TKKKQPANRepos = bieu06TKKKQPANRepos;
            _bieu06TKKKQPAN_TinhRepos = bieu06TKKKQPAN_TinhRepos;
            _bieu06TKKKQPAN_VungRepos = bieu06TKKKQPAN_VungRepos;

            _bieu01KKSLRepos = bieu01KKSLRepos;
            _bieu01KKSL_TinhRepos = bieu01KKSL_TinhRepos;
            _bieu01KKSL_VungRepos = bieu01KKSL_VungRepos;

            _bieu02KKSLRepos = bieu02KKSLRepos;
            _bieu02KKSL_TinhRepos = bieu02KKSL_TinhRepos;
            _bieu02KKSL_VungRepos = bieu02KKSL_VungRepos;

            _bieu01aKKNLTRepos = bieu01aKKNLTRepos;
            _bieu01aKKNLT_TinhRepos = bieu01aKKNLT_TinhRepos;
            _bieu01aKKNLT_VungRepos = bieu01aKKNLT_VungRepos;

            _bieu01cKKNLTRepos = bieu01cKKNLTRepos;
            _bieu01cKKNLT_TinhRepos = bieu01cKKNLT_TinhRepos;
            _bieu01cKKNLT_VungRepos = bieu01cKKNLT_VungRepos;

            _bieu01bKKNLTRepos = bieu01bKKNLTRepos;
            _bieu01bKKNLT_TinhRepos = bieu01bKKNLT_TinhRepos;
            _bieu01bKKNLT_VungRepos = bieu01bKKNLT_VungRepos;

            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize(PermissionNames.Pages_Report_DuyetBaoCao)]
        public async Task<CommonResponseDto> DuyetBaoCaoTinh(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == currentUser.DonViHanhChinhCode);
                    if (objdata != null)
                    {
                        var tinh = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                        var vung = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == tinh.MaVung && x.Year == year && x.CapDVHCId == (int)CAP_DVHC.VUNG);
                        if (vung == null)
                        {
                            commonResponseDto.Message = "Tỉnh này không nằm trong vùng nào";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
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
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
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
                            objdata.SoDVHCCon = await _dvhcRepos.CountAsync(x => x.Parent_Code == currentUser.DonViHanhChinhCode);
                        }
                        await _dvhcRepos.UpdateAsync(objdata);
                        #endregion
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        [AbpAuthorize(PermissionNames.Pages_Report_HuyBaoCao)]
        public async Task<CommonResponseDto> HuyDuyetBaoCaoTinh(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == currentUser.DonViHanhChinhCode);
                    if (objdata != null)
                    {
                        var tinh = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                        var vung = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == tinh.MaVung && x.Year == year && x.CapDVHCId == (int)CAP_DVHC.VUNG);
                        if (tinh != null)
                        {
                            //gọi hàm update biểu tỉnh
                            if (tinh.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                            {
                                commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, vung.Id, vung.MaVung, year, (int)HAM_DUYET.HUY);
                            }

                            #region cập nhật DVHC tỉnh sau khi duyệt tinh
                            tinh.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHUA_GUI;
                            tinh.NgayDuyet = DateTime.Now;
                            await _dvhcRepos.UpdateAsync(tinh);
                            #endregion
                        }
                        else
                        {
                            commonResponseDto.Message = "Tỉnh này không tồn tại";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }

                        #region cập nhật DVHC TW sau khi duyệt tỉnh
                        objdata.SoDVHCDaDuyet--;
                        await _dvhcRepos.UpdateAsync(objdata);
                        #endregion
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        private async Task<CommonResponseDto> CreateOrUpdateBieuTinh(DonViHanhChinh tinh, string maTinh, long vungId, string maVung, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            #region biểu 01TKKK
            var data_bieu01TKKK = await _bieu01TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_bieu01TKKK.Count > 0)
            {
                await CreateOrUpdateBieu01TKKK_Tinh(data_bieu01TKKK, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 01TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            #region biểu 02TKKK
            var data_bieu02TKKK = await _bieu02TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_bieu02TKKK.Count > 0)
            {
                await CreateOrUpdateBieu02TKKK_Tinh(data_bieu02TKKK, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 02TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            #region biểu 03TKKK
            var data_bieu03TKKK = await _bieu03TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_bieu03TKKK.Count > 0)
            {
                await CreateOrUpdateBieu03TKKK_Tinh(data_bieu03TKKK, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 03TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            #region biểu 04TKKK
            var data_bieu04TKKK = await _bieu04TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_bieu04TKKK != null)
            {
                await CreateOrUpdateBieu04TKKK_Tinh(data_bieu04TKKK, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 04TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            #region biểu 05TKKK
            var data_bieu05TKKK = await _bieu05TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_bieu05TKKK.Count > 0)
            {
                await CreateOrUpdateBieu05TKKK_Tinh(data_bieu05TKKK, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 05TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            #region biểu 06TKKKQPAN
            var data_bieu06TKKKQPAN = await _bieu06TKKKQPAN_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_bieu06TKKKQPAN.Count > 0)
            {
                await CreateOrDeleteBieu06TKKKQPAN_Tinh(data_bieu06TKKKQPAN, vungId, maVung, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu tỉnh biểu 06TKKKQPAN không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
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
                    //Tạo các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await CreateBieu01TKKK_Tinh(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh)
                {
                    //Cập nhật các bản ghi trung ương tương ứng với bản ghi tỉnh
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
                    sequence = tinh.sequence,
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
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu01TKKK_VungRepos.InsertAsync(objVung);
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
                        objTW.STT = tinh.STT;
                        objTW.LoaiDat = tinh.LoaiDat;
                        objTW.Year = tinh.Year;
                        objTW.sequence = tinh.sequence;

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
                        objVung.STT = tinh.STT;
                        objVung.LoaiDat = tinh.LoaiDat;
                        objVung.Year = tinh.Year;
                        objVung.sequence = tinh.sequence;
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

        #region Biểu 02TKKK
        private async Task CreateOrUpdateBieu02TKKK_Tinh(List<Bieu02TKKK_Tinh> tinh, long vungId, string maVung, long year, int hamduyet)
        {
            var data_TW = await _bieu02TKKKRepos.GetAllListAsync(x => x.Year == year);
            if (data_TW.Count == 0)
            {
                foreach (var item in tinh)
                {
                    //Tạo các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await CreateBieu02TKKK_Tinh(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh)
                {
                    //Cập nhật các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await UpdateBieu02TKKK_Tinh(item, vungId, maVung, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu02TKKK_Tinh(Bieu02TKKK_Tinh tinh, long vungId, string maVung)
        {
            try
            {
                //Thêm mới bản ghi trung ương
                var objTW = new Bieu02TKKK()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongSo = tinh.TongSo,
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
                    CoQuanNhaNuoc_TCQ = tinh.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = tinh.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = tinh.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = tinh.CongDongDanCu_CDQ,
                    Year = tinh.Year,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu02TKKKRepos.InsertAsync(objTW);
                //Thêm mới bản ghi vùng
                var objVung = new Bieu02TKKK_Vung()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongSo = tinh.TongSo,
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
                    CoQuanNhaNuoc_TCQ = tinh.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = tinh.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = tinh.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = tinh.CongDongDanCu_CDQ,
                    Year = tinh.Year,
                    VungId = vungId,
                    MaVung = maVung,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu02TKKK_VungRepos.InsertAsync(objVung);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu02TKKK_Tinh(Bieu02TKKK_Tinh tinh, long vungId, string maVung, long year, int hamduyet)
        {
            try
            {
                var objTW = await _bieu02TKKKRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year);
                var objVung = await _bieu02TKKK_VungRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year && x.MaVung == maVung);
                if (objTW.Id > 0)
                {
                    //update duyệt tỉnh
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objTW.TongSo += tinh.TongSo;
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
                        objTW.CoQuanNhaNuoc_TCQ += tinh.CoQuanNhaNuoc_TCQ;
                        objTW.DonViSuNghiep_TSQ += tinh.DonViSuNghiep_TSQ;
                        objTW.ToChucKinhTe_KTQ += tinh.ToChucKinhTe_KTQ;
                        objTW.CongDongDanCu_CDQ += tinh.CongDongDanCu_CDQ;
                        objTW.STT = tinh.STT;
                        objTW.LoaiDat = tinh.LoaiDat;
                        objTW.Year = tinh.Year;
                        objTW.sequence = tinh.sequence;

                        objVung.TongSo += tinh.TongSo;
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
                        objVung.CoQuanNhaNuoc_TCQ += tinh.CoQuanNhaNuoc_TCQ;
                        objVung.DonViSuNghiep_TSQ += tinh.DonViSuNghiep_TSQ;
                        objVung.ToChucKinhTe_KTQ += tinh.ToChucKinhTe_KTQ;
                        objVung.CongDongDanCu_CDQ += tinh.CongDongDanCu_CDQ;
                        objVung.STT = tinh.STT;
                        objVung.LoaiDat = tinh.LoaiDat;
                        objVung.Year = tinh.Year;
                        objVung.sequence = tinh.sequence;
                    }
                    //update huỷ duyệt tỉnh
                    else
                    {
                        objTW.TongSo -= tinh.TongSo;
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
                        objTW.CoQuanNhaNuoc_TCQ -= tinh.CoQuanNhaNuoc_TCQ;
                        objTW.DonViSuNghiep_TSQ -= tinh.DonViSuNghiep_TSQ;
                        objTW.ToChucKinhTe_KTQ -= tinh.ToChucKinhTe_KTQ;
                        objTW.CongDongDanCu_CDQ -= tinh.CongDongDanCu_CDQ;

                        objVung.TongSo -= tinh.TongSo;
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
                        objVung.CoQuanNhaNuoc_TCQ -= tinh.CoQuanNhaNuoc_TCQ;
                        objVung.DonViSuNghiep_TSQ -= tinh.DonViSuNghiep_TSQ;
                        objVung.ToChucKinhTe_KTQ -= tinh.ToChucKinhTe_KTQ;
                        objVung.CongDongDanCu_CDQ -= tinh.CongDongDanCu_CDQ;
                    }
                    await _bieu02TKKKRepos.UpdateAsync(objTW);
                    await _bieu02TKKK_VungRepos.UpdateAsync(objVung);
                }
                else
                {
                    await CreateBieu02TKKK_Tinh(tinh, vungId, maVung);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 03TKKK
        private async Task CreateOrUpdateBieu03TKKK_Tinh(List<Bieu03TKKK_Tinh> tinh, long vungId, string maVung, long year, int hamduyet)
        {
            var data_TW = await _bieu03TKKKRepos.GetAllListAsync(x => x.Year == year);
            if (data_TW.Count == 0)
            {
                foreach (var item in tinh)
                {
                    //Tạo các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await CreateBieu03TKKK_Tinh(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh)
                {
                    //Cập nhật các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await UpdateBieu03TKKK_Tinh(item, vungId, maVung, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu03TKKK_Tinh(Bieu03TKKK_Tinh tinh, long vungId, string maVung)
        {
            try
            {
                var bieu03Tkkk_tinh = new DVHCBieu03TKKKDto
                {
                    TenDVHC = _dvhcRepos.Single(x => x.Ma == tinh.MaTinh).Name,
                    MaDVHC = tinh.MaTinh,
                    TenLoaiDat = tinh.LoaiDat,
                    MaLoaiDat = tinh.Ma,
                    DienTich = tinh.TongDienTich,
                };
                var lstBieu03Tkkk_tinh = new List<DVHCBieu03TKKKDto>();
                lstBieu03Tkkk_tinh.Add(bieu03Tkkk_tinh);
                //Thêm mới bản ghi trung ương
                var objTW = new Bieu03TKKK()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongDienTich = tinh.TongDienTich,
                    DienTichTheoDVHC = lstBieu03Tkkk_tinh.ToJson(),
                    Year = tinh.Year,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu03TKKKRepos.InsertAsync(objTW);
                //Thêm mới bản ghi vùng
                var objVung = new Bieu03TKKK_Vung()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongDienTich = tinh.TongDienTich,
                    DienTichTheoDVHC = lstBieu03Tkkk_tinh.ToJson(),
                    Year = tinh.Year,
                    VungId = vungId,
                    MaVung = maVung,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu03TKKK_VungRepos.InsertAsync(objVung);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu03TKKK_Tinh(Bieu03TKKK_Tinh tinh, long vungId, string maVung, long year, int hamduyet)
        {
            try
            {
                var objTW = await _bieu03TKKKRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year);
                var objVung = await _bieu03TKKK_VungRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year && x.MaVung == maVung);
                if (objTW.Id > 0)
                {
                    var dientichtheoDVHC_TW = objTW.DienTichTheoDVHC.FromJson<List<DVHCBieu03TKKKDto>>();
                    var dientichtheoDVHC_Vung = objVung.DienTichTheoDVHC.FromJson<List<DVHCBieu03TKKKDto>>();
                    //update duyệt tỉnh
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        var bieu03Tkkk_tinh = new DVHCBieu03TKKKDto
                        {
                            TenDVHC = _dvhcRepos.Single(x => x.Ma == tinh.MaTinh).Name,
                            MaDVHC = tinh.MaTinh,
                            TenLoaiDat = tinh.LoaiDat,
                            MaLoaiDat = tinh.Ma,
                            DienTich = tinh.TongDienTich,
                        };

                        objTW.TongDienTich += tinh.TongDienTich;
                        objTW.STT = tinh.STT;
                        objTW.LoaiDat = tinh.LoaiDat;
                        objTW.Year = tinh.Year;
                        objTW.sequence = tinh.sequence;
                        dientichtheoDVHC_TW.Add(bieu03Tkkk_tinh);
                        objTW.DienTichTheoDVHC = dientichtheoDVHC_TW.ToJson();

                        objVung.TongDienTich += tinh.TongDienTich;
                        objVung.STT = tinh.STT;
                        objVung.LoaiDat = tinh.LoaiDat;
                        objVung.Year = tinh.Year;
                        objVung.sequence = tinh.sequence;
                        dientichtheoDVHC_Vung.Add(bieu03Tkkk_tinh);
                        objVung.DienTichTheoDVHC = dientichtheoDVHC_Vung.ToJson();
                    }
                    //update huỷ duyệt tỉnh
                    else
                    {
                        objTW.TongDienTich -= tinh.TongDienTich;
                        if (dientichtheoDVHC_TW.FirstOrDefault(x => x.MaDVHC == tinh.MaTinh && x.MaLoaiDat == tinh.Ma) != null)
                            dientichtheoDVHC_TW.Remove(dientichtheoDVHC_TW.FirstOrDefault(x => x.MaDVHC == tinh.MaTinh && x.MaLoaiDat == tinh.Ma));
                        objTW.DienTichTheoDVHC = dientichtheoDVHC_TW.ToJson();

                        objVung.TongDienTich -= tinh.TongDienTich;
                        if (dientichtheoDVHC_Vung.FirstOrDefault(x => x.MaDVHC == tinh.MaTinh && x.MaLoaiDat == tinh.Ma) != null)
                            dientichtheoDVHC_Vung.Remove(dientichtheoDVHC_Vung.FirstOrDefault(x => x.MaDVHC == tinh.MaTinh && x.MaLoaiDat == tinh.Ma));
                        objVung.DienTichTheoDVHC = dientichtheoDVHC_Vung.ToJson();
                    }
                    await _bieu03TKKKRepos.UpdateAsync(objTW);
                    await _bieu03TKKK_VungRepos.UpdateAsync(objVung);
                }
                else
                {
                    await CreateBieu03TKKK_Tinh(tinh, vungId, maVung);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 04TKKK
        private async Task CreateOrUpdateBieu04TKKK_Tinh(List<Bieu04TKKK_Tinh> tinh, long vungId, string maVung, long year, int hamduyet)
        {
            var data_TW = await _bieu04TKKKRepos.GetAllListAsync(x => x.Year == year);
            if (data_TW.Count == 0)
            {
                foreach (var item in tinh.OrderBy(x => x.sequence))
                {
                    //Tạo các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await CreateBieu04TKKK_Tinh(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh.OrderBy(x => x.sequence))
                {
                    //Cập nhật các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await UpdateBieu04TKKK_Tinh(item, vungId, maVung, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu04TKKK_Tinh(Bieu04TKKK_Tinh tinh, long vungId, string maVung)
        {
            try
            {
                //Thêm mới bản ghi trung ương
                var objTW = new Bieu04TKKK()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongSo_DT = tinh.TongSo_DT,
                    TongSo_CC = tinh.TongSo_CC,
                    CaNhanTrongNuoc_CNV_DT = tinh.CaNhanTrongNuoc_CNV_DT,
                    CaNhanTrongNuoc_CNV_CC = tinh.CaNhanTrongNuoc_CNV_CC,
                    NguoiVietNamONuocNgoai_CNN_DT = tinh.NguoiVietNamONuocNgoai_CNN_DT,
                    NguoiVietNamONuocNgoai_CNN_CC = tinh.NguoiVietNamONuocNgoai_CNN_CC,
                    CoQuanNhaNuoc_TCN_DT = tinh.CoQuanNhaNuoc_TCN_DT,
                    CoQuanNhaNuoc_TCN_CC = tinh.CoQuanNhaNuoc_TCN_CC,
                    DonViSuNghiep_TSN_DT = tinh.NguoiVietNamONuocNgoai_CNN_DT,
                    DonViSuNghiep_TSN_CC = tinh.DonViSuNghiep_TSN_CC,
                    ToChucXaHoi_TXH_DT = tinh.ToChucXaHoi_TXH_DT,
                    ToChucXaHoi_TXH_CC = tinh.ToChucXaHoi_TXH_CC,
                    ToChucKinhTe_TKT_DT = tinh.ToChucKinhTe_TKT_DT,
                    ToChucKinhTe_TKT_CC = tinh.ToChucKinhTe_TKT_CC,
                    ToChucKhac_TKH_DT = tinh.ToChucKhac_TKH_DT,
                    ToChucKhac_TKH_CC = tinh.ToChucKhac_TKH_CC,
                    ToChucTonGiao_TTG_DT = tinh.ToChucTonGiao_TTG_DT,
                    ToChucTonGiao_TTG_CC = tinh.ToChucTonGiao_TTG_CC,
                    CongDongDanCu_CDS_DT = tinh.CongDongDanCu_CDS_DT,
                    CongDongDanCu_CDS_CC = tinh.CongDongDanCu_CDS_CC,
                    ToChucNuocNgoai_TNG_DT = tinh.ToChucNuocNgoai_TNG_DT,
                    ToChucNuocNgoai_TNG_CC = tinh.ToChucNuocNgoai_TNG_CC,
                    NguoiGocVietNamONuocNgoai_NGV_DT = tinh.NguoiGocVietNamONuocNgoai_NGV_DT,
                    NguoiGocVietNamONuocNgoai_NGV_CC = tinh.NguoiGocVietNamONuocNgoai_NGV_CC,
                    ToChucKinhTeVonNuocNgoai_TVN_DT = tinh.ToChucKinhTeVonNuocNgoai_TVN_DT,
                    ToChucKinhTeVonNuocNgoai_TVN_CC = tinh.ToChucKinhTeVonNuocNgoai_TVN_CC,
                    CoQuanNhaNuoc_TCQ_DT = tinh.CoQuanNhaNuoc_TCQ_DT,
                    CoQuanNhaNuoc_TCQ_CC = tinh.CoQuanNhaNuoc_TCQ_CC,
                    DonViSuNghiep_TSQ_DT = tinh.DonViSuNghiep_TSQ_DT,
                    DonViSuNghiep_TSQ_CC = tinh.DonViSuNghiep_TSQ_CC,
                    ToChucKinhTe_KTQ_DT = tinh.ToChucKinhTe_KTQ_DT,
                    ToChucKinhTe_KTQ_CC = tinh.ToChucKinhTe_KTQ_CC,
                    CongDongDanCu_CDQ_DT = tinh.CongDongDanCu_CDQ_DT,
                    CongDongDanCu_CDQ_CC = tinh.CongDongDanCu_CDQ_CC,
                    Year = tinh.Year,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu04TKKKRepos.InsertAsync(objTW);
                //Thêm mới bản ghi vùng
                var objVung = new Bieu04TKKK_Vung()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    TongSo_DT = tinh.TongSo_DT,
                    TongSo_CC = tinh.TongSo_CC,
                    CaNhanTrongNuoc_CNV_DT = tinh.CaNhanTrongNuoc_CNV_DT,
                    CaNhanTrongNuoc_CNV_CC = tinh.CaNhanTrongNuoc_CNV_CC,
                    NguoiVietNamONuocNgoai_CNN_DT = tinh.NguoiVietNamONuocNgoai_CNN_DT,
                    NguoiVietNamONuocNgoai_CNN_CC = tinh.NguoiVietNamONuocNgoai_CNN_CC,
                    CoQuanNhaNuoc_TCN_DT = tinh.CoQuanNhaNuoc_TCN_DT,
                    CoQuanNhaNuoc_TCN_CC = tinh.CoQuanNhaNuoc_TCN_CC,
                    DonViSuNghiep_TSN_DT = tinh.NguoiVietNamONuocNgoai_CNN_DT,
                    DonViSuNghiep_TSN_CC = tinh.DonViSuNghiep_TSN_CC,
                    ToChucXaHoi_TXH_DT = tinh.ToChucXaHoi_TXH_DT,
                    ToChucXaHoi_TXH_CC = tinh.ToChucXaHoi_TXH_CC,
                    ToChucKinhTe_TKT_DT = tinh.ToChucKinhTe_TKT_DT,
                    ToChucKinhTe_TKT_CC = tinh.ToChucKinhTe_TKT_CC,
                    ToChucKhac_TKH_DT = tinh.ToChucKhac_TKH_DT,
                    ToChucKhac_TKH_CC = tinh.ToChucKhac_TKH_CC,
                    ToChucTonGiao_TTG_DT = tinh.ToChucTonGiao_TTG_DT,
                    ToChucTonGiao_TTG_CC = tinh.ToChucTonGiao_TTG_CC,
                    CongDongDanCu_CDS_DT = tinh.CongDongDanCu_CDS_DT,
                    CongDongDanCu_CDS_CC = tinh.CongDongDanCu_CDS_CC,
                    ToChucNuocNgoai_TNG_DT = tinh.ToChucNuocNgoai_TNG_DT,
                    ToChucNuocNgoai_TNG_CC = tinh.ToChucNuocNgoai_TNG_CC,
                    NguoiGocVietNamONuocNgoai_NGV_DT = tinh.NguoiGocVietNamONuocNgoai_NGV_DT,
                    NguoiGocVietNamONuocNgoai_NGV_CC = tinh.NguoiGocVietNamONuocNgoai_NGV_CC,
                    ToChucKinhTeVonNuocNgoai_TVN_DT = tinh.ToChucKinhTeVonNuocNgoai_TVN_DT,
                    ToChucKinhTeVonNuocNgoai_TVN_CC = tinh.ToChucKinhTeVonNuocNgoai_TVN_CC,
                    CoQuanNhaNuoc_TCQ_DT = tinh.CoQuanNhaNuoc_TCQ_DT,
                    CoQuanNhaNuoc_TCQ_CC = tinh.CoQuanNhaNuoc_TCQ_CC,
                    DonViSuNghiep_TSQ_DT = tinh.DonViSuNghiep_TSQ_DT,
                    DonViSuNghiep_TSQ_CC = tinh.DonViSuNghiep_TSQ_CC,
                    ToChucKinhTe_KTQ_DT = tinh.ToChucKinhTe_KTQ_DT,
                    ToChucKinhTe_KTQ_CC = tinh.ToChucKinhTe_KTQ_CC,
                    CongDongDanCu_CDQ_DT = tinh.CongDongDanCu_CDQ_DT,
                    CongDongDanCu_CDQ_CC = tinh.CongDongDanCu_CDQ_CC,
                    Year = tinh.Year,
                    VungId = vungId,
                    MaVung = maVung,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu04TKKK_VungRepos.InsertAsync(objVung);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu04TKKK_Tinh(Bieu04TKKK_Tinh tinh, long vungId, string maVung, long year, int hamduyet)
        {
            try
            {
                var objTW = await _bieu04TKKKRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year);
                var objVung = await _bieu04TKKK_VungRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year && x.MaVung == maVung);
                var objTWts = await _bieu04TKKKRepos.FirstOrDefaultAsync(x => x.Ma == "TS" && x.Year == year);
                var objVungts = await _bieu04TKKK_VungRepos.FirstOrDefaultAsync(x => x.Ma == "TS" && x.Year == year && x.MaVung == maVung);
                if (objTW.Id > 0)
                {
                    //update duyệt tỉnh
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        //objTW.TongSo_DT += tinh.TongSo_DT;
                        //objTW.TongSo_CC += tinh.TongSo_CC;
                        //objTW.CaNhanTrongNuoc_CNV_DT += tinh.CaNhanTrongNuoc_CNV_DT;
                        //objTW.CaNhanTrongNuoc_CNV_CC += tinh.CaNhanTrongNuoc_CNV_CC;
                        //objTW.NguoiVietNamONuocNgoai_CNN_DT += tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        //objTW.NguoiVietNamONuocNgoai_CNN_CC += tinh.NguoiVietNamONuocNgoai_CNN_CC;
                        //objTW.CoQuanNhaNuoc_TCN_DT += tinh.CoQuanNhaNuoc_TCN_DT;
                        //objTW.CoQuanNhaNuoc_TCN_CC += tinh.CoQuanNhaNuoc_TCN_CC;
                        //objTW.DonViSuNghiep_TSN_DT += tinh.DonViSuNghiep_TSN_DT;
                        //objTW.DonViSuNghiep_TSN_CC += tinh.DonViSuNghiep_TSN_CC;
                        //objTW.ToChucXaHoi_TXH_DT += tinh.ToChucXaHoi_TXH_DT;
                        //objTW.ToChucXaHoi_TXH_CC += tinh.ToChucXaHoi_TXH_CC;
                        //objTW.ToChucKinhTe_TKT_DT += tinh.ToChucKinhTe_TKT_DT;
                        //objTW.ToChucKinhTe_TKT_CC += tinh.ToChucKinhTe_TKT_CC;
                        //objTW.ToChucKhac_TKH_DT += tinh.ToChucKhac_TKH_DT;
                        //objTW.ToChucKhac_TKH_CC += tinh.ToChucKhac_TKH_CC;
                        //objTW.ToChucTonGiao_TTG_DT += tinh.ToChucTonGiao_TTG_DT;
                        //objTW.ToChucTonGiao_TTG_CC += tinh.ToChucTonGiao_TTG_CC;
                        //objTW.CongDongDanCu_CDS_DT += tinh.CongDongDanCu_CDS_DT;
                        //objTW.CongDongDanCu_CDS_CC += tinh.CongDongDanCu_CDS_CC;
                        //objTW.ToChucNuocNgoai_TNG_DT += tinh.ToChucNuocNgoai_TNG_DT;
                        //objTW.ToChucNuocNgoai_TNG_CC += tinh.ToChucNuocNgoai_TNG_CC;
                        //objTW.NguoiGocVietNamONuocNgoai_NGV_DT += tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        //objTW.NguoiGocVietNamONuocNgoai_NGV_CC += tinh.NguoiGocVietNamONuocNgoai_NGV_CC;
                        //objTW.ToChucKinhTeVonNuocNgoai_TVN_DT += tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        //objTW.ToChucKinhTeVonNuocNgoai_TVN_CC += tinh.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        //objTW.CoQuanNhaNuoc_TCQ_DT += tinh.CoQuanNhaNuoc_TCQ_DT;
                        //objTW.CoQuanNhaNuoc_TCQ_CC += tinh.CoQuanNhaNuoc_TCQ_CC;
                        //objTW.DonViSuNghiep_TSQ_DT += tinh.DonViSuNghiep_TSQ_DT;
                        //objTW.DonViSuNghiep_TSQ_CC += tinh.DonViSuNghiep_TSQ_CC;
                        //objTW.ToChucKinhTe_KTQ_DT += tinh.ToChucKinhTe_KTQ_DT;
                        //objTW.ToChucKinhTe_KTQ_CC += tinh.ToChucKinhTe_KTQ_CC;
                        //objTW.CongDongDanCu_CDQ_DT += tinh.CongDongDanCu_CDQ_DT;
                        //objTW.CongDongDanCu_CDQ_CC += tinh.CongDongDanCu_CDQ_CC;
                        //objTW.STT = tinh.STT;
                        //objTW.LoaiDat = tinh.LoaiDat;
                        //objTW.Year = tinh.Year;
                        //objTW.sequence = tinh.sequence;

                        //objVung.TongSo_DT += tinh.TongSo_DT;
                        //objVung.TongSo_CC += tinh.TongSo_CC;
                        //objVung.CaNhanTrongNuoc_CNV_DT += tinh.CaNhanTrongNuoc_CNV_DT;
                        //objVung.CaNhanTrongNuoc_CNV_CC += tinh.CaNhanTrongNuoc_CNV_CC;
                        //objVung.NguoiVietNamONuocNgoai_CNN_DT += tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        //objVung.NguoiVietNamONuocNgoai_CNN_CC += tinh.NguoiVietNamONuocNgoai_CNN_CC;
                        //objVung.CoQuanNhaNuoc_TCN_DT += tinh.CoQuanNhaNuoc_TCN_DT;
                        //objVung.CoQuanNhaNuoc_TCN_CC += tinh.CoQuanNhaNuoc_TCN_CC;
                        //objVung.DonViSuNghiep_TSN_DT += tinh.DonViSuNghiep_TSN_DT;
                        //objVung.DonViSuNghiep_TSN_CC += tinh.DonViSuNghiep_TSN_CC;
                        //objVung.ToChucXaHoi_TXH_DT += tinh.ToChucXaHoi_TXH_DT;
                        //objVung.ToChucXaHoi_TXH_CC += tinh.ToChucXaHoi_TXH_CC;
                        //objVung.ToChucKinhTe_TKT_DT += tinh.ToChucKinhTe_TKT_DT;
                        //objVung.ToChucKinhTe_TKT_CC += tinh.ToChucKinhTe_TKT_CC;
                        //objVung.ToChucKhac_TKH_DT += tinh.ToChucKhac_TKH_DT;
                        //objVung.ToChucKhac_TKH_CC += tinh.ToChucKhac_TKH_CC;
                        //objVung.ToChucTonGiao_TTG_DT += tinh.ToChucTonGiao_TTG_DT;
                        //objVung.ToChucTonGiao_TTG_CC += tinh.ToChucTonGiao_TTG_CC;
                        //objVung.CongDongDanCu_CDS_DT += tinh.CongDongDanCu_CDS_DT;
                        //objVung.CongDongDanCu_CDS_CC += tinh.CongDongDanCu_CDS_CC;
                        //objVung.ToChucNuocNgoai_TNG_DT += tinh.ToChucNuocNgoai_TNG_DT;
                        //objVung.ToChucNuocNgoai_TNG_CC += tinh.ToChucNuocNgoai_TNG_CC;
                        //objVung.NguoiGocVietNamONuocNgoai_NGV_DT += tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        //objVung.NguoiGocVietNamONuocNgoai_NGV_CC += tinh.NguoiGocVietNamONuocNgoai_NGV_CC;
                        //objVung.ToChucKinhTeVonNuocNgoai_TVN_DT += tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        //objVung.ToChucKinhTeVonNuocNgoai_TVN_CC += tinh.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        //objVung.CoQuanNhaNuoc_TCQ_DT += tinh.CoQuanNhaNuoc_TCQ_DT;
                        //objVung.CoQuanNhaNuoc_TCQ_CC += tinh.CoQuanNhaNuoc_TCQ_CC;
                        //objVung.DonViSuNghiep_TSQ_DT += tinh.DonViSuNghiep_TSQ_DT;
                        //objVung.DonViSuNghiep_TSQ_CC += tinh.DonViSuNghiep_TSQ_CC;
                        //objVung.ToChucKinhTe_KTQ_DT += tinh.ToChucKinhTe_KTQ_DT;
                        //objVung.ToChucKinhTe_KTQ_CC += tinh.ToChucKinhTe_KTQ_CC;
                        //objVung.CongDongDanCu_CDQ_DT += tinh.CongDongDanCu_CDQ_DT;
                        //objVung.CongDongDanCu_CDQ_CC += tinh.CongDongDanCu_CDQ_CC;
                        //objVung.STT = tinh.STT;
                        //objVung.LoaiDat = tinh.LoaiDat;
                        //objVung.Year = tinh.Year;
                        //objVung.sequence = tinh.sequence;
                        objTW.TongSo_DT += tinh.TongSo_DT;
                        objTW.TongSo_CC = Math.Round(objTW.TongSo_DT * 100 / (objTWts.TongSo_DT == 0 ? 1 : objTWts.TongSo_DT), 4);
                        objTW.CaNhanTrongNuoc_CNV_DT += tinh.CaNhanTrongNuoc_CNV_DT;
                        objTW.CaNhanTrongNuoc_CNV_CC = Math.Round(objTW.CaNhanTrongNuoc_CNV_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.NguoiVietNamONuocNgoai_CNN_DT += tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        objTW.NguoiVietNamONuocNgoai_CNN_CC = Math.Round(objTW.NguoiVietNamONuocNgoai_CNN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CoQuanNhaNuoc_TCN_DT += tinh.CoQuanNhaNuoc_TCN_DT;
                        objTW.CoQuanNhaNuoc_TCN_CC = Math.Round(objTW.CoQuanNhaNuoc_TCN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.DonViSuNghiep_TSN_DT += tinh.DonViSuNghiep_TSN_DT;
                        objTW.DonViSuNghiep_TSN_CC = Math.Round(objTW.DonViSuNghiep_TSN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucXaHoi_TXH_DT += tinh.ToChucXaHoi_TXH_DT;
                        objTW.ToChucXaHoi_TXH_CC = Math.Round(objTW.ToChucXaHoi_TXH_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKinhTe_TKT_DT += tinh.ToChucKinhTe_TKT_DT;
                        objTW.ToChucKinhTe_TKT_CC = Math.Round(objTW.ToChucKinhTe_TKT_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKhac_TKH_DT += tinh.ToChucKhac_TKH_DT;
                        objTW.ToChucKhac_TKH_CC = Math.Round(objTW.ToChucKhac_TKH_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucTonGiao_TTG_DT += tinh.ToChucTonGiao_TTG_DT;
                        objTW.ToChucTonGiao_TTG_CC = Math.Round(objTW.ToChucTonGiao_TTG_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CongDongDanCu_CDS_DT += tinh.CongDongDanCu_CDS_DT;
                        objTW.CongDongDanCu_CDS_CC = Math.Round(objTW.CongDongDanCu_CDS_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucNuocNgoai_TNG_DT += tinh.ToChucNuocNgoai_TNG_DT;
                        objTW.ToChucNuocNgoai_TNG_CC = Math.Round(objTW.ToChucNuocNgoai_TNG_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.NguoiGocVietNamONuocNgoai_NGV_DT += tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        objTW.NguoiGocVietNamONuocNgoai_NGV_CC = Math.Round(objTW.NguoiGocVietNamONuocNgoai_NGV_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKinhTeVonNuocNgoai_TVN_DT += tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objTW.ToChucKinhTeVonNuocNgoai_TVN_CC = Math.Round(objTW.ToChucKinhTeVonNuocNgoai_TVN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CoQuanNhaNuoc_TCQ_DT += tinh.CoQuanNhaNuoc_TCQ_DT;
                        objTW.CoQuanNhaNuoc_TCQ_CC = Math.Round(objTW.CoQuanNhaNuoc_TCQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.DonViSuNghiep_TSQ_DT += tinh.DonViSuNghiep_TSQ_DT;
                        objTW.DonViSuNghiep_TSQ_CC = Math.Round(objTW.DonViSuNghiep_TSQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKinhTe_KTQ_DT += tinh.ToChucKinhTe_KTQ_DT;
                        objTW.ToChucKinhTe_KTQ_CC = Math.Round(objTW.ToChucKinhTe_KTQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CongDongDanCu_CDQ_DT += tinh.CongDongDanCu_CDQ_DT;
                        objTW.CongDongDanCu_CDQ_CC = Math.Round(objTW.CongDongDanCu_CDQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.STT = tinh.STT;
                        objTW.LoaiDat = tinh.LoaiDat;
                        objTW.Year = tinh.Year;
                        objTW.sequence = tinh.sequence;

                        objVung.TongSo_DT += tinh.TongSo_DT;
                        objVung.TongSo_CC = Math.Round(objVung.TongSo_DT * 100 / (objVungts.TongSo_DT == 0 ? 1 : objVungts.TongSo_DT), 4);
                        objVung.CaNhanTrongNuoc_CNV_DT += tinh.CaNhanTrongNuoc_CNV_DT;
                        objVung.CaNhanTrongNuoc_CNV_CC = Math.Round(objVung.CaNhanTrongNuoc_CNV_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.NguoiVietNamONuocNgoai_CNN_DT += tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        objVung.NguoiVietNamONuocNgoai_CNN_CC = Math.Round(objVung.NguoiVietNamONuocNgoai_CNN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CoQuanNhaNuoc_TCN_DT += tinh.CoQuanNhaNuoc_TCN_DT;
                        objVung.CoQuanNhaNuoc_TCN_CC = Math.Round(objVung.CoQuanNhaNuoc_TCN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.DonViSuNghiep_TSN_DT += tinh.DonViSuNghiep_TSN_DT;
                        objVung.DonViSuNghiep_TSN_CC = Math.Round(objVung.DonViSuNghiep_TSN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucXaHoi_TXH_DT += tinh.ToChucXaHoi_TXH_DT;
                        objVung.ToChucXaHoi_TXH_CC = Math.Round(objVung.ToChucXaHoi_TXH_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKinhTe_TKT_DT += tinh.ToChucKinhTe_TKT_DT;
                        objVung.ToChucKinhTe_TKT_CC = Math.Round(objVung.ToChucKinhTe_TKT_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKhac_TKH_DT += tinh.ToChucKhac_TKH_DT;
                        objVung.ToChucKhac_TKH_CC = Math.Round(objVung.ToChucKhac_TKH_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucTonGiao_TTG_DT += tinh.ToChucTonGiao_TTG_DT;
                        objVung.ToChucTonGiao_TTG_CC = Math.Round(objVung.ToChucTonGiao_TTG_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CongDongDanCu_CDS_DT += tinh.CongDongDanCu_CDS_DT;
                        objVung.CongDongDanCu_CDS_CC = Math.Round(objVung.CongDongDanCu_CDS_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucNuocNgoai_TNG_DT += tinh.ToChucNuocNgoai_TNG_DT;
                        objVung.ToChucNuocNgoai_TNG_CC = Math.Round(objVung.ToChucNuocNgoai_TNG_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.NguoiGocVietNamONuocNgoai_NGV_DT += tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        objVung.NguoiGocVietNamONuocNgoai_NGV_CC = Math.Round(objVung.NguoiGocVietNamONuocNgoai_NGV_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKinhTeVonNuocNgoai_TVN_DT += tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objVung.ToChucKinhTeVonNuocNgoai_TVN_CC = Math.Round(objVung.ToChucKinhTeVonNuocNgoai_TVN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CoQuanNhaNuoc_TCQ_DT += tinh.CoQuanNhaNuoc_TCQ_DT;
                        objVung.CoQuanNhaNuoc_TCQ_CC = Math.Round(objVung.CoQuanNhaNuoc_TCQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.DonViSuNghiep_TSQ_DT += tinh.DonViSuNghiep_TSQ_DT;
                        objVung.DonViSuNghiep_TSQ_CC = Math.Round(objVung.DonViSuNghiep_TSQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKinhTe_KTQ_DT += tinh.ToChucKinhTe_KTQ_DT;
                        objVung.ToChucKinhTe_KTQ_CC = Math.Round(objVung.ToChucKinhTe_KTQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CongDongDanCu_CDQ_DT += tinh.CongDongDanCu_CDQ_DT;
                        objVung.CongDongDanCu_CDQ_CC = Math.Round(objVung.CongDongDanCu_CDQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.STT = tinh.STT;
                        objVung.LoaiDat = tinh.LoaiDat;
                        objVung.Year = tinh.Year;
                        objVung.sequence = tinh.sequence;
                    }
                    //update huỷ duyệt tỉnh
                    else
                    {
                        //objTW.TongSo_DT -= tinh.TongSo_DT;
                        //objTW.TongSo_CC -= tinh.TongSo_CC;
                        //objTW.CaNhanTrongNuoc_CNV_DT -= tinh.CaNhanTrongNuoc_CNV_DT;
                        //objTW.CaNhanTrongNuoc_CNV_CC -= tinh.CaNhanTrongNuoc_CNV_CC;
                        //objTW.NguoiVietNamONuocNgoai_CNN_DT -= tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        //objTW.NguoiVietNamONuocNgoai_CNN_CC -= tinh.NguoiVietNamONuocNgoai_CNN_CC;
                        //objTW.CoQuanNhaNuoc_TCN_DT -= tinh.CoQuanNhaNuoc_TCN_DT;
                        //objTW.CoQuanNhaNuoc_TCN_CC -= tinh.CoQuanNhaNuoc_TCN_CC;
                        //objTW.DonViSuNghiep_TSN_DT -= tinh.DonViSuNghiep_TSN_DT;
                        //objTW.DonViSuNghiep_TSN_CC -= tinh.DonViSuNghiep_TSN_CC;
                        //objTW.ToChucXaHoi_TXH_DT -= tinh.ToChucXaHoi_TXH_DT;
                        //objTW.ToChucXaHoi_TXH_CC -= tinh.ToChucXaHoi_TXH_CC;
                        //objTW.ToChucKinhTe_TKT_DT -= tinh.ToChucKinhTe_TKT_DT;
                        //objTW.ToChucKinhTe_TKT_CC -= tinh.ToChucKinhTe_TKT_CC;
                        //objTW.ToChucKhac_TKH_DT -= tinh.ToChucKhac_TKH_DT;
                        //objTW.ToChucKhac_TKH_CC -= tinh.ToChucKhac_TKH_CC;
                        //objTW.ToChucTonGiao_TTG_DT -= tinh.ToChucTonGiao_TTG_DT;
                        //objTW.ToChucTonGiao_TTG_CC -= tinh.ToChucTonGiao_TTG_CC;
                        //objTW.CongDongDanCu_CDS_DT -= tinh.CongDongDanCu_CDS_DT;
                        //objTW.CongDongDanCu_CDS_CC -= tinh.CongDongDanCu_CDS_CC;
                        //objTW.ToChucNuocNgoai_TNG_DT -= tinh.ToChucNuocNgoai_TNG_DT;
                        //objTW.ToChucNuocNgoai_TNG_CC -= tinh.ToChucNuocNgoai_TNG_CC;
                        //objTW.NguoiGocVietNamONuocNgoai_NGV_DT -= tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        //objTW.NguoiGocVietNamONuocNgoai_NGV_CC -= tinh.NguoiGocVietNamONuocNgoai_NGV_CC;
                        //objTW.ToChucKinhTeVonNuocNgoai_TVN_DT -= tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        //objTW.ToChucKinhTeVonNuocNgoai_TVN_CC -= tinh.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        //objTW.CoQuanNhaNuoc_TCQ_DT -= tinh.CoQuanNhaNuoc_TCQ_DT;
                        //objTW.CoQuanNhaNuoc_TCQ_CC -= tinh.CoQuanNhaNuoc_TCQ_CC;
                        //objTW.DonViSuNghiep_TSQ_DT -= tinh.DonViSuNghiep_TSQ_DT;
                        //objTW.DonViSuNghiep_TSQ_CC -= tinh.DonViSuNghiep_TSQ_CC;
                        //objTW.ToChucKinhTe_KTQ_DT -= tinh.ToChucKinhTe_KTQ_DT;
                        //objTW.ToChucKinhTe_KTQ_CC -= tinh.ToChucKinhTe_KTQ_CC;
                        //objTW.CongDongDanCu_CDQ_DT -= tinh.CongDongDanCu_CDQ_DT;
                        //objTW.CongDongDanCu_CDQ_CC -= tinh.CongDongDanCu_CDQ_CC;

                        //objVung.TongSo_DT -= tinh.TongSo_DT;
                        //objVung.TongSo_CC -= tinh.TongSo_CC;
                        //objVung.CaNhanTrongNuoc_CNV_DT -= tinh.CaNhanTrongNuoc_CNV_DT;
                        //objVung.CaNhanTrongNuoc_CNV_CC -= tinh.CaNhanTrongNuoc_CNV_CC;
                        //objVung.NguoiVietNamONuocNgoai_CNN_DT -= tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        //objVung.NguoiVietNamONuocNgoai_CNN_CC -= tinh.NguoiVietNamONuocNgoai_CNN_CC;
                        //objVung.CoQuanNhaNuoc_TCN_DT -= tinh.CoQuanNhaNuoc_TCN_DT;
                        //objVung.CoQuanNhaNuoc_TCN_CC -= tinh.CoQuanNhaNuoc_TCN_CC;
                        //objVung.DonViSuNghiep_TSN_DT -= tinh.DonViSuNghiep_TSN_DT;
                        //objVung.DonViSuNghiep_TSN_CC -= tinh.DonViSuNghiep_TSN_CC;
                        //objVung.ToChucXaHoi_TXH_DT -= tinh.ToChucXaHoi_TXH_DT;
                        //objVung.ToChucXaHoi_TXH_CC -= tinh.ToChucXaHoi_TXH_CC;
                        //objVung.ToChucKinhTe_TKT_DT -= tinh.ToChucKinhTe_TKT_DT;
                        //objVung.ToChucKinhTe_TKT_CC -= tinh.ToChucKinhTe_TKT_CC;
                        //objVung.ToChucKhac_TKH_DT -= tinh.ToChucKhac_TKH_DT;
                        //objVung.ToChucKhac_TKH_CC -= tinh.ToChucKhac_TKH_CC;
                        //objVung.ToChucTonGiao_TTG_DT -= tinh.ToChucTonGiao_TTG_DT;
                        //objVung.ToChucTonGiao_TTG_CC -= tinh.ToChucTonGiao_TTG_CC;
                        //objVung.CongDongDanCu_CDS_DT -= tinh.CongDongDanCu_CDS_DT;
                        //objVung.CongDongDanCu_CDS_CC -= tinh.CongDongDanCu_CDS_CC;
                        //objVung.ToChucNuocNgoai_TNG_DT -= tinh.ToChucNuocNgoai_TNG_DT;
                        //objVung.ToChucNuocNgoai_TNG_CC -= tinh.ToChucNuocNgoai_TNG_CC;
                        //objVung.NguoiGocVietNamONuocNgoai_NGV_DT -= tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        //objVung.NguoiGocVietNamONuocNgoai_NGV_CC -= tinh.NguoiGocVietNamONuocNgoai_NGV_CC;
                        //objVung.ToChucKinhTeVonNuocNgoai_TVN_DT -= tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        //objVung.ToChucKinhTeVonNuocNgoai_TVN_CC -= tinh.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        //objVung.CoQuanNhaNuoc_TCQ_DT -= tinh.CoQuanNhaNuoc_TCQ_DT;
                        //objVung.CoQuanNhaNuoc_TCQ_CC -= tinh.CoQuanNhaNuoc_TCQ_CC;
                        //objVung.DonViSuNghiep_TSQ_DT -= tinh.DonViSuNghiep_TSQ_DT;
                        //objVung.DonViSuNghiep_TSQ_CC -= tinh.DonViSuNghiep_TSQ_CC;
                        //objVung.ToChucKinhTe_KTQ_DT -= tinh.ToChucKinhTe_KTQ_DT;
                        //objVung.ToChucKinhTe_KTQ_CC -= tinh.ToChucKinhTe_KTQ_CC;
                        //objVung.CongDongDanCu_CDQ_DT -= tinh.CongDongDanCu_CDQ_DT;
                        //objVung.CongDongDanCu_CDQ_CC -= tinh.CongDongDanCu_CDQ_CC;
                        objTW.TongSo_DT -= tinh.TongSo_DT;
                        objTW.TongSo_CC = Math.Round(objTW.TongSo_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CaNhanTrongNuoc_CNV_DT -= tinh.CaNhanTrongNuoc_CNV_DT;
                        objTW.CaNhanTrongNuoc_CNV_CC = Math.Round(objTW.CaNhanTrongNuoc_CNV_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.NguoiVietNamONuocNgoai_CNN_DT -= tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        objTW.NguoiVietNamONuocNgoai_CNN_CC = Math.Round(objTW.NguoiVietNamONuocNgoai_CNN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CoQuanNhaNuoc_TCN_DT -= tinh.CoQuanNhaNuoc_TCN_DT;
                        objTW.CoQuanNhaNuoc_TCN_CC = Math.Round(objTW.CoQuanNhaNuoc_TCN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.DonViSuNghiep_TSN_DT -= tinh.DonViSuNghiep_TSN_DT;
                        objTW.DonViSuNghiep_TSN_CC = Math.Round(objTW.DonViSuNghiep_TSN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucXaHoi_TXH_DT -= tinh.ToChucXaHoi_TXH_DT;
                        objTW.ToChucXaHoi_TXH_CC = Math.Round(objTW.ToChucXaHoi_TXH_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKinhTe_TKT_DT -= tinh.ToChucKinhTe_TKT_DT;
                        objTW.ToChucKinhTe_TKT_CC = Math.Round(objTW.ToChucKinhTe_TKT_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKhac_TKH_DT -= tinh.ToChucKhac_TKH_DT;
                        objTW.ToChucKhac_TKH_CC = Math.Round(objTW.ToChucKhac_TKH_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucTonGiao_TTG_DT -= tinh.ToChucTonGiao_TTG_DT;
                        objTW.ToChucTonGiao_TTG_CC = Math.Round(objTW.ToChucTonGiao_TTG_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CongDongDanCu_CDS_DT -= tinh.CongDongDanCu_CDS_DT;
                        objTW.CongDongDanCu_CDS_CC = Math.Round(objTW.CongDongDanCu_CDS_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucNuocNgoai_TNG_DT -= tinh.ToChucNuocNgoai_TNG_DT;
                        objTW.ToChucNuocNgoai_TNG_CC = Math.Round(objTW.ToChucNuocNgoai_TNG_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.NguoiGocVietNamONuocNgoai_NGV_DT -= tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        objTW.NguoiGocVietNamONuocNgoai_NGV_CC = Math.Round(objTW.NguoiGocVietNamONuocNgoai_NGV_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKinhTeVonNuocNgoai_TVN_DT -= tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objTW.ToChucKinhTeVonNuocNgoai_TVN_CC = Math.Round(objTW.ToChucKinhTeVonNuocNgoai_TVN_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CoQuanNhaNuoc_TCQ_DT -= tinh.CoQuanNhaNuoc_TCQ_DT;
                        objTW.CoQuanNhaNuoc_TCQ_CC = Math.Round(objTW.CoQuanNhaNuoc_TCQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.DonViSuNghiep_TSQ_DT -= tinh.DonViSuNghiep_TSQ_DT;
                        objTW.DonViSuNghiep_TSQ_CC = Math.Round(objTW.DonViSuNghiep_TSQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.ToChucKinhTe_KTQ_DT -= tinh.ToChucKinhTe_KTQ_DT;
                        objTW.ToChucKinhTe_KTQ_CC = Math.Round(objTW.ToChucKinhTe_KTQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);
                        objTW.CongDongDanCu_CDQ_DT -= tinh.CongDongDanCu_CDQ_DT;
                        objTW.CongDongDanCu_CDQ_CC = Math.Round(objTW.CongDongDanCu_CDQ_DT * 100 / (objTW.TongSo_DT == 0 ? 1 : objTW.TongSo_DT), 4);

                        objVung.TongSo_DT -= tinh.TongSo_DT;
                        objVung.TongSo_CC = Math.Round(objVung.TongSo_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CaNhanTrongNuoc_CNV_DT -= tinh.CaNhanTrongNuoc_CNV_DT;
                        objVung.CaNhanTrongNuoc_CNV_CC = Math.Round(objVung.CaNhanTrongNuoc_CNV_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.NguoiVietNamONuocNgoai_CNN_DT -= tinh.NguoiVietNamONuocNgoai_CNN_DT;
                        objVung.NguoiVietNamONuocNgoai_CNN_CC = Math.Round(objVung.NguoiVietNamONuocNgoai_CNN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CoQuanNhaNuoc_TCN_DT -= tinh.CoQuanNhaNuoc_TCN_DT;
                        objVung.CoQuanNhaNuoc_TCN_CC = Math.Round(objVung.CoQuanNhaNuoc_TCN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.DonViSuNghiep_TSN_DT -= tinh.DonViSuNghiep_TSN_DT;
                        objVung.DonViSuNghiep_TSN_CC = Math.Round(objVung.DonViSuNghiep_TSN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucXaHoi_TXH_DT -= tinh.ToChucXaHoi_TXH_DT;
                        objVung.ToChucXaHoi_TXH_CC = Math.Round(objVung.ToChucXaHoi_TXH_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKinhTe_TKT_DT -= tinh.ToChucKinhTe_TKT_DT;
                        objVung.ToChucKinhTe_TKT_CC = Math.Round(objVung.ToChucKinhTe_TKT_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKhac_TKH_DT -= tinh.ToChucKhac_TKH_DT;
                        objVung.ToChucKhac_TKH_CC = Math.Round(objVung.ToChucKhac_TKH_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucTonGiao_TTG_DT -= tinh.ToChucTonGiao_TTG_DT;
                        objVung.ToChucTonGiao_TTG_CC = Math.Round(objVung.ToChucTonGiao_TTG_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CongDongDanCu_CDS_DT -= tinh.CongDongDanCu_CDS_DT;
                        objVung.CongDongDanCu_CDS_CC = Math.Round(objVung.CongDongDanCu_CDS_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucNuocNgoai_TNG_DT -= tinh.ToChucNuocNgoai_TNG_DT;
                        objVung.ToChucNuocNgoai_TNG_CC = Math.Round(objVung.ToChucNuocNgoai_TNG_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.NguoiGocVietNamONuocNgoai_NGV_DT -= tinh.NguoiGocVietNamONuocNgoai_NGV_DT;
                        objVung.NguoiGocVietNamONuocNgoai_NGV_CC = Math.Round(objVung.NguoiGocVietNamONuocNgoai_NGV_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKinhTeVonNuocNgoai_TVN_DT -= tinh.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objVung.ToChucKinhTeVonNuocNgoai_TVN_CC = Math.Round(objVung.ToChucKinhTeVonNuocNgoai_TVN_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CoQuanNhaNuoc_TCQ_DT -= tinh.CoQuanNhaNuoc_TCQ_DT;
                        objVung.CoQuanNhaNuoc_TCQ_CC = Math.Round(objVung.CoQuanNhaNuoc_TCQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.DonViSuNghiep_TSQ_DT -= tinh.DonViSuNghiep_TSQ_DT;
                        objVung.DonViSuNghiep_TSQ_CC = Math.Round(objVung.DonViSuNghiep_TSQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.ToChucKinhTe_KTQ_DT -= tinh.ToChucKinhTe_KTQ_DT;
                        objVung.ToChucKinhTe_KTQ_CC = Math.Round(objVung.ToChucKinhTe_KTQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                        objVung.CongDongDanCu_CDQ_DT -= tinh.CongDongDanCu_CDQ_DT;
                        objVung.CongDongDanCu_CDQ_CC = Math.Round(objVung.CongDongDanCu_CDQ_DT * 100 / (objVung.TongSo_DT == 0 ? 1 : objVung.TongSo_DT), 4);
                    }
                        await _bieu04TKKKRepos.UpdateAsync(objTW);
                        await _bieu04TKKK_VungRepos.UpdateAsync(objVung);
                    }
                    else
                    {
                        await CreateBieu04TKKK_Tinh(tinh, vungId, maVung);
                    }
                }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 05TKKK
        private async Task CreateOrUpdateBieu05TKKK_Tinh(List<Bieu05TKKK_Tinh> tinh, long vungId, string maVung, long year, int hamduyet)
        {
            var data_TW = await _bieu05TKKKRepos.GetAllListAsync(x => x.Year == year);
            if (data_TW.Count == 0)
            {
                foreach (var item in tinh)
                {
                    //Tạo các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await CreateBieu05TKKK_Tinh(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh)
                {
                    //Cập nhật các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await UpdateBieu05TKKK_Tinh(item, vungId, maVung, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu05TKKK_Tinh(Bieu05TKKK_Tinh tinh, long vungId, string maVung)
        {
            try
            {
                //Thêm mới bản ghi trung ương
                var objTW = new Bieu05TKKK()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    Nam = tinh.Nam,
                    LUA = tinh.LUA,
                    HNK = tinh.HNK,
                    CLN = tinh.CLN,
                    RDD = tinh.RDD,
                    RPH = tinh.RPH,
                    RSX = tinh.RSX,
                    NTS = tinh.NTS,
                    CNT = tinh.CNT,
                    LMU = tinh.LMU,
                    NKH = tinh.NKH,
                    ONT = tinh.ONT,
                    ODT = tinh.ODT,
                    TSC = tinh.TSC,
                    CQP = tinh.CQP,
                    CAN = tinh.CAN,
                    DVH = tinh.DVH,
                    DXH = tinh.DXH,
                    DYT = tinh.DYT,
                    DGD = tinh.DGD,
                    DTT = tinh.DTT,
                    DKH = tinh.DKH,
                    DMT = tinh.DMT,
                    DKT = tinh.DKT,
                    DNG = tinh.DNG,
                    DSK = tinh.DSK,
                    SKK = tinh.SKK,
                    SKN = tinh.SKN,
                    SCT = tinh.SCT,
                    TMD = tinh.TMD,
                    SKC = tinh.SKC,
                    SKS = tinh.SKS,
                    DGT = tinh.DGT,
                    DTL = tinh.DTL,
                    DCT = tinh.DCT,
                    DPC = tinh.DPC,
                    DDD = tinh.DDD,
                    DRA = tinh.DRA,
                    DNL = tinh.DNL,
                    DBV = tinh.DBV,
                    DCH = tinh.DCH,
                    DKV = tinh.DKV,
                    TON = tinh.TON,
                    TIN = tinh.TIN,
                    NTD = tinh.NTD,
                    MNC = tinh.MNC,
                    SON = tinh.SON,
                    PNK = tinh.PNK,
                    CGT = tinh.CGT,
                    BCS = tinh.BCS,
                    DCS = tinh.DCS,
                    NCS = tinh.NCS,
                    MCS = tinh.MCS,
                    GiamKhac = tinh.GiamKhac,
                    Year = tinh.Year,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu05TKKKRepos.InsertAsync(objTW);
                //Thêm mới bản ghi vùng
                var objVung = new Bieu05TKKK_Vung()
                {
                    STT = tinh.STT,
                    LoaiDat = tinh.LoaiDat,
                    Ma = tinh.Ma,
                    Nam = tinh.Nam,
                    LUA = tinh.LUA,
                    HNK = tinh.HNK,
                    CLN = tinh.CLN,
                    RDD = tinh.RDD,
                    RPH = tinh.RPH,
                    RSX = tinh.RSX,
                    NTS = tinh.NTS,
                    CNT = tinh.CNT,
                    LMU = tinh.LMU,
                    NKH = tinh.NKH,
                    ONT = tinh.ONT,
                    ODT = tinh.ODT,
                    TSC = tinh.TSC,
                    CQP = tinh.CQP,
                    CAN = tinh.CAN,
                    DVH = tinh.DVH,
                    DXH = tinh.DXH,
                    DYT = tinh.DYT,
                    DGD = tinh.DGD,
                    DTT = tinh.DTT,
                    DKH = tinh.DKH,
                    DMT = tinh.DMT,
                    DKT = tinh.DKT,
                    DNG = tinh.DNG,
                    DSK = tinh.DSK,
                    SKK = tinh.SKK,
                    SKN = tinh.SKN,
                    SCT = tinh.SCT,
                    TMD = tinh.TMD,
                    SKC = tinh.SKC,
                    SKS = tinh.SKS,
                    DGT = tinh.DGT,
                    DTL = tinh.DTL,
                    DCT = tinh.DCT,
                    DPC = tinh.DPC,
                    DDD = tinh.DDD,
                    DRA = tinh.DRA,
                    DNL = tinh.DNL,
                    DBV = tinh.DBV,
                    DCH = tinh.DCH,
                    DKV = tinh.DKV,
                    TON = tinh.TON,
                    TIN = tinh.TIN,
                    NTD = tinh.NTD,
                    MNC = tinh.MNC,
                    SON = tinh.SON,
                    PNK = tinh.PNK,
                    CGT = tinh.CGT,
                    BCS = tinh.BCS,
                    DCS = tinh.DCS,
                    NCS = tinh.NCS,
                    MCS = tinh.MCS,
                    GiamKhac = tinh.GiamKhac,
                    Year = tinh.Year,
                    VungId = vungId,
                    MaVung = maVung,
                    sequence = tinh.sequence,
                    Active = true,
                };
                await _bieu05TKKK_VungRepos.InsertAsync(objVung);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu05TKKK_Tinh(Bieu05TKKK_Tinh tinh, long vungId, string maVung, long year, int hamduyet)
        {
            try
            {
                var objTW = await _bieu05TKKKRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year);
                var objVung = await _bieu05TKKK_VungRepos.FirstOrDefaultAsync(x => x.Ma == tinh.Ma && x.Year == year && x.MaVung == maVung);
                if (objTW.Id > 0)
                {
                    //update duyệt tỉnh
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objTW.Nam += tinh.Nam;
                        objTW.LUA += tinh.LUA;
                        objTW.HNK += tinh.HNK;
                        objTW.CLN += tinh.CLN;
                        objTW.RDD += tinh.RDD;
                        objTW.RPH += tinh.RPH;
                        objTW.RSX += tinh.RSX;
                        objTW.NTS += tinh.NTS;
                        objTW.CNT += tinh.CNT;
                        objTW.LMU += tinh.LMU;
                        objTW.NKH += tinh.NKH;
                        objTW.ONT += tinh.ONT;
                        objTW.ODT += tinh.ODT;
                        objTW.TSC += tinh.TSC;
                        objTW.CQP += tinh.CQP;
                        objTW.CAN += tinh.CAN;
                        objTW.DVH += tinh.DVH;
                        objTW.DXH += tinh.DXH;
                        objTW.DYT += tinh.DYT;
                        objTW.DGD += tinh.DGD;
                        objTW.DTT += tinh.DTT;
                        objTW.DKH += tinh.DKH;
                        objTW.DMT += tinh.DMT;
                        objTW.DKT += tinh.DKT;
                        objTW.DNG += tinh.DNG;
                        objTW.DSK += tinh.DSK;
                        objTW.SKK += tinh.SKK;
                        objTW.SKN += tinh.SKN;
                        objTW.SCT += tinh.SCT;
                        objTW.TMD += tinh.TMD;
                        objTW.SKC += tinh.SKC;
                        objTW.SKS += tinh.SKS;
                        objTW.DGT += tinh.DGT;
                        objTW.DTL += tinh.DTL;
                        objTW.DCT += tinh.DCT;
                        objTW.DPC += tinh.DPC;
                        objTW.DDD += tinh.DDD;
                        objTW.DRA += tinh.DRA;
                        objTW.DNL += tinh.DNL;
                        objTW.DBV += tinh.DBV;
                        objTW.DCH += tinh.DCH;
                        objTW.DKV += tinh.DKV;
                        objTW.TON += tinh.TON;
                        objTW.TIN += tinh.TIN;
                        objTW.NTD += tinh.NTD;
                        objTW.MNC += tinh.MNC;
                        objTW.SON += tinh.SON;
                        objTW.PNK += tinh.PNK;
                        objTW.CGT += tinh.CGT;
                        objTW.BCS += tinh.BCS;
                        objTW.DCS += tinh.DCS;
                        objTW.NCS += tinh.NCS;
                        objTW.MCS += tinh.MCS;
                        objTW.GiamKhac += tinh.GiamKhac;
                        objTW.STT = tinh.STT;
                        objTW.LoaiDat = tinh.LoaiDat;
                        objTW.Year = tinh.Year;
                        objTW.sequence = tinh.sequence;

                        objVung.Nam += tinh.Nam;
                        objVung.LUA += tinh.LUA;
                        objVung.HNK += tinh.HNK;
                        objVung.CLN += tinh.CLN;
                        objVung.RDD += tinh.RDD;
                        objVung.RPH += tinh.RPH;
                        objVung.RSX += tinh.RSX;
                        objVung.NTS += tinh.NTS;
                        objVung.CNT += tinh.CNT;
                        objVung.LMU += tinh.LMU;
                        objVung.NKH += tinh.NKH;
                        objVung.ONT += tinh.ONT;
                        objVung.ODT += tinh.ODT;
                        objVung.TSC += tinh.TSC;
                        objVung.CQP += tinh.CQP;
                        objVung.CAN += tinh.CAN;
                        objVung.DVH += tinh.DVH;
                        objVung.DXH += tinh.DXH;
                        objVung.DYT += tinh.DYT;
                        objVung.DGD += tinh.DGD;
                        objVung.DTT += tinh.DTT;
                        objVung.DKH += tinh.DKH;
                        objVung.DMT += tinh.DMT;
                        objVung.DKT += tinh.DKT;
                        objVung.DNG += tinh.DNG;
                        objVung.DSK += tinh.DSK;
                        objVung.SKK += tinh.SKK;
                        objVung.SKN += tinh.SKN;
                        objVung.SCT += tinh.SCT;
                        objVung.TMD += tinh.TMD;
                        objVung.SKC += tinh.SKC;
                        objVung.SKS += tinh.SKS;
                        objVung.DGT += tinh.DGT;
                        objVung.DTL += tinh.DTL;
                        objVung.DCT += tinh.DCT;
                        objVung.DPC += tinh.DPC;
                        objVung.DDD += tinh.DDD;
                        objVung.DRA += tinh.DRA;
                        objVung.DNL += tinh.DNL;
                        objVung.DBV += tinh.DBV;
                        objVung.DCH += tinh.DCH;
                        objVung.DKV += tinh.DKV;
                        objVung.TON += tinh.TON;
                        objVung.TIN += tinh.TIN;
                        objVung.NTD += tinh.NTD;
                        objVung.MNC += tinh.MNC;
                        objVung.SON += tinh.SON;
                        objVung.PNK += tinh.PNK;
                        objVung.CGT += tinh.CGT;
                        objVung.BCS += tinh.BCS;
                        objVung.DCS += tinh.DCS;
                        objVung.NCS += tinh.NCS;
                        objVung.MCS += tinh.MCS;
                        objVung.GiamKhac += tinh.GiamKhac;
                        objVung.STT = tinh.STT;
                        objVung.LoaiDat = tinh.LoaiDat;
                        objVung.Year = tinh.Year;
                        objVung.sequence = tinh.sequence;
                    }
                    //update huỷ duyệt tỉnh
                    else
                    {
                        objTW.Nam -= tinh.Nam;
                        objTW.LUA -= tinh.LUA;
                        objTW.HNK -= tinh.HNK;
                        objTW.CLN -= tinh.CLN;
                        objTW.RDD -= tinh.RDD;
                        objTW.RPH -= tinh.RPH;
                        objTW.RSX -= tinh.RSX;
                        objTW.NTS -= tinh.NTS;
                        objTW.CNT -= tinh.CNT;
                        objTW.LMU -= tinh.LMU;
                        objTW.NKH -= tinh.NKH;
                        objTW.ONT -= tinh.ONT;
                        objTW.ODT -= tinh.ODT;
                        objTW.TSC -= tinh.TSC;
                        objTW.CQP -= tinh.CQP;
                        objTW.CAN -= tinh.CAN;
                        objTW.DVH -= tinh.DVH;
                        objTW.DXH -= tinh.DXH;
                        objTW.DYT -= tinh.DYT;
                        objTW.DGD -= tinh.DGD;
                        objTW.DTT -= tinh.DTT;
                        objTW.DKH -= tinh.DKH;
                        objTW.DMT -= tinh.DMT;
                        objTW.DKT -= tinh.DKT;
                        objTW.DNG -= tinh.DNG;
                        objTW.DSK -= tinh.DSK;
                        objTW.SKK -= tinh.SKK;
                        objTW.SKN -= tinh.SKN;
                        objTW.SCT -= tinh.SCT;
                        objTW.TMD -= tinh.TMD;
                        objTW.SKC -= tinh.SKC;
                        objTW.SKS -= tinh.SKS;
                        objTW.DGT -= tinh.DGT;
                        objTW.DTL -= tinh.DTL;
                        objTW.DCT -= tinh.DCT;
                        objTW.DPC -= tinh.DPC;
                        objTW.DDD -= tinh.DDD;
                        objTW.DRA -= tinh.DRA;
                        objTW.DNL -= tinh.DNL;
                        objTW.DBV -= tinh.DBV;
                        objTW.DCH -= tinh.DCH;
                        objTW.DKV -= tinh.DKV;
                        objTW.TON -= tinh.TON;
                        objTW.TIN -= tinh.TIN;
                        objTW.NTD -= tinh.NTD;
                        objTW.MNC -= tinh.MNC;
                        objTW.SON -= tinh.SON;
                        objTW.PNK -= tinh.PNK;
                        objTW.CGT -= tinh.CGT;
                        objTW.BCS -= tinh.BCS;
                        objTW.DCS -= tinh.DCS;
                        objTW.NCS -= tinh.NCS;
                        objTW.MCS -= tinh.MCS;
                        objTW.GiamKhac -= tinh.GiamKhac;

                        objVung.Nam -= tinh.Nam;
                        objVung.LUA -= tinh.LUA;
                        objVung.HNK -= tinh.HNK;
                        objVung.CLN -= tinh.CLN;
                        objVung.RDD -= tinh.RDD;
                        objVung.RPH -= tinh.RPH;
                        objVung.RSX -= tinh.RSX;
                        objVung.NTS -= tinh.NTS;
                        objVung.CNT -= tinh.CNT;
                        objVung.LMU -= tinh.LMU;
                        objVung.NKH -= tinh.NKH;
                        objVung.ONT -= tinh.ONT;
                        objVung.ODT -= tinh.ODT;
                        objVung.TSC -= tinh.TSC;
                        objVung.CQP -= tinh.CQP;
                        objVung.CAN -= tinh.CAN;
                        objVung.DVH -= tinh.DVH;
                        objVung.DXH -= tinh.DXH;
                        objVung.DYT -= tinh.DYT;
                        objVung.DGD -= tinh.DGD;
                        objVung.DTT -= tinh.DTT;
                        objVung.DKH -= tinh.DKH;
                        objVung.DMT -= tinh.DMT;
                        objVung.DKT -= tinh.DKT;
                        objVung.DNG -= tinh.DNG;
                        objVung.DSK -= tinh.DSK;
                        objVung.SKK -= tinh.SKK;
                        objVung.SKN -= tinh.SKN;
                        objVung.SCT -= tinh.SCT;
                        objVung.TMD -= tinh.TMD;
                        objVung.SKC -= tinh.SKC;
                        objVung.SKS -= tinh.SKS;
                        objVung.DGT -= tinh.DGT;
                        objVung.DTL -= tinh.DTL;
                        objVung.DCT -= tinh.DCT;
                        objVung.DPC -= tinh.DPC;
                        objVung.DDD -= tinh.DDD;
                        objVung.DRA -= tinh.DRA;
                        objVung.DNL -= tinh.DNL;
                        objVung.DBV -= tinh.DBV;
                        objVung.DCH -= tinh.DCH;
                        objVung.DKV -= tinh.DKV;
                        objVung.TON -= tinh.TON;
                        objVung.TIN -= tinh.TIN;
                        objVung.NTD -= tinh.NTD;
                        objVung.MNC -= tinh.MNC;
                        objVung.SON -= tinh.SON;
                        objVung.PNK -= tinh.PNK;
                        objVung.CGT -= tinh.CGT;
                        objVung.BCS -= tinh.BCS;
                        objVung.DCS -= tinh.DCS;
                        objVung.NCS -= tinh.NCS;
                        objVung.MCS -= tinh.MCS;
                        objVung.GiamKhac -= tinh.GiamKhac;
                    }
                    await _bieu05TKKKRepos.UpdateAsync(objTW);
                    await _bieu05TKKK_VungRepos.UpdateAsync(objVung);
                }
                else
                {
                    await CreateBieu05TKKK_Tinh(tinh, vungId, maVung);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 06TKKK
        private async Task CreateOrDeleteBieu06TKKKQPAN_Tinh(List<Bieu06TKKKQPAN_Tinh> tinh, long vungId, string maVung, long year, int hamduyet)
        {
            var data_TW = await _bieu06TKKKQPANRepos.GetAllListAsync(x => x.Year == year);
            if (hamduyet == (int)HAM_DUYET.DUYET)
            {
                foreach (var item in tinh)
                {
                    //Tạo các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await CreateBieu06TKKK(item, vungId, maVung);
                }
            }
            else
            {
                foreach (var item in tinh)
                {
                    //Xoá các bản ghi trung ương tương ứng với bản ghi tỉnh
                    await DeleteBieu06TKKK(item, vungId, maVung, year);
                }
            }
        }

        private async Task CreateBieu06TKKK(Bieu06TKKKQPAN_Tinh tinh, long vungId, string maVung)
        {
            try
            {
                //Thêm mới bản ghi trung ương
                var objTW = new Bieu06TKKKQPAN()
                {
                    STT = tinh.STT,
                    DonVi = tinh.DonVi,
                    DiaChi = tinh.DiaChi,
                    DienTichDatQuocPhong = tinh.DienTichDatQuocPhong,
                    DienTichKetHopKhac = tinh.DienTichKetHopKhac,
                    LoaiDatKetHopKhac = tinh.LoaiDatKetHopKhac,
                    DienTichDaDoDac = tinh.DienTichDaDoDac,
                    SoGCNDaCap = tinh.SoGCNDaCap,
                    DienTichDaCapGCN = tinh.DienTichDaCapGCN,
                    GhiChu = tinh.GhiChu,
                    MaTinh = tinh.MaTinh,
                    TinhId = tinh.TinhId,
                    Year = tinh.Year,
                    Active = true,
                };
                await _bieu06TKKKQPANRepos.InsertAsync(objTW);
                //Thêm mới bản ghi vùng
                var objVung = new Bieu06TKKKQPAN_Vung()
                {
                    STT = tinh.STT,
                    DonVi = tinh.DonVi,
                    DiaChi = tinh.DiaChi,
                    DienTichDatQuocPhong = tinh.DienTichDatQuocPhong,
                    DienTichKetHopKhac = tinh.DienTichKetHopKhac,
                    LoaiDatKetHopKhac = tinh.LoaiDatKetHopKhac,
                    DienTichDaDoDac = tinh.DienTichDaDoDac,
                    SoGCNDaCap = tinh.SoGCNDaCap,
                    DienTichDaCapGCN = tinh.DienTichDaCapGCN,
                    GhiChu = tinh.GhiChu,
                    MaTinh = tinh.MaTinh,
                    TinhId = tinh.TinhId,
                    Year = tinh.Year,
                    VungId = vungId,
                    MaVung = maVung,
                    Active = true,
                };
                await _bieu06TKKKQPAN_VungRepos.InsertAsync(objVung);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task DeleteBieu06TKKK(Bieu06TKKKQPAN_Tinh tinh, long vungId, string maVung, long year)
        {
            try
            {
                //xoá biểu trung ương
                var lstbieu06TKKKQPAN = await _bieu06TKKKQPANRepos.GetAllListAsync(x => x.MaTinh == tinh.MaTinh && x.Year == year);
                if (lstbieu06TKKKQPAN.Count > 0)
                {
                    foreach (var item in lstbieu06TKKKQPAN)
                    {
                        await _bieu06TKKKQPANRepos.DeleteAsync(item);
                    }
                }
                //xoá biểu vùng
                var lstbieu06TKKKQPAN_vung = await _bieu06TKKKQPAN_VungRepos.GetAllListAsync(x => x.MaVung == maVung && x.Year == year);
                if (lstbieu06TKKKQPAN_vung.Count > 0)
                {
                    foreach (var vung in lstbieu06TKKKQPAN_vung)
                    {
                        await _bieu06TKKKQPAN_VungRepos.DeleteAsync(vung);
                    }
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
