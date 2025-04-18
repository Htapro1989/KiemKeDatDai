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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using KiemKeDatDai.RisApplication;
using static KiemKeDatDai.CommonEnum;
using System.Transactions;
using KiemKeDatDai.AppCore.Utility;
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using OfficeOpenXml;
using Newtonsoft.Json.Linq;

namespace KiemKeDatDai.RisApplication
{
    [AbpAuthorize]
    public class DanhMucDVHCAppService : KiemKeDatDaiAppServiceBase, IDonViHanhChinhAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<CapDVHC, long> _cdvhcRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaXaRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _cache;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public DanhMucDVHCAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<CapDVHC, long> cdvhcRepos,
            IRepository<User, long> userRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaXaRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            IDistributedCache distributedCache,
            IMemoryCache cache
            //ILogAppService iLogAppService
            )
        {
            _cacheManager = cacheManager;
            _iocResolver = iocResolver;
            _dvhcRepos = dvhcRepos;
            _cdvhcRepos = cdvhcRepos;
            _userRepos = userRepos;
            _bieu01TKKK_XaXaRepos = bieu01TKKK_XaXaRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _distributedCache = distributedCache;
            _cache = cache;
            //_iLogAppService = iLogAppService;
        }

        public async Task<CommonResponseDto> GetAll(DVHCDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from obj in _dvhcRepos.GetAll()
                             where obj.Year == input.Year
                             select new DVHCOutputDto
                             {
                                 Id = obj.Id,
                                 Name = obj.Name,
                                 TenVung = obj.TenVung,
                                 MaVung = obj.MaVung,
                                 TenTinh = obj.TenTinh,
                                 MaTinh = obj.MaTinh,
                                 TenHuyen = obj.TenHuyen,
                                 MaHuyen = obj.MaHuyen,
                                 TenXa = obj.TenXa,
                                 MaXa = obj.MaXa,
                                 Ma = obj.Ma,
                                 Parent_id = obj.Parent_id,
                                 Parent_Code = obj.Parent_Code,
                                 CapDVHCId = obj.CapDVHCId,
                                 TrangThaiDuyet = obj.TrangThaiDuyet,
                                 Year = obj.Year,
                                 CreationTime = obj.CreationTime,
                                 Active = obj.Active
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.ToLower().Contains(input.Filter.ToLower())
                             || x.TenVung.ToLower().Contains(input.Filter.ToLower())
                             || x.TenTinh.ToLower().Contains(input.Filter.ToLower())
                             || x.TenHuyen.ToLower().Contains(input.Filter.ToLower())
                             || x.TenXa.ToLower().Contains(input.Filter.ToLower())
                             || x.Ma.ToLower().Contains(input.Filter.ToLower()))
                             .WhereIf(!string.IsNullOrWhiteSpace(input.MaVung), x => x.Ma.ToLower() == input.MaVung.ToLower() && x.CapDVHCId == (int)CAP_DVHC.VUNG)
                             .WhereIf(!string.IsNullOrWhiteSpace(input.MaTinh), x => x.Ma.ToLower() == input.MaTinh.ToLower() && x.CapDVHCId == (int)CAP_DVHC.TINH);

                var totalCount = await query.CountAsync();
                var lstDvhc = await query.OrderBy(x => x.CreationTime)
                                    .Skip(input.SkipCount)
                                    .Take(input.MaxResultCount)
                                    .ToListAsync();

                commonResponseDto.ReturnValue = new PagedResultDto<DVHCOutputDto>()
                {
                    Items = lstDvhc,
                    TotalCount = totalCount
                };

                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDVHCByYear(int year)
        {
            try
            {
                List<DVHCOutputDto> pagedResultDto = new List<DVHCOutputDto>();
                var query = (from obj in _dvhcRepos.GetAll()
                             where obj.Year == year
                             select new DVHCOutputDto
                             {
                                 Id = obj.Id,
                                 Name = obj.Name,
                                 TenVung = obj.TenVung,
                                 MaVung = obj.MaVung,
                                 TenTinh = obj.TenTinh,
                                 MaTinh = obj.MaTinh,
                                 TenHuyen = obj.TenHuyen,
                                 MaHuyen = obj.MaHuyen,
                                 TenXa = obj.TenXa,
                                 MaXa = obj.MaXa,
                                 Ma = obj.Ma,
                                 Parent_id = obj.Parent_id,
                                 Parent_Code = obj.Parent_Code,
                                 CapDVHCId = obj.CapDVHCId,
                                 TrangThaiDuyet = obj.TrangThaiDuyet,
                                 Year = obj.Year,
                                 CreationTime = obj.CreationTime,
                                 Active = obj.Active
                             })
                             .Where(x => x.Active == true && x.Year == year)
                             .OrderBy(x => x.MaTinh)
                             .ThenBy(x => x.CapDVHCId);

                pagedResultDto = await query.ToListAsync();
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 👈 Fix lỗi Unicode
                };
                string json = JsonSerializer.Serialize(pagedResultDto, options);
                byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json);

                var stream = new MemoryStream(fileBytes);

                return new FileStreamResult(stream, "application/json")
                {
                    FileDownloadName = "dataDVHC.json"
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return null;
        }
        public async Task<CommonResponseDto> GetByUser(DVHCInput input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var userObj = await _userRepos.FirstOrDefaultAsync(input.UserId.Value);
                if (userObj != null)
                {
                    var dvhcId = userObj.DonViHanhChinhId;
                    if (dvhcId != 0)
                    {
                        var query = (from dvhc in _dvhcRepos.GetAll()
                                     join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
                                     where dvhc.Id == dvhcId && dvhc.Year == input.Year
                                     select new DVHCOutputDto
                                     {
                                         Id = dvhc.Id,
                                         TenVung = dvhc.TenVung,
                                         MaVung = dvhc.MaVung,
                                         TenTinh = dvhc.TenTinh,
                                         MaTinh = dvhc.MaTinh,
                                         TenHuyen = dvhc.TenHuyen,
                                         MaHuyen = dvhc.MaHuyen,
                                         TenXa = dvhc.TenXa,
                                         MaXa = dvhc.MaXa,
                                         Ma = dvhc.Ma,
                                         Name = dvhc.Name,
                                         Parent_id = dvhc.Parent_id,
                                         CapDVHCId = dvhc.CapDVHCId,
                                         Active = dvhc.Active,
                                         Year = dvhc.Year,
                                         TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                         ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1,
                                         CreationTime = dvhc.CreationTime
                                     });
                        commonResponseDto.ReturnValue = await query.ToListAsync();
                        commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                        commonResponseDto.Message = "Thành Công";
                    }
                    else
                    {
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        commonResponseDto.Message = "Không tìm thấy tài khoản người dùng trong hệ thống!";
                    }
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
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var query = (from dvhc in _dvhcRepos.GetAll()
                             join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
                             where dvhc.Parent_id == id
                             select new DVHCOutputDto
                             {
                                 Id = dvhc.Id,
                                 TenVung = dvhc.TenVung,
                                 MaVung = dvhc.MaVung,
                                 TenTinh = dvhc.TenTinh,
                                 MaTinh = dvhc.MaTinh,
                                 TenHuyen = dvhc.TenHuyen,
                                 MaHuyen = dvhc.MaHuyen,
                                 TenXa = dvhc.TenXa,
                                 MaXa = dvhc.MaXa,
                                 Ma = dvhc.Ma,
                                 Name = dvhc.Name,
                                 Parent_id = dvhc.Parent_id,
                                 Parent_Code = dvhc.Parent_Code,
                                 CapDVHCId = dvhc.CapDVHCId,
                                 Active = dvhc.Active,
                                 Year = dvhc.Year,
                                 TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1
                             });
                if (query != null)
                {
                    commonResponseDto.ReturnValue = await query.ToListAsync();
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không có dữ liệu!";
                    return commonResponseDto;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetByYear(long year, int capDVHC)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
                             where dvhc.Year == year && dvhc.CapDVHCId == capDVHC
                             select new DVHCOutputDto
                             {
                                 Id = dvhc.Id,
                                 TenVung = dvhc.TenVung,
                                 MaVung = dvhc.MaVung,
                                 TenTinh = dvhc.TenTinh,
                                 MaTinh = dvhc.MaTinh,
                                 TenHuyen = dvhc.TenHuyen,
                                 MaHuyen = dvhc.MaHuyen,
                                 TenXa = dvhc.TenXa,
                                 MaXa = dvhc.MaXa,
                                 Ma = dvhc.Ma,
                                 Name = dvhc.Name,
                                 Parent_id = dvhc.Parent_id,
                                 Parent_Code = dvhc.Parent_Code,
                                 CapDVHCId = dvhc.CapDVHCId,
                                 Active = dvhc.Active,
                                 Year = dvhc.Year,
                                 TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1
                             });
                if (query != null)
                {
                    commonResponseDto.ReturnValue = await query.ToListAsync();
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không có dữ liệu!";
                    return commonResponseDto;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetId(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
                             where dvhc.Id == id
                             select new DVHCOutputDto
                             {
                                 Id = dvhc.Id,
                                 TenVung = dvhc.TenVung,
                                 MaVung = dvhc.MaVung,
                                 TenTinh = dvhc.TenTinh,
                                 MaTinh = dvhc.MaTinh,
                                 TenHuyen = dvhc.TenHuyen,
                                 MaHuyen = dvhc.MaHuyen,
                                 TenXa = dvhc.TenXa,
                                 MaXa = dvhc.MaXa,
                                 Ma = dvhc.Ma,
                                 Name = dvhc.Name,
                                 Parent_id = dvhc.Parent_id,
                                 Parent_Code = dvhc.Parent_Code,
                                 CapDVHCId = dvhc.CapDVHCId,
                                 Active = dvhc.Active,
                                 Year = dvhc.Year,
                                 TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1
                             });
                if (query != null)
                {
                    commonResponseDto.ReturnValue = await query.ToListAsync();
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không có dữ liệu cấp dưới!";
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> CreateOrUpdate(DVHCInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (string.IsNullOrWhiteSpace(input.Ma))
                {
                    commonResponseDto.Message = "Mã đơn vị hành chính không được để trống";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
                var currentUser = await GetCurrentUserAsync();
                var allDvhc = await _dvhcRepos.GetAllAsync();
                if (input.Id != 0)
                {
                    var data = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        if (input.Ma != data.Ma)
                        {

                            if (CheckMaDVHC(input.Ma))
                            {
                                commonResponseDto.Message = "Mã đơn vị hành chính này đã tồn tại";
                                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }
                        }
                        data = input.MapTo(data);
                        data.TrangThaiDuyet = input.TrangThaiDuyet;
                        switch (data.CapDVHCId)
                        {
                            case (int)CAP_DVHC.XA:
                                data.TenXa = data.Name;
                                data.MaXa = input.Ma;
                                data.TenHuyen = allDvhc.Single(x => x.Ma == data.Parent_Code).Name;
                                data.MaHuyen = input.Parent_Code;
                                data.MaTinh = allDvhc.Single(x => x.Ma == data.MaHuyen).MaTinh;
                                data.TenTinh = allDvhc.Single(x => x.Ma == data.MaTinh).Name;
                                break;
                            case (int)CAP_DVHC.HUYEN:
                                data.MaHuyen = input.Ma;
                                data.TenHuyen = data.Name;
                                data.TenTinh = allDvhc.Single(x => x.Ma == data.Parent_Code).Name;
                                data.MaTinh = input.Parent_Code;
                                break;
                            case (int)CAP_DVHC.TINH:
                                data.TenTinh = data.Name;
                                data.MaTinh = input.Ma;
                                data.TenVung = allDvhc.Single(x => x.Ma == data.Parent_Code).Name;
                                data.MaVung = input.Parent_Code;
                                break;
                            case (int)CAP_DVHC.VUNG:
                                data.TenVung = data.Name;
                                break;
                            default:
                                break;
                        }
                        await _dvhcRepos.UpdateAsync(data);
                    }
                    else
                    {
                        commonResponseDto.Message = "Đơn vị hành chính này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                }
                else
                {
                    if (CheckMaDVHC(input.Ma))
                    {
                        commonResponseDto.Message = "Mã đơn vị hành chính này đã tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    var dvhc = input.MapTo<DVHCInputDto>();
                    switch (dvhc.CapDVHCId)
                    {
                        case (int)CAP_DVHC.XA:
                            dvhc.TenXa = input.Name;
                            dvhc.MaXa = input.Ma;
                            dvhc.TenHuyen = allDvhc.Single(x => x.Ma == dvhc.Parent_Code).Name;
                            dvhc.MaHuyen = input.Parent_Code;
                            dvhc.MaTinh = allDvhc.Single(x => x.Ma == dvhc.MaHuyen).MaTinh;
                            dvhc.TenTinh = allDvhc.Single(x => x.Ma == dvhc.MaTinh).Name;
                            break;
                        case (int)CAP_DVHC.HUYEN:
                            dvhc.TenHuyen = input.Name;
                            dvhc.MaHuyen = input.Ma;
                            dvhc.TenTinh = allDvhc.Single(x => x.Ma == dvhc.Parent_Code).Name;
                            dvhc.MaTinh = input.Parent_Code;
                            break;
                        case (int)CAP_DVHC.TINH:
                            dvhc.TenTinh = input.Name;
                            dvhc.MaTinh = input.Ma;
                            dvhc.TenVung = allDvhc.Single(x => x.Ma == dvhc.Parent_Code).Name;
                            dvhc.MaVung = input.Parent_Code;
                            break;
                        default:
                            break;
                    }
                    await _dvhcRepos.InsertAsync(dvhc);
                }
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        private bool CheckMaDVHC(string ma)
        {
            var objDVHC = _dvhcRepos.FirstOrDefault(x => x.Ma == ma);
            if (objDVHC != null)
                return true;
            return false;
        }

        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var objDVHC = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (objDVHC != null)
                {
                    await _dvhcRepos.DeleteAsync(objDVHC);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Dơn vị hành chính này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        public async Task<CommonResponseDto> BaoCaoDVHC(BaoCaoInPutDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentUser = await GetCurrentUserAsync();
                var lstBaoCao = new List<BaoCaoDonViHanhChinhOutPutDto>();
                var currentDvhc = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.Ma && x.Year == input.Year);
                var allDvhc = await _dvhcRepos.GetAllAsync();
                if (currentDvhc != null)
                {
                    //lấy thằng gốc
                    var baoCaoDVHC = new BaoCaoDonViHanhChinhOutPutDto
                    {
                        Id = currentDvhc.Id,
                        Ten = currentDvhc.Name,
                        MaDVHC = currentDvhc.Ma,
                        CapDVHC = currentDvhc.CapDVHCId,
                        ParentId = currentDvhc.Parent_id,
                        NgayCapNhat = currentDvhc.NgayGui,
                        TrangThaiDuyet = currentDvhc.TrangThaiDuyet,
                        ChildStatus = 1
                    };
                    baoCaoDVHC.Root = currentUser.DonViHanhChinhCode == input.Ma ? true : false;
                    switch (currentDvhc.CapDVHCId)
                    {
                        case (int)CAP_DVHC.TRUNG_UONG:
                            baoCaoDVHC.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA);
                            baoCaoDVHC.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.VUNG:
                            var lstMaTinh = allDvhc.Where(x => x.Parent_Code == currentDvhc.Ma).Select(x => x.Ma).ToArray();
                            baoCaoDVHC.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh));
                            baoCaoDVHC.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            baoCaoDVHC.TrangThaiDuyet = null;
                            break;
                        case (int)CAP_DVHC.TINH:
                            baoCaoDVHC.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == currentDvhc.MaTinh);
                            baoCaoDVHC.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == currentDvhc.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == currentDvhc.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.HUYEN:
                            baoCaoDVHC.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == currentDvhc.MaHuyen);
                            baoCaoDVHC.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == currentDvhc.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == currentDvhc.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.XA:
                            baoCaoDVHC.Tong = 1;
                            baoCaoDVHC.TongDuyet = currentDvhc.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET ? 1 : 0;
                            baoCaoDVHC.TongNop = currentDvhc.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET ? 1 : 0;
                            baoCaoDVHC.ChildStatus = 0;
                            var _bieu01TKKK = await _bieu01TKKK_XaXaRepos.FirstOrDefaultAsync(x => x.MaXa == currentDvhc.Ma && x.Year == input.Year);
                            if (_bieu01TKKK != null)
                                baoCaoDVHC.NgayCapNhat = _bieu01TKKK.CreationTime;
                            break;
                        default:
                            break;
                    }
                    //lấy danh sách con
                    var lstChild = allDvhc.Where(x => x.Parent_Code == input.Ma && x.Year == input.Year).ToList();
                    //Xác định  trạng thái button nộp báo cáo
                    if (baoCaoDVHC.Root == true)
                    {
                        baoCaoDVHC.IsNopBaoCao = (baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.CHO_DUYET && baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET) ? true : false;
                        if (baoCaoDVHC.CapDVHC == (int)CAP_DVHC.TRUNG_UONG || baoCaoDVHC.CapDVHC == (int)CAP_DVHC.VUNG)
                            baoCaoDVHC.IsNopBaoCao = false;
                        if (baoCaoDVHC.ChildStatus == 0)
                        {
                            baoCaoDVHC.IsNopBaoCao = baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET ? true : false;
                        }
                        else
                        {
                            var soDaDuyet = allDvhc.Count(x => x.Parent_Code == input.Ma && x.Year == input.Year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            if (soDaDuyet == lstChild.Count && baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET)
                                baoCaoDVHC.IsNopBaoCao = true;
                            else
                                baoCaoDVHC.IsNopBaoCao = false;
                        }
                    }
                    lstBaoCao.Add(baoCaoDVHC);
                    if (lstChild != null)
                    {
                        foreach (var item in lstChild)
                        {
                            //lấy thằng con
                            var baoCaoDVHC_child = new BaoCaoDonViHanhChinhOutPutDto
                            {
                                Id = item.Id,
                                Ten = item.Name,
                                MaDVHC = item.Ma,
                                CapDVHC = item.CapDVHCId,
                                ParentId = item.Parent_id,
                                NgayCapNhat = item.NgayGui,
                                TrangThaiDuyet = item.TrangThaiDuyet,
                                IsNopBaoCao = false
                            };
                            switch (item.CapDVHCId)
                            {
                                case (int)CAP_DVHC.VUNG:
                                    var lstMaTinh = allDvhc.Where(x => x.Parent_Code == currentDvhc.Ma).Select(x => x.Ma).ToArray();
                                    baoCaoDVHC_child.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh));
                                    baoCaoDVHC_child.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                                    baoCaoDVHC_child.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                                    baoCaoDVHC_child.ChildStatus = 1;
                                    baoCaoDVHC_child.TrangThaiDuyet = null;
                                    break;
                                case (int)CAP_DVHC.TINH:
                                    baoCaoDVHC_child.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == item.MaTinh);
                                    baoCaoDVHC_child.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == item.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                                    baoCaoDVHC_child.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == item.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                                    baoCaoDVHC_child.ChildStatus = 1;
                                    break;
                                case (int)CAP_DVHC.HUYEN:
                                    baoCaoDVHC_child.Tong = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == item.MaHuyen);
                                    baoCaoDVHC_child.TongDuyet = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == item.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                                    baoCaoDVHC_child.TongNop = allDvhc.Count(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == item.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                                    baoCaoDVHC_child.ChildStatus = 1;
                                    break;
                                case (int)CAP_DVHC.XA:
                                    baoCaoDVHC_child.Tong = 1;
                                    baoCaoDVHC_child.TongDuyet = item.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET ? 1 : 0;
                                    baoCaoDVHC_child.TongNop = item.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET ? 1 : 0;
                                    baoCaoDVHC_child.ChildStatus = 0;
                                    var _bieu01TKKK = await _bieu01TKKK_XaXaRepos.FirstOrDefaultAsync(x => x.MaXa == item.Ma && x.Year == input.Year);
                                    if (_bieu01TKKK != null)
                                        baoCaoDVHC.NgayCapNhat = _bieu01TKKK.CreationTime;
                                    break;
                                default:
                                    break;
                            }
                            lstBaoCao.Add(baoCaoDVHC_child);
                        }
                    }
                    commonResponseDto.ReturnValue = lstBaoCao;
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "ĐVHC này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
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
        public async Task<CommonResponseDto> GetDropDownVung()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.VUNG
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownTinhByVungId(long vungId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.TINH && dvhc.Parent_id == vungId
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownTinhByMaVung(string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.TINH && dvhc.Parent_Code == ma
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownTinh()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.TINH
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownHuyenByTinhId(long tinhId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.HUYEN && dvhc.Parent_id == tinhId
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownXaByHuyenId(long huyenId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.XA && dvhc.Parent_id == huyenId
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownHuyenByMaTinh(string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.HUYEN && dvhc.Parent_Code == ma
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        public async Task<CommonResponseDto> GetDropDownXaByMaHuyen(string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll()
                             where dvhc.CapDVHCId == (int)CAP_DVHC.XA && dvhc.Parent_Code == ma
                             select new DropDownListDVHCDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                                 Ma = dvhc.Ma,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
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
        public async Task<CommonResponseDto> UploadFileDVHC(IFormFile fileUpload, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                //var results = new List<List<DamInfoJsonOutput>>();
                var urlFile = await WriteFile(fileUpload);

                var dt = new System.Data.DataTable();
                var fi = new FileInfo(urlFile);
                // Check if the file exists
                if (!fi.Exists)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "File " + urlFile + " không tồn tại";
                }
                //ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var excel = new ExcelPackage(new MemoryStream(System.IO.File.ReadAllBytes(urlFile)));

                var worksheets = excel.Workbook.Worksheets;
                if (worksheets == null)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không đọc được file";
                }
                else
                {
                    foreach (var sheet in worksheets)
                    {
                        var table = sheet.Tables.FirstOrDefault();
                        if (table != null)
                        {
                            //if (sheet.Index == 0)
                            //{
                            //    await _dvhcRepos.DeleteAsync(x => x.Year == year);
                            //}
                            var tableData = table.ToDataTable();
                            var jArray = JArray.FromObject(tableData);
                            foreach (var item in jArray)
                            {
                                if (item != null)
                                {
                                    var data = new DonViHanhChinh()
                                    {
                                        TenVung = item.Value<string>("TenVung"),
                                        MaVung = item.Value<string>("MaVung"),
                                        TenTinh = item.Value<string>("TenTinh"),
                                        MaTinh = item.Value<string>("MaTinh"),
                                        TenHuyen = item.Value<string>("TenHuyen"),
                                        MaHuyen = item.Value<string>("MaHuyen"),
                                        TenXa = item.Value<string>("TenXa"),
                                        MaXa = item.Value<string>("MaXa"),
                                        Ma = item.Value<string>("MaXa") != null && item.Value<string>("MaXa") != "" ? item.Value<string>("MaXa") : 
                                            (item.Value<string>("MaHuyen") != null && item.Value<string>("MaHuyen") != "" ? item.Value<string>("MaHuyen") : item.Value<string>("MaTinh")),
                                        Name = item.Value<string>("TenXa") != null && item.Value<string>("TenXa") != "" ? item.Value<string>("TenXa") :
                                            (item.Value<string>("TenHuyen") != null && item.Value<string>("TenHuyen") != "" ? item.Value<string>("TenHuyen") : item.Value<string>("TenTinh")),
                                        //Name = item.Value<string>("Name"),
                                        Parent_id = null,
                                        Parent_Code = item.Value<string>("MaXa") != null && item.Value<string>("MaXa") != "" ? item.Value<string>("MaHuyen") : item.Value<string>("MaTinh"),
                                        CapDVHCId = item.Value<string>("MaXa") != null && item.Value<string>("MaXa") != "" ? 4 :
                                            (item.Value<string>("MaHuyen") != null && item.Value<string>("MaHuyen") != "" ? 3 : 2),
                                        Active = true,
                                        Year = year,
                                        TrangThaiDuyet = null,
                                        NgayGui = null,
                                        NgayDuyet = null,
                                        SoDVHCCon = null,
                                        SoDVHCDaDuyet = null,
                                        MaxFileUpload = null
                                    };
                                    await _dvhcRepos.InsertAsync(data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
        public async Task<FileStreamResult> DownloadTemplateDVHC()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var template = "TemplateImport_DVHC.xlsx";
                MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(Path.Combine("wwwroot/Templates/excels", template)));
                return new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "Template_DVHC.xlsx"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            string exactPathDirectory = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks.ToString() + extension;
                var filePath = "wwwroot\\Uploads\\Files\\DVHC";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                exactPathDirectory = "wwwroot\\Uploads\\Files\\DVHC" + "\\" + fileName;
                var exactPath = "wwwroot\\Uploads\\Files\\DVHC" + "\\" + fileName;
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return exactPathDirectory;
        }
    }
}
