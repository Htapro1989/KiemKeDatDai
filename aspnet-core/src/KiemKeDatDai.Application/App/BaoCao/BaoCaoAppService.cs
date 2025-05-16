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
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ICache mainCache;

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
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
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
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:Default"];
        }


        [AbpAuthorize(PermissionNames.Pages_Report_NopBaoCao)]
        public async Task<CommonResponseDto> NopBaoCao(long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == currentUser.DonViHanhChinhCode && x.Year == year);

                    if (objdata != null)
                    {
                        if (objdata.SoDVHCDaDuyet < objdata.SoDVHCCon && objdata.CapDVHCId != 4)
                        {
                            commonResponseDto.Message = "Chưa duyệt hết các ĐVHC trực thuộc";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }

                        objdata.NgayGui = DateTime.Now;
                        objdata.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHO_DUYET;

                        await _dvhcRepos.UpdateAsync(objdata);
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

        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> ThongKeSoLieu()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var _thongke = new ThongKeSoLieuOutputDto();
                    var year = 2024;
                    _thongke.TongSoTinh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.Year == year);
                    _thongke.TongSoTinhHoanThanh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.Year == year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.TongSoHuyen = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.HUYEN && x.Year == year);
                    _thongke.TongSoHuyenHoanThanh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.HUYEN && x.Year == year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.TongSoXa = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.Year == year);
                    _thongke.TongSoXaHoanThanh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.Year == year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);

                    var _listMaTinhMNPB = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_MIEN_NUI_PHIA_BAC).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDBSH = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_BANG_SONG_HONG).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDHMT = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DUYEN_HAI_MIEN_TRUNG).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhTN = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_TAY_NGUYEN).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDNBc = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_NAM_BO).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDBSCL = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_BANG_SONG_CUU_LONG).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    _thongke.VungMienNuiPhiaBac = await _dvhcRepos.CountAsync(x => _listMaTinhMNPB.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDongBangSongHong = await _dvhcRepos.CountAsync(x => _listMaTinhDBSH.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDuyenHaiMienTrung = await _dvhcRepos.CountAsync(x => _listMaTinhDHMT.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungTayNguyen = await _dvhcRepos.CountAsync(x => _listMaTinhTN.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDongNamBo = await _dvhcRepos.CountAsync(x => _listMaTinhDNBc.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDongBangSongCuuLong = await _dvhcRepos.CountAsync(x => _listMaTinhDBSCL.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);

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


        [AbpAuthorize]
        public async Task<CommonResponseDto> DeleteAllDataXa(long year, string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
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

        [AbpAuthorize(PermissionNames.Pages_Administration_System_ThongKe)]
        public async Task<CommonResponseDto> ReportNumberXaByDate(DateTime fromDate, DateTime toDate)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                DateTime endDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);
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
