using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.RisApplication;
using KiemKeDatDai.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiemKeDatDai.CommonEnum;

namespace KiemKeDatDai.App.BieuPhuLuc
{
    [AbpAuthorize]
    public class BieuPhuLucAppService : KiemKeDatDaiAppServiceBase, IBieuPhuLucAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Data_Commune, long> _dcRepos;
        private readonly IRepository<Data_BienDong, long> _dbdRepos;
        private readonly IRepository<CapDVHC, long> _cdvhcRepos;
        private readonly IRepository<User, long> _userRepos;

        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public BieuPhuLucAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Data_Commune, long> dcRepos,
            IRepository<Data_BienDong, long> dbdRepos,
            IRepository<CapDVHC, long> cdvhcRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _cacheManager = cacheManager;
            _iocResolver = iocResolver;
            _dvhcRepos = dvhcRepos;
            _dcRepos = dcRepos;
            _dbdRepos = dbdRepos;
            _cdvhcRepos = cdvhcRepos;
            _userRepos = userRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetBieuPL3ByDVHC(BieuPhuLucIIISearchDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var result = new BieuPhuLucIIIOutputDto();
                var dvhcObj = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == input.dvhcId && x.Year == input.year);
                if (dvhcObj != null)
                {
                    var query = (from item in _dcRepos.GetAll()
                                where item.MaXa == dvhcObj.MaXa && item.Year == input.year
                                 select new BieuPhuLucIIIDto()
                                {
                                    DienTich = item.DienTich,
                                    MaLoaiDatHienTrang = item.MucDichSuDung,
                                    MaLoaiDatKyTruoc = item.MucDichSuDungKyTruoc,
                                    MaLoaiDatSuDungKetHop = "",
                                    MaDoiTuongHienTrang = item.MaDoiTuong,
                                    MaDoiTuongKyTruoc = item.MaDoiTuongKyTruoc,
                                    MaKhuVucTongHop = "",
                                    GhiChu = "",
                                    MaXa = item.MaXa
                                });
                    //var lstBMPL3 = _dcRepos.GetAll()
                    //    .Where(x => x.MaXa == dvhcObj.MaXa)
                    //    .Select((item, index) => new BieuPhuLucIIIDto()
                    //        {
                    //            DienTich = item.DienTich,
                    //            MaLoaiDatHienTrang = item.MucDichSuDung,
                    //            MaLoaiDatKyTruoc = item.MucDichSuDungKyTruoc,
                    //            MaLoaiDatSuDungKetHop = "",
                    //            MaDoiTuongHienTrang = item.MaDoiTuong,
                    //            MaDoiTuongKyTruoc = item.MaDoiTuongKyTruoc,
                    //            MaKhuVucTongHop = "",
                    //            GhiChu = ""
                    //    }).ToList();
                    result.BieuPhuLucIIIDtos = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
                    //result.BieuPhuLucIIIs = await query.ToListAsync();
                    result.TenTinh = dvhcObj.TenTinh;
                    result.TenHuyen = dvhcObj.TenHuyen;
                    result.TenXa = dvhcObj.TenXa;
                    result.year = input.year;
                    commonResponseDto.ReturnValue = result;
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else 
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Đơn vị hành chính không tồn tại";
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetBieuPL4ByDVHC(BieuPhuLucIVSearchDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var result = new BieuPhuLucIVOutputDto();
                var dvhcObj = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == input.dvhcId && x.Year == input.year);
                if (dvhcObj != null)
                {
                    var query = (from item in _dbdRepos.GetAll()
                                 where item.MaXa == dvhcObj.MaXa
                                 select new BieuPhuLucIVDto()
                                 {
                                     SHTDTruocBD = item.SHKDTruocBienDong,
                                     SHTDSauBD = item.SHKDSauBienDong,
                                     TenNguoiSDDat = item.TenChuSuDung,
                                     DiaChiThuaDat = item.DiaChiThuaDat,
                                     DienTichBD = item.DienTichBienDong,
                                     MaLoaiDatTruocBD = item.MDSDTruocBienDong,
                                     MaLoaiDatSauBD = item.MDSDSauBienDong,
                                     NDTD = item.NDThayDoi
                                 });
                    //var lstBMPL3 = _dcRepos.GetAll()
                    //    .Where(x => x.MaXa == dvhcObj.MaXa)
                    //    .Select((item, index) => new BieuPhuLucIIIDto()
                    //        {
                    //            DienTich = item.DienTich,
                    //            MaLoaiDatHienTrang = item.MucDichSuDung,
                    //            MaLoaiDatKyTruoc = item.MucDichSuDungKyTruoc,
                    //            MaLoaiDatSuDungKetHop = "",
                    //            MaDoiTuongHienTrang = item.MaDoiTuong,
                    //            MaDoiTuongKyTruoc = item.MaDoiTuongKyTruoc,
                    //            MaKhuVucTongHop = "",
                    //            GhiChu = ""
                    //    }).ToList();
                    result.BieuPhuLucIVDtos = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
                    //result.BieuPhuLucIIIs = await query.ToListAsync();
                    result.TenTinh = dvhcObj.TenTinh;
                    result.TenHuyen = dvhcObj.TenHuyen;
                    result.TenXa = dvhcObj.TenXa;
                    result.year = input.year;
                    commonResponseDto.ReturnValue = result;
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Đơn vị hành chính không tồn tại";
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
    }
}
