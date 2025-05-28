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
using Aspose.Cells;
using KiemKeDatDai.AppCore.Utility;
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai.RisApplication
{
    public class HuyenAppService : KiemKeDatDaiAppServiceBase, IHuyenAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;

        private readonly IRepository<Bieu02TKKK_Xa, long> _bieu02TKKK_XaRepos;
        private readonly IRepository<Bieu02TKKK_Huyen, long> _bieu02TKKK_HuyenRepos;

        private readonly IRepository<Bieu03TKKK_Huyen, long> _bieu03TKKK_HuyenRepos;

        private readonly IRepository<Bieu04TKKK_Xa, long> _bieu04TKKK_XaRepos;
        private readonly IRepository<Bieu04TKKK_Huyen, long> _bieu04TKKK_HuyenRepos;

        private readonly IRepository<Bieu05TKKK_Xa, long> _bieu05TKKK_XaRepos;
        private readonly IRepository<Bieu05TKKK_Huyen, long> _bieu05TKKK_HuyenRepos;

        private readonly IRepository<Bieu01KKSL_Xa, long> _bieu01KKSL_XaRepos;
        private readonly IRepository<Bieu01KKSL_Huyen, long> _bieu01KKSL_HuyenRepos;

        private readonly IRepository<Bieu02KKSL_Xa, long> _bieu02KKSL_XaRepos;
        private readonly IRepository<Bieu02KKSL_Huyen, long> _bieu02KKSL_HuyenRepos;

        private readonly IRepository<Bieu01aKKNLT_Xa, long> _bieu01aKKNLT_XaRepos;
        private readonly IRepository<Bieu01aKKNLT_Huyen, long> _bieu01aKKNLT_HuyenRepos;

        private readonly IRepository<Bieu01bKKNLT_Xa, long> _bieu01bKKNLT_XaRepos;
        private readonly IRepository<Bieu01bKKNLT_Huyen, long> _bieu01bKKNLT_HuyenRepos;

        private readonly IRepository<Bieu01cKKNLT_Xa, long> _bieu01cKKNLT_XaRepos;
        private readonly IRepository<Bieu01cKKNLT_Huyen, long> _bieu01cKKNLT_HuyenRepos;

        private readonly IRepository<BieuPhuLucIII, long> _bieuPhuLucIIIRepos;
        private readonly IRepository<BieuPhuLucIV, long> _bieuPhuLucIVRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IRepository<File, long> _fileRepos;
        private readonly IRepository<ConfigSystem, long> _configSystemRepos;
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

            IRepository<Bieu03TKKK_Huyen, long> bieu03TKKK_HuyenRepos,

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
            IRepository<File, long> fileRepos,
            IRepository<ConfigSystem, long> configSystemRepos,
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

            _bieu03TKKK_HuyenRepos = bieu03TKKK_HuyenRepos;

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
            _fileRepos = fileRepos;
            _configSystemRepos = configSystemRepos;
            _userRepos = userRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize(PermissionNames.Pages_Report_DuyetBaoCao)]
        public async Task<CommonResponseDto> DuyetBaoCaoXa(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var currentDvhc = await _dvhcRepos.FirstOrDefaultAsync(currentUser.DonViHanhChinhId.Value);

                    if (currentDvhc != null)
                    {
                        if (currentDvhc.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                        {
                            commonResponseDto.Message = "Huyện đã được duyệt, không thể duyệt xã";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var xa = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                            if (xa != null)
                            {
                                //Kiểm tra hệ thống có config yêu cầu file dgn k?
                                var currentConfigSystem = await _configSystemRepos.FirstOrDefaultAsync(x => x.Active == true);
                                if (currentConfigSystem != null)
                                {
                                    var jsonConfigSystem = JsonConvert.DeserializeObject<JsonConfigSytem>(currentConfigSystem.JsonConfigSystem);
                                    if (jsonConfigSystem.IsRequiredFileDGN == true)
                                    {
                                        var checkFileDgnReponse = await CheckFileDgn(ma);
                                        if (checkFileDgnReponse.IsCheck == false)
                                        {
                                            commonResponseDto.Message = xa.Name + checkFileDgnReponse.Message;
                                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                                            return commonResponseDto;
                                        }

                                    }
                                }
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuHuyen(currentDvhc, ma, year, (int)HAM_DUYET.DUYET);

                                #region cập nhật DVHC xã sau khi duyệt xã
                                xa.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                xa.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(xa);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Xã này không tồn tại";
                                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC huyện sau khi duyệt xã
                            if (currentDvhc.SoDVHCDaDuyet == null)
                            {
                                currentDvhc.SoDVHCDaDuyet = 1;
                            }
                            else
                            {
                                currentDvhc.SoDVHCDaDuyet++;
                            }
                            if (currentDvhc.SoDVHCCon == null)
                            {
                                currentDvhc.SoDVHCCon = await _dvhcRepos.CountAsync(x => x.Parent_id == currentUser.DonViHanhChinhId.Value);
                            }
                            await _dvhcRepos.UpdateAsync(currentDvhc);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Huyện này không tồn tại";
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
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var xa = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                            if (xa != null)
                            {
                                //gọi hàm update biểu huyện(trường hợp xã đã duyệt)
                                if (xa.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                                {
                                    commonResponseDto = await CreateOrUpdateBieuHuyen(objdata, ma, year, (int)HAM_DUYET.HUY);
                                }
                                #region cập nhật DVHC xã sau khi duyệt xã
                                xa.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHUA_GUI;
                                xa.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(xa);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Xã này không tồn tại";
                                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
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


        private async Task<CommonResponseDto> CreateOrUpdateBieuHuyen(DonViHanhChinh huyen, string maXa, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            #region biểu 01TKKK 03TKKK
            var data_bieu01TKKK = await _bieu01TKKK_XaRepos.GetAllListAsync(x => x.MaXa == maXa && x.Year == year);
            if (data_bieu01TKKK.Count > 0)
            {
                await CreateOrUpdateBieu01TKKK_Huyen(data_bieu01TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
                await CreateOrUpdateBieu03TKKK_Huyen(data_bieu01TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu xã biểu 01TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }
            #endregion

            #region biểu 02TKKK
            var data_bieu02TKKK = await _bieu02TKKK_XaRepos.GetAllListAsync(x => x.MaXa == maXa && x.Year == year);
            if (data_bieu02TKKK.Count > 0)
            {
                await CreateOrUpdateBieu02TKKK_Huyen(data_bieu02TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu xã biểu 02TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
            }
            #endregion

            #region biểu 04TKKK
            var data_bieu04TKKK = await _bieu04TKKK_XaRepos.GetAllListAsync(x => x.MaXa == maXa && x.Year == year);
            if (data_bieu04TKKK.Count > 0)
            {
                await CreateOrUpdateBieu04TKKK_Huyen(data_bieu04TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu xã biểu 04TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
            }
            #endregion

            #region biểu 05TKKK
            var data_bieu05TKKK = await _bieu05TKKK_XaRepos.GetAllListAsync(x => x.MaXa == maXa && x.Year == year);
            if (data_bieu05TKKK.Count > 0)
            {
                await CreateOrUpdateBieu05TKKK_Huyen(data_bieu05TKKK, huyen.Id, huyen.MaHuyen, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu xã biểu 05TKKK không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
            }
            #endregion

            commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
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
                    sequence = xa.sequence,
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
                        objhuyen.STT = xa.STT;
                        objhuyen.LoaiDat = xa.LoaiDat;
                        objhuyen.Year = xa.Year;
                        objhuyen.sequence = xa.sequence;
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
                    sequence = xa.sequence,
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
                        objhuyen.STT = xa.STT;
                        objhuyen.LoaiDat = xa.LoaiDat;
                        objhuyen.Year = xa.Year;
                        objhuyen.sequence = xa.sequence;
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

        #region Biểu 03TKKK
        private async Task CreateOrUpdateBieu03TKKK_Huyen(List<Bieu01TKKK_Xa> xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            var data_huyen = await _bieu03TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_huyen.Count == 0)
            {
                foreach (var item in xa)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi xã
                    await CreateBieu03TKKK_Huyen(item, huyenId, maHuyen);
                }
            }
            else
            {
                foreach (var item in xa)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi xã
                    await UpdateBieu03TKKK_Huyen(item, huyenId, maHuyen, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu03TKKK_Huyen(Bieu01TKKK_Xa xa, long huyenId, string maHuyen)
        {
            try
            {
                var bieu03Tkkk_xa = new DVHCBieu03TKKKDto
                {
                    TenDVHC = _dvhcRepos.Single(x => x.Ma == xa.MaXa).Name,
                    MaDVHC = xa.MaXa,
                    TenLoaiDat = xa.LoaiDat,
                    MaLoaiDat = xa.Ma,
                    DienTich = xa.TongDienTichDVHC,
                };
                var lstBieu03Tkkk_xa = new List<DVHCBieu03TKKKDto>();
                lstBieu03Tkkk_xa.Add(bieu03Tkkk_xa);
                var objhuyen = new Bieu03TKKK_Huyen()
                {
                    STT = xa.STT,
                    LoaiDat = xa.LoaiDat,
                    Ma = xa.Ma,
                    TongDienTich = xa.TongDienTichDVHC,
                    DienTichTheoDVHC = lstBieu03Tkkk_xa.ToJson(),
                    HuyenId = huyenId,
                    MaHuyen = maHuyen,
                    Year = xa.Year,
                    sequence = xa.sequence,
                    Active = true,
                };
                await _bieu03TKKK_HuyenRepos.InsertAsync(objhuyen);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu03TKKK_Huyen(Bieu01TKKK_Xa xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            try
            {
                var objhuyen = await _bieu03TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.MaHuyen == maHuyen && x.Ma == xa.Ma && x.Year == year);
                if (objhuyen.Id > 0)
                {
                    var dientichtheoDVHC = objhuyen.DienTichTheoDVHC.FromJson<List<DVHCBieu03TKKKDto>>();
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        var bieu03Tkkk_xa = new DVHCBieu03TKKKDto
                        {
                            TenDVHC = _dvhcRepos.Single(x => x.Ma == xa.MaXa).Name,
                            MaDVHC = xa.MaXa,
                            TenLoaiDat = xa.LoaiDat,
                            MaLoaiDat = xa.Ma,
                            DienTich = xa.TongDienTichDVHC,
                        };
                        objhuyen.TongDienTich += xa.TongDienTichDVHC;
                        objhuyen.STT = xa.STT;
                        objhuyen.LoaiDat = xa.LoaiDat;
                        objhuyen.Year = xa.Year;
                        objhuyen.sequence = xa.sequence;
                        dientichtheoDVHC.Add(bieu03Tkkk_xa);
                        objhuyen.DienTichTheoDVHC = dientichtheoDVHC.ToJson();
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objhuyen.TongDienTich -= xa.TongDienTichDVHC;
                        if (dientichtheoDVHC.FirstOrDefault(x => x.MaDVHC == xa.MaXa && x.MaLoaiDat == xa.Ma) != null)
                            dientichtheoDVHC.Remove(dientichtheoDVHC.FirstOrDefault(x => x.MaDVHC == xa.MaXa && x.MaLoaiDat == xa.Ma));
                        objhuyen.DienTichTheoDVHC = dientichtheoDVHC.ToJson();
                    }
                    await _bieu03TKKK_HuyenRepos.UpdateAsync(objhuyen);
                }
                else
                {
                    await CreateBieu03TKKK_Huyen(xa, huyenId, maHuyen);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 04TKKK
        private async Task CreateOrUpdateBieu04TKKK_Huyen(List<Bieu04TKKK_Xa> xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            var data_huyen = await _bieu04TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_huyen.Count == 0)
            {
                foreach (var item in xa)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi xã
                    await CreateBieu04TKKK_Huyen(item, huyenId, maHuyen);
                }
            }
            else
            {
                foreach (var item in xa)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi xã
                    await UpdateBieu04TKKK_Huyen(item, huyenId, maHuyen, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu04TKKK_Huyen(Bieu04TKKK_Xa xa, long huyenId, string maHuyen)
        {
            try
            {
                var objhuyen = new Bieu04TKKK_Huyen()
                {
                    STT = xa.STT,
                    LoaiDat = xa.LoaiDat,
                    Ma = xa.Ma,
                    TongSo_DT = xa.TongSo_DT,
                    TongSo_CC = xa.TongSo_CC,
                    CaNhanTrongNuoc_CNV_DT = xa.CaNhanTrongNuoc_CNV_DT,
                    CaNhanTrongNuoc_CNV_CC = xa.CaNhanTrongNuoc_CNV_CC,
                    NguoiVietNamONuocNgoai_CNN_DT = xa.NguoiVietNamONuocNgoai_CNN_DT,
                    NguoiVietNamONuocNgoai_CNN_CC = xa.NguoiVietNamONuocNgoai_CNN_CC,
                    CoQuanNhaNuoc_TCN_DT = xa.CoQuanNhaNuoc_TCN_DT,
                    CoQuanNhaNuoc_TCN_CC = xa.CoQuanNhaNuoc_TCN_CC,
                    DonViSuNghiep_TSN_DT = xa.DonViSuNghiep_TSN_DT,
                    DonViSuNghiep_TSN_CC = xa.DonViSuNghiep_TSN_CC,
                    ToChucXaHoi_TXH_DT = xa.ToChucXaHoi_TXH_DT,
                    ToChucXaHoi_TXH_CC = xa.ToChucXaHoi_TXH_CC,
                    ToChucKinhTe_TKT_DT = xa.ToChucKinhTe_TKT_DT,
                    ToChucKinhTe_TKT_CC = xa.ToChucKinhTe_TKT_CC,
                    ToChucKhac_TKH_DT = xa.ToChucKhac_TKH_DT,
                    ToChucKhac_TKH_CC = xa.ToChucKhac_TKH_CC,
                    ToChucTonGiao_TTG_DT = xa.ToChucTonGiao_TTG_DT,
                    ToChucTonGiao_TTG_CC = xa.ToChucTonGiao_TTG_CC,
                    CongDongDanCu_CDS_DT = xa.CongDongDanCu_CDS_DT,
                    CongDongDanCu_CDS_CC = xa.CongDongDanCu_CDS_CC,
                    ToChucNuocNgoai_TNG_DT = xa.ToChucNuocNgoai_TNG_DT,
                    ToChucNuocNgoai_TNG_CC = xa.ToChucNuocNgoai_TNG_CC,
                    NguoiGocVietNamONuocNgoai_NGV_DT = xa.NguoiGocVietNamONuocNgoai_NGV_DT,
                    NguoiGocVietNamONuocNgoai_NGV_CC = xa.NguoiGocVietNamONuocNgoai_NGV_CC,
                    ToChucKinhTeVonNuocNgoai_TVN_DT = xa.ToChucKinhTeVonNuocNgoai_TVN_DT,
                    ToChucKinhTeVonNuocNgoai_TVN_CC = xa.ToChucKinhTeVonNuocNgoai_TVN_CC,
                    CoQuanNhaNuoc_TCQ_DT = xa.CoQuanNhaNuoc_TCQ_DT,
                    CoQuanNhaNuoc_TCQ_CC = xa.CoQuanNhaNuoc_TCQ_CC,
                    DonViSuNghiep_TSQ_DT = xa.DonViSuNghiep_TSQ_DT,
                    DonViSuNghiep_TSQ_CC = xa.DonViSuNghiep_TSQ_CC,
                    ToChucKinhTe_KTQ_DT = xa.ToChucKinhTe_KTQ_DT,
                    ToChucKinhTe_KTQ_CC = xa.ToChucKinhTe_KTQ_CC,
                    CongDongDanCu_CDQ_DT = xa.CongDongDanCu_CDQ_DT,
                    CongDongDanCu_CDQ_CC = xa.CongDongDanCu_CDQ_CC,
                    HuyenId = huyenId,
                    MaHuyen = maHuyen,
                    Year = xa.Year,
                    sequence = xa.sequence,
                    Active = true,
                };
                await _bieu04TKKK_HuyenRepos.InsertAsync(objhuyen);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu04TKKK_Huyen(Bieu04TKKK_Xa xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            try
            {
                var objhuyen = await _bieu04TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.MaHuyen == maHuyen && x.Ma == xa.Ma && x.Year == year);
                var objhuyents = await _bieu04TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.MaHuyen == maHuyen && x.Ma == "TS" && x.Year == year);
                if (objhuyen.Id > 0)
                {
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        //objhuyen.TongSo_DT += xa.TongSo_DT;
                        //objhuyen.TongSo_CC += xa.TongSo_CC;
                        //objhuyen.CaNhanTrongNuoc_CNV_DT += xa.CaNhanTrongNuoc_CNV_DT;
                        //objhuyen.CaNhanTrongNuoc_CNV_CC += xa.CaNhanTrongNuoc_CNV_CC;
                        //objhuyen.NguoiVietNamONuocNgoai_CNN_DT += xa.NguoiVietNamONuocNgoai_CNN_DT;
                        //objhuyen.NguoiVietNamONuocNgoai_CNN_CC += xa.NguoiVietNamONuocNgoai_CNN_CC;
                        //objhuyen.CoQuanNhaNuoc_TCN_DT += xa.CoQuanNhaNuoc_TCN_DT;
                        //objhuyen.CoQuanNhaNuoc_TCN_CC += xa.CoQuanNhaNuoc_TCN_CC;
                        //objhuyen.DonViSuNghiep_TSN_DT += xa.DonViSuNghiep_TSN_DT;
                        //objhuyen.DonViSuNghiep_TSN_CC += xa.DonViSuNghiep_TSN_CC;
                        //objhuyen.ToChucXaHoi_TXH_DT += xa.ToChucXaHoi_TXH_DT;
                        //objhuyen.ToChucXaHoi_TXH_CC += xa.ToChucXaHoi_TXH_CC;
                        //objhuyen.ToChucKinhTe_TKT_DT += xa.ToChucKinhTe_TKT_DT;
                        //objhuyen.ToChucKinhTe_TKT_CC += xa.ToChucKinhTe_TKT_CC;
                        //objhuyen.ToChucKhac_TKH_DT += xa.ToChucKhac_TKH_DT;
                        //objhuyen.ToChucKhac_TKH_CC += xa.ToChucKhac_TKH_CC;
                        //objhuyen.ToChucTonGiao_TTG_DT += xa.ToChucTonGiao_TTG_DT;
                        //objhuyen.ToChucTonGiao_TTG_CC += xa.ToChucTonGiao_TTG_CC;
                        //objhuyen.CongDongDanCu_CDS_DT += xa.CongDongDanCu_CDS_DT;
                        //objhuyen.CongDongDanCu_CDS_CC += xa.CongDongDanCu_CDS_CC;
                        //objhuyen.ToChucNuocNgoai_TNG_DT += xa.ToChucNuocNgoai_TNG_DT;
                        //objhuyen.ToChucNuocNgoai_TNG_CC += xa.ToChucNuocNgoai_TNG_CC;
                        //objhuyen.NguoiGocVietNamONuocNgoai_NGV_DT += xa.NguoiGocVietNamONuocNgoai_NGV_DT;
                        //objhuyen.NguoiGocVietNamONuocNgoai_NGV_CC += xa.NguoiGocVietNamONuocNgoai_NGV_CC;
                        //objhuyen.ToChucKinhTeVonNuocNgoai_TVN_DT += xa.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        //objhuyen.ToChucKinhTeVonNuocNgoai_TVN_CC += xa.ToChucKinhTeVonNuocNgoai_TVN_CC;
                        //objhuyen.CoQuanNhaNuoc_TCQ_DT += xa.CoQuanNhaNuoc_TCQ_DT;
                        //objhuyen.CoQuanNhaNuoc_TCQ_CC += xa.CoQuanNhaNuoc_TCQ_CC;
                        //objhuyen.DonViSuNghiep_TSQ_DT += xa.DonViSuNghiep_TSQ_DT;
                        //objhuyen.DonViSuNghiep_TSQ_CC += xa.DonViSuNghiep_TSQ_CC;
                        //objhuyen.ToChucKinhTe_KTQ_DT += xa.ToChucKinhTe_KTQ_DT;
                        //objhuyen.ToChucKinhTe_KTQ_CC += xa.ToChucKinhTe_KTQ_CC;
                        //objhuyen.CongDongDanCu_CDQ_DT += xa.CongDongDanCu_CDQ_DT;
                        //objhuyen.CongDongDanCu_CDQ_CC += xa.CongDongDanCu_CDQ_CC;
                        objhuyen.TongSo_DT += xa.TongSo_DT;
                        objhuyen.TongSo_CC = Math.Round(objhuyen.TongSo_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CaNhanTrongNuoc_CNV_DT += xa.CaNhanTrongNuoc_CNV_DT;
                        objhuyen.CaNhanTrongNuoc_CNV_CC = Math.Round(objhuyen.CaNhanTrongNuoc_CNV_DT * 100 / objhuyents.TongSo_DT,4);
                        objhuyen.NguoiVietNamONuocNgoai_CNN_DT += xa.NguoiVietNamONuocNgoai_CNN_DT;
                        objhuyen.NguoiVietNamONuocNgoai_CNN_CC = Math.Round(objhuyen.NguoiVietNamONuocNgoai_CNN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CoQuanNhaNuoc_TCN_DT += xa.CoQuanNhaNuoc_TCN_DT;
                        objhuyen.CoQuanNhaNuoc_TCN_CC = Math.Round(objhuyen.CoQuanNhaNuoc_TCN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.DonViSuNghiep_TSN_DT += xa.DonViSuNghiep_TSN_DT;
                        objhuyen.DonViSuNghiep_TSN_CC = Math.Round(objhuyen.DonViSuNghiep_TSN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucXaHoi_TXH_DT += xa.ToChucXaHoi_TXH_DT;
                        objhuyen.ToChucXaHoi_TXH_CC = Math.Round(objhuyen.ToChucXaHoi_TXH_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKinhTe_TKT_DT += xa.ToChucKinhTe_TKT_DT;
                        objhuyen.ToChucKinhTe_TKT_CC = Math.Round(objhuyen.ToChucKinhTe_TKT_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKhac_TKH_DT += xa.ToChucKhac_TKH_DT;
                        objhuyen.ToChucKhac_TKH_CC = Math.Round(objhuyen.ToChucKhac_TKH_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucTonGiao_TTG_DT += xa.ToChucTonGiao_TTG_DT;
                        objhuyen.ToChucTonGiao_TTG_CC = Math.Round(objhuyen.ToChucTonGiao_TTG_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CongDongDanCu_CDS_DT += xa.CongDongDanCu_CDS_DT;
                        objhuyen.CongDongDanCu_CDS_CC = Math.Round(objhuyen.CongDongDanCu_CDS_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucNuocNgoai_TNG_DT += xa.ToChucNuocNgoai_TNG_DT;
                        objhuyen.ToChucNuocNgoai_TNG_CC = Math.Round(objhuyen.ToChucNuocNgoai_TNG_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV_DT += xa.NguoiGocVietNamONuocNgoai_NGV_DT;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV_CC = Math.Round(objhuyen.NguoiGocVietNamONuocNgoai_NGV_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN_DT += xa.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN_CC = Math.Round(objhuyen.ToChucKinhTeVonNuocNgoai_TVN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CoQuanNhaNuoc_TCQ_DT += xa.CoQuanNhaNuoc_TCQ_DT;
                        objhuyen.CoQuanNhaNuoc_TCQ_CC = Math.Round(objhuyen.CoQuanNhaNuoc_TCQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.DonViSuNghiep_TSQ_DT += xa.DonViSuNghiep_TSQ_DT;
                        objhuyen.DonViSuNghiep_TSQ_CC = Math.Round(objhuyen.DonViSuNghiep_TSQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKinhTe_KTQ_DT += xa.ToChucKinhTe_KTQ_DT;
                        objhuyen.ToChucKinhTe_KTQ_CC = Math.Round(objhuyen.ToChucKinhTe_KTQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CongDongDanCu_CDQ_DT += xa.CongDongDanCu_CDQ_DT;
                        objhuyen.CongDongDanCu_CDQ_CC = Math.Round(objhuyen.CongDongDanCu_CDQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.STT = xa.STT;
                        objhuyen.LoaiDat = xa.LoaiDat;
                        objhuyen.Year = xa.Year;
                        objhuyen.sequence = xa.sequence;
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objhuyen.TongSo_DT -= xa.TongSo_DT;
                        objhuyen.TongSo_CC = Math.Round(objhuyen.TongSo_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CaNhanTrongNuoc_CNV_DT -= xa.CaNhanTrongNuoc_CNV_DT;
                        objhuyen.CaNhanTrongNuoc_CNV_CC = Math.Round(objhuyen.CaNhanTrongNuoc_CNV_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.NguoiVietNamONuocNgoai_CNN_DT -= xa.NguoiVietNamONuocNgoai_CNN_DT;
                        objhuyen.NguoiVietNamONuocNgoai_CNN_CC = Math.Round(objhuyen.NguoiVietNamONuocNgoai_CNN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CoQuanNhaNuoc_TCN_DT -= xa.CoQuanNhaNuoc_TCN_DT;
                        objhuyen.CoQuanNhaNuoc_TCN_CC = Math.Round(objhuyen.CoQuanNhaNuoc_TCN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.DonViSuNghiep_TSN_DT -= xa.DonViSuNghiep_TSN_DT;
                        objhuyen.DonViSuNghiep_TSN_CC = Math.Round(objhuyen.DonViSuNghiep_TSN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucXaHoi_TXH_DT -= xa.ToChucXaHoi_TXH_DT;
                        objhuyen.ToChucXaHoi_TXH_CC = Math.Round(objhuyen.ToChucXaHoi_TXH_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKinhTe_TKT_DT -= xa.ToChucKinhTe_TKT_DT;
                        objhuyen.ToChucKinhTe_TKT_CC = Math.Round(objhuyen.ToChucKinhTe_TKT_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKhac_TKH_DT -= xa.ToChucKhac_TKH_DT;
                        objhuyen.ToChucKhac_TKH_CC = Math.Round(objhuyen.ToChucKhac_TKH_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucTonGiao_TTG_DT -= xa.ToChucTonGiao_TTG_DT;
                        objhuyen.ToChucTonGiao_TTG_CC = Math.Round(objhuyen.ToChucTonGiao_TTG_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CongDongDanCu_CDS_DT -= xa.CongDongDanCu_CDS_DT;
                        objhuyen.CongDongDanCu_CDS_CC = Math.Round(objhuyen.CongDongDanCu_CDS_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucNuocNgoai_TNG_DT -= xa.ToChucNuocNgoai_TNG_DT;
                        objhuyen.ToChucNuocNgoai_TNG_CC = Math.Round(objhuyen.ToChucNuocNgoai_TNG_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV_DT -= xa.NguoiGocVietNamONuocNgoai_NGV_DT;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV_CC = Math.Round(objhuyen.NguoiGocVietNamONuocNgoai_NGV_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN_DT -= xa.ToChucKinhTeVonNuocNgoai_TVN_DT;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN_CC = Math.Round(objhuyen.ToChucKinhTeVonNuocNgoai_TVN_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CoQuanNhaNuoc_TCQ_DT -= xa.CoQuanNhaNuoc_TCQ_DT;
                        objhuyen.CoQuanNhaNuoc_TCQ_CC = Math.Round(objhuyen.CoQuanNhaNuoc_TCQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.DonViSuNghiep_TSQ_DT -= xa.DonViSuNghiep_TSQ_DT;
                        objhuyen.DonViSuNghiep_TSQ_CC = Math.Round(objhuyen.DonViSuNghiep_TSQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.ToChucKinhTe_KTQ_DT -= xa.ToChucKinhTe_KTQ_DT;
                        objhuyen.ToChucKinhTe_KTQ_CC = Math.Round(objhuyen.ToChucKinhTe_KTQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                        objhuyen.CongDongDanCu_CDQ_DT -= xa.CongDongDanCu_CDQ_DT;
                        objhuyen.CongDongDanCu_CDQ_CC = Math.Round(objhuyen.CongDongDanCu_CDQ_DT * 100 / (objhuyents.TongSo_DT == 0 ? 1 : objhuyents.TongSo_DT), 4);
                    }
                    await _bieu04TKKK_HuyenRepos.UpdateAsync(objhuyen);
                }
                else
                {
                    await CreateBieu04TKKK_Huyen(xa, huyenId, maHuyen);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #region Biểu 05TKKK
        private async Task CreateOrUpdateBieu05TKKK_Huyen(List<Bieu05TKKK_Xa> xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            var data_huyen = await _bieu05TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_huyen.Count == 0)
            {
                foreach (var item in xa)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi xã
                    await CreateBieu05TKKK_Huyen(item, huyenId, maHuyen);
                }
            }
            else
            {
                foreach (var item in xa)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi xã
                    await UpdateBieu05TKKK_Huyen(item, huyenId, maHuyen, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu05TKKK_Huyen(Bieu05TKKK_Xa xa, long huyenId, string maHuyen)
        {
            try
            {
                var objhuyen = new Bieu05TKKK_Huyen()
                {
                    STT = xa.STT,
                    LoaiDat = xa.LoaiDat,
                    Ma = xa.Ma,
                    Nam = xa.Nam,
                    LUA = xa.LUA,
                    HNK = xa.HNK,
                    CLN = xa.CLN,
                    RDD = xa.RDD,
                    RPH = xa.RPH,
                    RSX = xa.RSX,
                    NTS = xa.NTS,
                    CNT = xa.CNT,
                    LMU = xa.LMU,
                    NKH = xa.NKH,
                    ONT = xa.ONT,
                    ODT = xa.ODT,
                    TSC = xa.TSC,
                    CQP = xa.CQP,
                    CAN = xa.CAN,
                    DVH = xa.DVH,
                    DXH = xa.DXH,
                    DYT = xa.DYT,
                    DGD = xa.DGD,
                    DTT = xa.DTT,
                    DKH = xa.DKH,
                    DMT = xa.DMT,
                    DKT = xa.DKT,
                    DNG = xa.DNG,
                    DSK = xa.DSK,
                    SKK = xa.SKK,
                    SKN = xa.SKN,
                    SCT = xa.SCT,
                    TMD = xa.TMD,
                    SKC = xa.SKC,
                    SKS = xa.SKS,
                    DGT = xa.DGT,
                    DTL = xa.DTL,
                    DCT = xa.DCT,
                    DPC = xa.DPC,
                    DDD = xa.DDD,
                    DRA = xa.DRA,
                    DNL = xa.DNL,
                    DBV = xa.DBV,
                    DCH = xa.DCH,
                    DKV = xa.DKV,
                    TON = xa.TON,
                    TIN = xa.TIN,
                    NTD = xa.NTD,
                    MNC = xa.MNC,
                    SON = xa.SON,
                    PNK = xa.PNK,
                    CGT = xa.CGT,
                    BCS = xa.BCS,
                    DCS = xa.DCS,
                    NCS = xa.NCS,
                    MCS = xa.MCS,
                    GiamKhac = xa.GiamKhac,
                    HuyenId = huyenId,
                    MaHuyen = maHuyen,
                    Year = xa.Year,
                    sequence = xa.sequence,
                    Active = true,
                };
                await _bieu05TKKK_HuyenRepos.InsertAsync(objhuyen);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private async Task UpdateBieu05TKKK_Huyen(Bieu05TKKK_Xa xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            try
            {
                var objhuyen = await _bieu05TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.MaHuyen == maHuyen && x.Ma == xa.Ma && x.Year == year);
                if (objhuyen.Id > 0)
                {
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objhuyen.Nam += xa.Nam;
                        objhuyen.LUA += xa.LUA;
                        objhuyen.HNK += xa.HNK;
                        objhuyen.CLN += xa.CLN;
                        objhuyen.RDD += xa.RDD;
                        objhuyen.RPH += xa.RPH;
                        objhuyen.RSX += xa.RSX;
                        objhuyen.NTS += xa.NTS;
                        objhuyen.CNT += xa.CNT;
                        objhuyen.LMU += xa.LMU;
                        objhuyen.NKH += xa.NKH;
                        objhuyen.ONT += xa.ONT;
                        objhuyen.ODT += xa.ODT;
                        objhuyen.TSC += xa.TSC;
                        objhuyen.CQP += xa.CQP;
                        objhuyen.CAN += xa.CAN;
                        objhuyen.DVH += xa.DVH;
                        objhuyen.DXH += xa.DXH;
                        objhuyen.DYT += xa.DYT;
                        objhuyen.DGD += xa.DGD;
                        objhuyen.DTT += xa.DTT;
                        objhuyen.DKH += xa.DKH;
                        objhuyen.DMT += xa.DMT;
                        objhuyen.DKT += xa.DKT;
                        objhuyen.DNG += xa.DNG;
                        objhuyen.DSK += xa.DSK;
                        objhuyen.SKK += xa.SKK;
                        objhuyen.SKN += xa.SKN;
                        objhuyen.SCT += xa.SCT;
                        objhuyen.TMD += xa.TMD;
                        objhuyen.SKC += xa.SKC;
                        objhuyen.SKS += xa.SKS;
                        objhuyen.DGT += xa.DGT;
                        objhuyen.DTL += xa.DTL;
                        objhuyen.DCT += xa.DCT;
                        objhuyen.DPC += xa.DPC;
                        objhuyen.DDD += xa.DDD;
                        objhuyen.DRA += xa.DRA;
                        objhuyen.DNL += xa.DNL;
                        objhuyen.DBV += xa.DBV;
                        objhuyen.DCH += xa.DCH;
                        objhuyen.DKV += xa.DKV;
                        objhuyen.TON += xa.TON;
                        objhuyen.TIN += xa.TIN;
                        objhuyen.NTD += xa.NTD;
                        objhuyen.MNC += xa.MNC;
                        objhuyen.SON += xa.SON;
                        objhuyen.PNK += xa.PNK;
                        objhuyen.CGT += xa.CGT;
                        objhuyen.BCS += xa.BCS;
                        objhuyen.DCS += xa.DCS;
                        objhuyen.NCS += xa.NCS;
                        objhuyen.MCS += xa.MCS;
                        objhuyen.GiamKhac += xa.GiamKhac;
                        objhuyen.STT = xa.STT;
                        objhuyen.LoaiDat = xa.LoaiDat;
                        objhuyen.Year = xa.Year;
                        objhuyen.sequence = xa.sequence;
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objhuyen.Nam -= xa.Nam;
                        objhuyen.LUA -= xa.LUA;
                        objhuyen.HNK -= xa.HNK;
                        objhuyen.CLN -= xa.CLN;
                        objhuyen.RDD -= xa.RDD;
                        objhuyen.RPH -= xa.RPH;
                        objhuyen.RSX -= xa.RSX;
                        objhuyen.NTS -= xa.NTS;
                        objhuyen.CNT -= xa.CNT;
                        objhuyen.LMU -= xa.LMU;
                        objhuyen.NKH -= xa.NKH;
                        objhuyen.ONT -= xa.ONT;
                        objhuyen.ODT -= xa.ODT;
                        objhuyen.TSC -= xa.TSC;
                        objhuyen.CQP -= xa.CQP;
                        objhuyen.CAN -= xa.CAN;
                        objhuyen.DVH -= xa.DVH;
                        objhuyen.DXH -= xa.DXH;
                        objhuyen.DYT -= xa.DYT;
                        objhuyen.DGD -= xa.DGD;
                        objhuyen.DTT -= xa.DTT;
                        objhuyen.DKH -= xa.DKH;
                        objhuyen.DMT -= xa.DMT;
                        objhuyen.DKT -= xa.DKT;
                        objhuyen.DNG -= xa.DNG;
                        objhuyen.DSK -= xa.DSK;
                        objhuyen.SKK -= xa.SKK;
                        objhuyen.SKN -= xa.SKN;
                        objhuyen.SCT -= xa.SCT;
                        objhuyen.TMD -= xa.TMD;
                        objhuyen.SKC -= xa.SKC;
                        objhuyen.SKS -= xa.SKS;
                        objhuyen.DGT -= xa.DGT;
                        objhuyen.DTL -= xa.DTL;
                        objhuyen.DCT -= xa.DCT;
                        objhuyen.DPC -= xa.DPC;
                        objhuyen.DDD -= xa.DDD;
                        objhuyen.DRA -= xa.DRA;
                        objhuyen.DNL -= xa.DNL;
                        objhuyen.DBV -= xa.DBV;
                        objhuyen.DCH -= xa.DCH;
                        objhuyen.DKV -= xa.DKV;
                        objhuyen.TON -= xa.TON;
                        objhuyen.TIN -= xa.TIN;
                        objhuyen.NTD -= xa.NTD;
                        objhuyen.MNC -= xa.MNC;
                        objhuyen.SON -= xa.SON;
                        objhuyen.PNK -= xa.PNK;
                        objhuyen.CGT -= xa.CGT;
                        objhuyen.BCS -= xa.BCS;
                        objhuyen.DCS -= xa.DCS;
                        objhuyen.NCS -= xa.NCS;
                        objhuyen.MCS -= xa.MCS;
                        objhuyen.GiamKhac -= xa.GiamKhac;
                    }
                    await _bieu05TKKK_HuyenRepos.UpdateAsync(objhuyen);
                }
                else
                {
                    await CreateBieu05TKKK_Huyen(xa, huyenId, maHuyen);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        private async Task<CheckFileDgnReponse> CheckFileDgn(string ma)
        {
            var checkFileDgnReponse = new CheckFileDgnReponse
            {
                IsCheck = false,
                Message = ""
            };

            var lstFileName = await _fileRepos.GetAll().Where(x => x.MaDVHC == ma).Select(x => x.FileName).ToListAsync();
            var userXa = await _userRepos.FirstOrDefaultAsync(x => x.DonViHanhChinhCode == ma);

            //kiểm tra tên file có giống định dạng BDHT_TenXa.dgn
            if (userXa != null)
            {
                string nameXa = "BDHT_" + (Utility.convertToUnSign3(userXa.Name)).Replace(" ", "") + ".dgn";

                if (lstFileName.Count > 0)
                {
                    foreach (var fileName in lstFileName)
                    {
                        if (nameXa.ToLower() == fileName.ToLower())
                        {
                            checkFileDgnReponse.IsCheck = true;

                            return checkFileDgnReponse;
                        }
                    }
                }

                checkFileDgnReponse.Message = " chưa nộp file " + nameXa + " theo quy định.";
            }

            return checkFileDgnReponse;
        }
    }
}
