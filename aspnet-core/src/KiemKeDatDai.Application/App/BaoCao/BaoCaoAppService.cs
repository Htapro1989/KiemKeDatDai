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
using KiemKeDatDai.Authorization;
using System.Linq.Expressions;
using Abp.EntityFrameworkCore;
using KiemKeDatDai.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace KiemKeDatDai.RisApplication
{
    public class BaoCaoAppService : KiemKeDatDaiAppServiceBase, IBaoCaoAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
        private readonly IRepository<Bieu02TKKK_Xa, long> _bieu02TKKK_XaRepos;
        private readonly IRepository<Bieu04TKKK_Xa, long> _bieu04TKKK_XaRepos;
        private readonly IRepository<Bieu05TKKK_Xa, long> _bieu05TKKK_XaRepos;
        private readonly IRepository<Bieu01aKKNLT_Xa, long> _bieu01aKKNLT_XaRepos;
        private readonly IRepository<Bieu01bKKNLT_Xa, long> _bieu01bKKNLT_XaRepos;
        private readonly IRepository<Bieu01cKKNLT_Xa, long> _bieu01cKKNLT_XaRepos;
        private readonly IRepository<Bieu01KKSL_Xa, long> _bieu01KKSL_XaRepos;
        private readonly IRepository<Bieu02KKSL_Xa, long> _bieu02KKSL_XaRepos;
        private readonly IRepository<BieuPhuLucIII, long> _bieuPhuLucIIIRepos;
        private readonly IRepository<BieuPhuLucIV, long> _bieuPhuLucIVRepos;
        private readonly IRepository<KhoanhDat_KyTruoc, long> _khoanhDat_KyTruocRepos;
        private readonly IRepository<Data_Target, long> _data_TargetRepos;
        private readonly IRepository<SoLieuKyTruoc, long> _soLieuKyTruocRepos;
        private readonly IRepository<Data_Commune, long> _data_CommuneRepos;
        private readonly IRepository<Data_TangGiamKhac, long> _data_TangGiamKhacRepos;
        private readonly IRepository<Data_BienDong, long> _data_BienDongRepos;
        private readonly IRepository<File, long> _fileRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ICache mainCache;
        private readonly ILogsAppService _logsAppService;

        public BaoCaoAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,
            IRepository<Bieu02TKKK_Xa, long> bieu02TKKK_XaRepos,
            IRepository<Bieu04TKKK_Xa, long> bieu04TKKK_XaRepos,
            IRepository<Bieu05TKKK_Xa, long> bieu05TKKK_XaRepos,
            IRepository<Bieu01aKKNLT_Xa, long> bieu01aKKNLT_XaRepos,
            IRepository<Bieu01bKKNLT_Xa, long> bieu01bKKNLT_XaRepos,
            IRepository<Bieu01cKKNLT_Xa, long> bieu01cKKNLT_XaRepos,
            IRepository<Bieu01KKSL_Xa, long> bieu01KKSL_XaRepos,
            IRepository<Bieu02KKSL_Xa, long> bieu02KKSL_XaRepos,
            IRepository<BieuPhuLucIII, long> bieuPhuLucIIIRepos,
            IRepository<BieuPhuLucIV, long> bieuPhuLucIVRepos,
            IRepository<KhoanhDat_KyTruoc, long> khoanhDat_KyTruocRepos,
            IRepository<Data_Target, long> data_TargetRepos,
            IRepository<SoLieuKyTruoc, long> soLieuKyTruocRepos,
            IRepository<Data_Commune, long> data_CommuneRepos,
            IRepository<Data_TangGiamKhac, long> data_TangGiamKhacRepos,
            IRepository<Data_BienDong, long> data_BienDongRepos,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepos,
            IRepository<File, long> fileRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            ILogsAppService logsAppService,
            IConfiguration configuration
            )
        {
            _dvhcRepos = dvhcRepos;
            _dmKyThongKeKiemKeRepos = dmKyThongKeKiemKeRepos;
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
            _bieu02TKKK_XaRepos = bieu02TKKK_XaRepos;
            _bieu04TKKK_XaRepos = bieu04TKKK_XaRepos;
            _bieu05TKKK_XaRepos = bieu05TKKK_XaRepos;
            _bieu01cKKNLT_XaRepos = bieu01cKKNLT_XaRepos;
            _bieu01bKKNLT_XaRepos = bieu01bKKNLT_XaRepos;
            _bieu01aKKNLT_XaRepos = bieu01aKKNLT_XaRepos;
            _bieu01KKSL_XaRepos = bieu01KKSL_XaRepos;
            _bieu02KKSL_XaRepos = bieu02KKSL_XaRepos;
            _bieuPhuLucIIIRepos = bieuPhuLucIIIRepos;
            _bieuPhuLucIVRepos = bieuPhuLucIVRepos;
            _khoanhDat_KyTruocRepos = khoanhDat_KyTruocRepos;
            _data_TargetRepos = data_TargetRepos;
            _soLieuKyTruocRepos = soLieuKyTruocRepos;
            _data_CommuneRepos = data_CommuneRepos;
            _data_TangGiamKhacRepos = data_TangGiamKhacRepos;
            _data_BienDongRepos = data_BienDongRepos;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _fileRepos = fileRepos;
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:Default"];
            _logsAppService = logsAppService;
        }


        [AbpAuthorize(PermissionNames.Pages_Report_NopBaoCao)]
        public async Task<CommonResponseDto> NopBaoCao(long year, string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var allDvhc = await _dvhcRepos.GetAll().Where(x => x.Year == year).ToListAsync();
                    var curentDvhc = allDvhc.FirstOrDefault(x => x.Ma == ma);

                    if (curentDvhc != null)
                    {
                        if (curentDvhc.CapDVHCId == (int)CAP_DVHC.XA)
                        {
                            if (await CheckFileXaDayDuLieu(curentDvhc.Ma, year) == false)
                            {
                                commonResponseDto.Message = "Xã chưa đẩy dữ liệu. Vui lòng kiểm tra lại";
                                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }
                        }

                        var soDVHCCon = allDvhc.Count(x => x.Parent_Code == curentDvhc.Ma);
                        var soDVHCDaDuyet = allDvhc.Count(x => x.Parent_Code == curentDvhc.Ma && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);

                        //Kiểm tra xem đã duyệt hết các đvhc trực thuộc chưa
                        if (soDVHCDaDuyet < soDVHCCon && curentDvhc.CapDVHCId != 4)
                        {
                            commonResponseDto.Message = "Chưa duyệt hết các ĐVHC trực thuộc";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }

                        curentDvhc.NgayGui = DateTime.Now;
                        curentDvhc.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHO_DUYET;

                        await _dvhcRepos.UpdateAsync(curentDvhc);

                        //ghi log hệ thống
                        string description = "";
                        switch (curentDvhc.CapDVHCId)
                        {
                            case (int)CAP_DVHC.XA:
                                description = "Xã " + curentDvhc.TenXa + " nộp báo cáo";
                                break;
                            case (int)CAP_DVHC.HUYEN:
                                description = "Huyện " + curentDvhc.TenHuyen + " nộp báo cáo";
                                break;
                            case (int)CAP_DVHC.TINH:
                                description = "Tỉnh " + curentDvhc.TenTinh + " nộp báo cáo";
                                break;
                        }

                        var log = new LogsInputDto
                        {
                            UserId = currentUser.Id,
                            UserName = currentUser.UserName,
                            FullName = currentUser.FullName,
                            Action = (int)HANH_DONG.NOP,
                            Description = description,
                            Timestamp = DateTime.Now,
                        };

                        await _logsAppService.CreateOrUpdate(log);
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
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
            return commonResponseDto;
        }
        private async Task<bool> CheckFileXaDayDuLieu(string ma, long year)
        {
            var lstFileName = await _fileRepos
                .GetAll()
                .Where(x => x.MaDVHC == ma && x.Year == year && x.FileType == FILE_KYTHONGKE)
                .ToListAsync();

            if (lstFileName.Count > 0)
            {
                return true;
            }
            return false;
        }

        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> ThongKeSoLieu(long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                int tinh = (int)CAP_DVHC.TINH, huyen = (int)CAP_DVHC.HUYEN, xa = (int)CAP_DVHC.XA;
                int daDuyet = (int)TRANG_THAI_DUYET.DA_DUYET, choDuyet = (int)TRANG_THAI_DUYET.CHO_DUYET;
                var allDvhc = await _dvhcRepos.GetAll().Where(x => x.Year == year && x.Active == true).ToListAsync();
                var _thongke = new ThongKeSoLieuOutputDto();

                _thongke.TongSoTinh = allDvhc.Count(x => x.CapDVHCId == tinh);
                _thongke.TongSoTinhChoDuyet = allDvhc.Count(x => x.CapDVHCId == tinh && x.TrangThaiDuyet == choDuyet);
                _thongke.TongSoTinhHoanThanh = allDvhc.Count(x => x.CapDVHCId == tinh && x.TrangThaiDuyet == daDuyet);
                _thongke.TongSoHuyen = allDvhc.Count(x => x.CapDVHCId == huyen);
                _thongke.TongSoHuyenChoDuyet = allDvhc.Count(x => x.CapDVHCId == huyen && x.TrangThaiDuyet == choDuyet);
                _thongke.TongSoHuyenHoanThanh = allDvhc.Count(x => x.CapDVHCId == huyen && x.TrangThaiDuyet == daDuyet);
                _thongke.TongSoXa = allDvhc.Count(x => x.CapDVHCId == xa);
                _thongke.TongSoXaHoanThanh = allDvhc.Count(x => x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);

                DateTime fromDate = new DateTime(2024, 01, 01), toDate = DateTime.Now;
                var result = await ReportNumberXaByDate(fromDate, toDate);

                _thongke.TongSoXaDayDuLieu = (int)result.ReturnValue;

                var _listMaTinhMNPB = allDvhc.Where(x => x.CapDVHCId == tinh && x.MaVung == ((int)VUNG_MIEN.VUNG_MIEN_NUI_PHIA_BAC).ToString()).Select(x => x.Ma).ToList();
                var _listMaTinhDBSH = allDvhc.Where(x => x.CapDVHCId == tinh && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_BANG_SONG_HONG).ToString()).Select(x => x.Ma).ToList();
                var _listMaTinhDHMT = allDvhc.Where(x => x.CapDVHCId == tinh && x.MaVung == ((int)VUNG_MIEN.VUNG_DUYEN_HAI_MIEN_TRUNG).ToString()).Select(x => x.Ma).ToList();
                var _listMaTinhTN = allDvhc.Where(x => x.CapDVHCId == tinh && x.MaVung == ((int)VUNG_MIEN.VUNG_TAY_NGUYEN).ToString()).Select(x => x.Ma).ToList();
                var _listMaTinhDNBc = allDvhc.Where(x => x.CapDVHCId == tinh && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_NAM_BO).ToString()).Select(x => x.Ma).ToList();
                var _listMaTinhDBSCL = allDvhc.Where(x => x.CapDVHCId == tinh && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_BANG_SONG_CUU_LONG).ToString()).Select(x => x.Ma).ToList();

                _thongke.VungMienNuiPhiaBac = allDvhc.Count(x => _listMaTinhMNPB.Contains(x.MaTinh) && x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);
                _thongke.VungDongBangSongHong = allDvhc.Count(x => _listMaTinhDBSH.Contains(x.MaTinh) && x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);
                _thongke.VungDuyenHaiMienTrung = allDvhc.Count(x => _listMaTinhDHMT.Contains(x.MaTinh) && x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);
                _thongke.VungTayNguyen = allDvhc.Count(x => _listMaTinhTN.Contains(x.MaTinh) && x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);
                _thongke.VungDongNamBo = allDvhc.Count(x => _listMaTinhDNBc.Contains(x.MaTinh) && x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);
                _thongke.VungDongBangSongCuuLong = allDvhc.Count(x => _listMaTinhDBSCL.Contains(x.MaTinh) && x.CapDVHCId == xa && x.TrangThaiDuyet == daDuyet);

                _thongke.PhanTramVungMienNuiPhiaBac = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungMienNuiPhiaBac / _thongke.TongSoXa.Value * 100, 2) : 0;
                _thongke.PhanTramVungDongBangSongHong = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDongBangSongHong / _thongke.TongSoXa.Value * 100, 2) : 0;
                _thongke.PhanTramVungDuyenHaiMienTrung = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDuyenHaiMienTrung / _thongke.TongSoXa.Value * 100, 2) : 0;
                _thongke.PhanTramVungTayNguyen = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungTayNguyen / _thongke.TongSoXa.Value * 100, 2) : 0;
                _thongke.PhanTramVungDongNamBo = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDongNamBo / _thongke.TongSoXa.Value * 100, 2) : 0;
                _thongke.PhanTramVungDongBangSongCuuLong = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDongBangSongCuuLong / _thongke.TongSoXa.Value * 100, 2) : 0;
                _thongke.ChuaNopBaoCao = 100 - (_thongke.PhanTramVungMienNuiPhiaBac + _thongke.PhanTramVungDongBangSongHong + _thongke.PhanTramVungDuyenHaiMienTrung + _thongke.PhanTramVungTayNguyen + _thongke.PhanTramVungDongNamBo + _thongke.PhanTramVungDongBangSongCuuLong);

                commonResponseDto.ReturnValue = _thongke;
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Fatal(ex.Message);
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> DeleteAllDataXa(long year, string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentUser = await GetCurrentUserAsync();
                var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma && x.Year == year);

                if (objdata != null)
                {
                    if (objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET || objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                    {
                        commonResponseDto.Message = "ĐVHC này đang chờ duyệt hoặc đã duyệt, không thể xoá dữ liệu";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }

                    else
                    {
                        using (var con = new SqlConnection(_connectionString))
                        {
                            await con.ExecuteAsync(
                                "Delete_All_BieuXa", // Tên stored procedure
                                new { MaXa = ma, @Year = year },
                                commandType: CommandType.StoredProcedure // Chỉ định đây là stored procedure
                            );
                        }

                    }

                    //ghi log hệ thống
                    var log = new LogsInputDto
                    {
                        UserId = currentUser.Id,
                        UserName = currentUser.UserName,
                        FullName = currentUser.FullName,
                        Action = (int)HANH_DONG.XOA,
                        Description = "Xóa dữ liệu biểu xã",
                        Timestamp = DateTime.Now,
                    };

                    await _logsAppService.CreateOrUpdate(log);
                }
                else
                {
                    commonResponseDto.Message = "ĐVHC này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Fatal(ex.Message);
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> DeleteAllBieuHuyen(long year, string maHuyen)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentUser = await GetCurrentUserAsync();
                var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == maHuyen && x.Year == year);

                if (objdata != null)
                {
                    if (objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET || objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                    {
                        commonResponseDto.Message = "ĐVHC này đang chờ duyệt hoặc đã duyệt, không thể xoá dữ liệu";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }

                    else
                    {
                        using (var con = new SqlConnection(_connectionString))
                        {
                            await con.ExecuteAsync(
                                "Delete_All_BieuHuyen", // Tên stored procedure
                                new { @MaHuyen = maHuyen, @Year = year },
                                commandType: CommandType.StoredProcedure // Chỉ định đây là stored procedure
                            );
                        }

                    }

                    //ghi log hệ thống
                    var log = new LogsInputDto
                    {
                        UserId = currentUser.Id,
                        UserName = currentUser.UserName,
                        FullName = currentUser.FullName,
                        Action = (int)HANH_DONG.XOA,
                        Description = "Xóa dữ liệu biểu huyện",
                        Timestamp = DateTime.Now,
                    };

                    await _logsAppService.CreateOrUpdate(log);
                }
                else
                {
                    commonResponseDto.Message = "ĐVHC này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Xóa dữ liệu các biểu huyện thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Fatal(ex.Message);
            }
            return commonResponseDto;
        }

        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> ReportNumberXaByDate(DateTime fromDate, DateTime toDate)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                DateTime endDate = toDate.AddDays(1);
                DateTime startDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
                using (var con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryAsync<int>(
                        "ReportNumberXaByDate", // Tên stored procedure
                        new { @FromDate = startDate, @ToDate = endDate },
                        commandType: CommandType.StoredProcedure // Chỉ định đây là stored procedure
                    );
                    commonResponseDto.ReturnValue = result.ToArray()[0];
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Fatal(ex.Message);
            }
            return commonResponseDto;
        }
    }
}
