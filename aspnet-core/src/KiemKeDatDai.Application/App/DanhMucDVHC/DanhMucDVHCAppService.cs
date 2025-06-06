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
using KiemKeDatDai.Authorization;
using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Aspose.Cells;

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
        private readonly IRepository<EntitiesDb.File, long> _fileRepos;
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _cache;
        IUnitOfWorkManager _unitOfWorkManager;

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
            IRepository<EntitiesDb.File, long> fileRepos,
            IHttpContextAccessor httpContextAccessor,
            IDistributedCache distributedCache,
            IUnitOfWorkManager unitOfWorkManager,
            IMemoryCache cache
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
            _fileRepos = fileRepos;
            _distributedCache = distributedCache;
            _cache = cache;
            _unitOfWorkManager = unitOfWorkManager;
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
                var userObj = await _userRepos.FirstOrDefaultAsync(input.UserId.Value);

                if (userObj != null)
                {
                    var maDvhc = userObj.DonViHanhChinhCode;

                    if (maDvhc != null)
                    {
                        var query = (from dvhc in _dvhcRepos.GetAll().Where(x => x.Year == input.Year && x.Ma == maDvhc)
                                     join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
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
                                         CreationTime = dvhc.CreationTime,
                                         MaxFileUpload = dvhc.MaxFileUpload
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

        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentDvhc = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == id);

                var query = (from dvhc in _dvhcRepos.GetAll().Where(x => x.Active == true && x.Year == currentDvhc.Year)
                             join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
                             where dvhc.Parent_Code == currentDvhc.Ma
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
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1,
                                 MaxFileUpload = dvhc.MaxFileUpload
                             });

                var lstDvhc = await query.ToListAsync();

                commonResponseDto.ReturnValue = lstDvhc;
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

        public async Task<CommonResponseDto> GetByIdForUser(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentDvhc = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == id);

                var query = (from dvhc in _dvhcRepos.GetAll().Where(x => x.Active == true && x.Year == currentDvhc.Year)
                             join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.MaCapDVHC
                             where dvhc.Parent_Code == currentDvhc.Ma
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
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1,
                                 IsExitsUser = true,
                                 MaxFileUpload = dvhc.MaxFileUpload

                             });

                var lstDvhc = await query.ToListAsync();

                var allUser = await _userRepos.GetAll().ToListAsync();

                foreach (var item in lstDvhc)
                {
                    var lstMa = await _iUserAppService.GetChildrenMa(item.Ma);

                    lstMa.Add(item.Ma);

                    foreach (var ma in lstMa)
                    {
                        if (allUser.FirstOrDefault(x => x.DonViHanhChinhCode == ma) == null)
                        {
                            item.IsExitsUser = false;
                            break;
                        }
                    }
                }

                commonResponseDto.ReturnValue = lstDvhc;
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
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1,
                                 MaxFileUpload = dvhc.MaxFileUpload
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
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1,
                                 MaxFileUpload = dvhc.MaxFileUpload

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


        [AbpAuthorize(PermissionNames.Pages_Administration_System_Dvhc)]
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
                var allDvhc = await _dvhcRepos.GetAll().Where(x => x.Year == input.Year).ToListAsync();

                if (input.Id != 0)
                {
                    commonResponseDto = await Update(input, allDvhc);
                }
                else
                {
                    commonResponseDto = await Create(input, allDvhc);
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

        public async Task<CommonResponseDto> Create(DVHCInputDto input, List<DonViHanhChinh> allDvhc)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            if (CheckMaDVHC(input.Ma))
            {
                commonResponseDto.Message = "Mã đơn vị hành chính" + input.Ma + "đã tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            var dvhc = input.MapTo<DVHCInputDto>();
            dvhc.Active = true;

            switch (dvhc.CapDVHCId)
            {
                case (int)CAP_DVHC.XA:
                    dvhc.TenXa = input.Name;
                    dvhc.MaXa = input.Ma;
                    dvhc.TenHuyen = input.TenHuyen != null ? input.TenHuyen : allDvhc.Single(x => x.Ma == dvhc.Parent_Code).Name;
                    dvhc.MaHuyen = input.MaHuyen != null ? input.MaHuyen : input.Parent_Code;
                    dvhc.MaTinh = input.MaTinh != null ? input.MaTinh : allDvhc.Single(x => x.Ma == dvhc.MaHuyen).MaTinh;
                    dvhc.TenTinh = input.TenTinh != null ? input.TenTinh : allDvhc.Single(x => x.Ma == dvhc.MaTinh).Name;
                    dvhc.MaxFileUpload = input.MaxFileUpload != null ? input.MaxFileUpload : 5;
                    break;
                case (int)CAP_DVHC.HUYEN:
                    dvhc.TenHuyen = input.Name;
                    dvhc.MaHuyen = input.Ma;
                    dvhc.TenTinh = input.TenTinh != null ? input.TenTinh : allDvhc.Single(x => x.Ma == dvhc.Parent_Code).Name;
                    dvhc.MaTinh = input.MaTinh != null ? input.MaTinh : input.Parent_Code;
                    break;
                case (int)CAP_DVHC.TINH:
                    dvhc.TenTinh = input.Name;
                    dvhc.MaTinh = input.Ma;
                    dvhc.TenVung = input.TenVung != null ? input.TenVung : allDvhc.Single(x => x.Ma == dvhc.Parent_Code).Name;
                    dvhc.MaVung = input.MaVung != null ? input.MaVung : input.Parent_Code;
                    break;
                default:
                    break;
            }

            await _dvhcRepos.InsertAsync(dvhc);

            commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        private async Task<CommonResponseDto> Update(DVHCInputDto input, List<DonViHanhChinh> allDvhc)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            var data = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (data == null)
            {
                commonResponseDto.Message = "Đơn vị hành chính này không tồn tại";
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                return commonResponseDto;

            }

            //Nếu sửa mã đvhc thì phải check lại mã này đã tồn tại chưa
            if (input.Ma != data.Ma)
            {
                if (CheckMaDVHC(input.Ma))
                {
                    commonResponseDto.Message = "Mã đơn vị hành chính" + input.Ma + "đã tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
            }

            data = input.MapTo(data);
            data.TrangThaiDuyet = input.TrangThaiDuyet;
            data.Active = true;

            switch (data.CapDVHCId)
            {
                case (int)CAP_DVHC.XA:
                    data.TenXa = data.Name;
                    data.MaXa = input.Ma;
                    data.TenHuyen = allDvhc.Single(x => x.Ma == data.Parent_Code).Name;
                    data.MaHuyen = input.Parent_Code;
                    data.MaTinh = allDvhc.Single(x => x.Ma == data.MaHuyen).MaTinh;
                    data.TenTinh = allDvhc.Single(x => x.Ma == data.MaTinh).Name;
                    data.MaxFileUpload = input.MaxFileUpload;
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

            commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        private bool CheckMaDVHC(string ma)
        {
            var objDVHC = _dvhcRepos.FirstOrDefault(x => x.Ma == ma);

            if (objDVHC != null)
                return true;
            return false;
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_System_Dvhc)]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
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
                var allDvhc = await _dvhcRepos.GetAll().Where(x => x.Active == true && x.Year == input.Year).ToListAsync();

                if (currentDvhc == null)
                {
                    commonResponseDto.Message = "ĐVHC này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }

                //lấy thằng gốc
                var rootBaoCao = await BuildBaoCaoAsync(currentDvhc, input, allDvhc);
                rootBaoCao.Root = currentUser.DonViHanhChinhCode == input.Ma ? true : false;

                //lấy danh sách con
                var lstChild = allDvhc.Where(x => x.Parent_Code == input.Ma).ToList();

                //Xác định  trạng thái button nộp báo cáo
                if (rootBaoCao.Root == true)
                {
                    rootBaoCao.IsNopBaoCao = await CheckNopBaoCao(rootBaoCao, input, allDvhc, lstChild);
                }

                lstBaoCao.Add(rootBaoCao);

                if (lstChild != null)
                {
                    foreach (var item in lstChild)
                    {
                        //lấy thằng con
                        var childBaoCao = await BuildBaoCaoAsync(item, input, allDvhc);
                        lstBaoCao.Add(childBaoCao);
                    }
                }

                commonResponseDto.ReturnValue = lstBaoCao;
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

        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> BaoCaoDVHCForDashboard(BaoCaoInPutDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentUser = await _userRepos.FirstOrDefaultAsync(x => x.DonViHanhChinhCode == 0.ToString());
                var lstBaoCao = new List<BaoCaoDonViHanhChinhOutPutDto>();
                var currentDvhc = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.Ma && x.Year == input.Year);
                var allDvhc = await _dvhcRepos.GetAll().Where(x => x.Active == true && x.Year == input.Year).ToListAsync();

                if (currentDvhc == null)
                {
                    commonResponseDto.Message = "ĐVHC này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }

                //lấy thằng gốc
                var rootBaoCao = await BuildBaoCaoAsync(currentDvhc, input, allDvhc);
                rootBaoCao.Root = currentUser.DonViHanhChinhCode == input.Ma ? true : false;


                //lấy danh sách con
                var lstChild = allDvhc.Where(x => x.Parent_Code == input.Ma).ToList();
                lstBaoCao.Add(rootBaoCao);

                if (lstChild != null)
                {
                    foreach (var item in lstChild)
                    {
                        //lấy thằng con
                        var childBaoCao = await BuildBaoCaoAsync(item, input, allDvhc);
                        lstBaoCao.Add(childBaoCao);
                    }
                }

                commonResponseDto.ReturnValue = lstBaoCao;
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

        private async Task<BaoCaoDonViHanhChinhOutPutDto> BuildBaoCaoAsync(DonViHanhChinh dvhc, BaoCaoInPutDto input, List<DonViHanhChinh> allDvhc)
        {
            var allFile = _fileRepos.GetAll()
                .Where(x => x.Year == input.Year && x.MaDVHC != "")
                .AsEnumerable()
                .DistinctBy(x => x.MaDVHC)
                .ToList();
            var allDvhcXa = allDvhc
                .Where(x => x.CapDVHCId == (int)CAP_DVHC.XA)
                .ToList();

            var dto = new BaoCaoDonViHanhChinhOutPutDto
            {
                Id = dvhc.Id,
                Ten = dvhc.Name,
                MaDVHC = dvhc.Ma,
                CapDVHC = dvhc.CapDVHCId,
                ParentId = dvhc.Parent_id,
                TrangThaiDuyet = dvhc.TrangThaiDuyet,
                NgayCapNhat = dvhc.NgayGui,
                ChildStatus = 1
            };

            var tongDayDuLieuInputDto = new TongDayDuLieuInputDto
            {
                AllDvhc = allDvhcXa,
                AllFile = allFile,
                MaXa = null,
                MaHuyen = null,
                MaTinh = null,
                ListMaTinh = new List<string>()
            };

            switch (dvhc.CapDVHCId)
            {
                case (int)CAP_DVHC.XA:
                    dto.Tong = 1;
                    dto.TongDuyet = dvhc.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET ? 1 : 0;
                    dto.TongNop = dvhc.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET ? 1 : 0;
                    tongDayDuLieuInputDto.MaXa = dvhc.MaXa;
                    dto.TongDayDuLieu = TongDayDuLieu(tongDayDuLieuInputDto);
                    dto.ChildStatus = 0;

                    var bieu01 = await _bieu01TKKK_XaXaRepos.FirstOrDefaultAsync(x => x.MaXa == dvhc.Ma && x.Year == input.Year);
                    if (bieu01 != null)
                        dto.NgayCapNhat = bieu01.CreationTime;
                    break;

                case (int)CAP_DVHC.HUYEN:
                    var dataHuyen = DemBaoCao(allDvhc, x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == dvhc.MaHuyen);
                    dto.Tong = dataHuyen.Tong;
                    dto.TongDuyet = dataHuyen.Duyet;
                    dto.TongNop = dataHuyen.Nop;
                    tongDayDuLieuInputDto.MaHuyen = dvhc.MaHuyen;
                    dto.TongDayDuLieu = TongDayDuLieu(tongDayDuLieuInputDto);
                    break;

                case (int)CAP_DVHC.TINH:
                    var dataTinh = DemBaoCao(allDvhc, x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == dvhc.MaTinh);
                    dto.Tong = dataTinh.Tong;
                    dto.TongDuyet = dataTinh.Duyet;
                    dto.TongNop = dataTinh.Nop;
                    tongDayDuLieuInputDto.MaTinh = dvhc.MaTinh;
                    dto.TongDayDuLieu = TongDayDuLieu(tongDayDuLieuInputDto);
                    break;

                case (int)CAP_DVHC.VUNG:
                    var lstMaTinh = allDvhc.Where(x => x.Parent_Code == dvhc.Ma).Select(x => x.Ma).ToArray();
                    var dataVung = DemBaoCao(allDvhc, x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh));
                    dto.Tong = dataVung.Tong;
                    dto.TongDuyet = dataVung.Duyet;
                    dto.TongNop = dataVung.Nop;
                    dto.TrangThaiDuyet = null;
                    tongDayDuLieuInputDto.ListMaTinh = lstMaTinh.ToList();
                    dto.TongDayDuLieu = TongDayDuLieu(tongDayDuLieuInputDto);
                    break;

                case (int)CAP_DVHC.TRUNG_UONG:
                    var dataTW = DemBaoCao(allDvhc, x => x.CapDVHCId == (int)CAP_DVHC.XA);
                    dto.Tong = dataTW.Tong;
                    dto.TongDuyet = dataTW.Duyet;
                    dto.TongNop = dataTW.Nop;
                    tongDayDuLieuInputDto.ListMaTinh = allDvhc.Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH).Select(x => x.Ma).ToList();
                    dto.TongDayDuLieu = TongDayDuLieu(tongDayDuLieuInputDto);
                    break;

            }

            return dto;
        }

        private (int Tong, int Duyet, int Nop) DemBaoCao(List<DonViHanhChinh> all, Func<DonViHanhChinh, bool> condition)
        {
            var list = all.Where(condition).ToList();
            return (
                list.Count,
                list.Count(x => x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET),
                list.Count(x => x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET || x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
            );
        }

        private int TongDayDuLieu(TongDayDuLieuInputDto input)
        {
            var lstMaXa = new List<string>();

            if (input.MaXa != null)
            {
                lstMaXa.Add(input.MaXa);
            }

            if (input.MaHuyen != null)
            {
                lstMaXa = input.AllDvhc
                    .Where(x => x.MaHuyen == input.MaHuyen)
                    .Select(x => x.MaXa)
                    .Distinct()
                    .ToList();
            }

            if (input.MaTinh != null)
            {
                lstMaXa = input.AllDvhc
                    .Where(x => x.MaTinh == input.MaTinh)
                    .Select(x => x.MaXa)
                    .Distinct()
                    .ToList();
            }

            if (input.ListMaTinh != null)
            {
                lstMaXa = input.AllDvhc
                    .Where(x => input.ListMaTinh.Contains(x.MaTinh))
                    .Select(x => x.MaXa)
                    .Distinct()
                    .ToList();
            }

            var query = input.AllFile.Where(x => lstMaXa.Contains(x.MaDVHC)).ToList();
            return query.Count();
        }

        private async Task<bool> CheckNopBaoCao(BaoCaoDonViHanhChinhOutPutDto baoCaoDVHC, BaoCaoInPutDto input, List<DonViHanhChinh> allDvhc, List<DonViHanhChinh> lstChild)
        {
            bool isNopBaoCao = false;
            isNopBaoCao = (baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.CHO_DUYET && baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET) ? true : false;

            if (baoCaoDVHC.CapDVHC == (int)CAP_DVHC.TRUNG_UONG || baoCaoDVHC.CapDVHC == (int)CAP_DVHC.VUNG)
                isNopBaoCao = false;

            if (baoCaoDVHC.ChildStatus == 0)
            {
                isNopBaoCao = baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET ? true : false;
            }
            else
            {
                var soDaDuyet = allDvhc.Count(x => x.Parent_Code == input.Ma && x.Year == input.Year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);

                if (soDaDuyet == lstChild.Count && baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET)
                    isNopBaoCao = true;
                else
                    isNopBaoCao = false;
            }

            return isNopBaoCao;
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

        [AbpAuthorize(PermissionNames.Pages_Administration_System_Dvhc)]
        public async Task<CommonResponseDto> UploadFileDVHC(IFormFile fileUpload)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    //var results = new List<List<DamInfoJsonOutput>>();
                    string tenThuMuc = "DVHC";
                    var urlFile = await Utility.WriteFile(fileUpload, tenThuMuc);

                    var dt = new System.Data.DataTable();
                    var fi = new FileInfo(urlFile);

                    // Check if the file exists
                    if (!fi.Exists)
                    {
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        commonResponseDto.Message = "File " + urlFile + " không tồn tại";
                        return commonResponseDto;
                    }

                    //ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var excel = new ExcelPackage(new MemoryStream(System.IO.File.ReadAllBytes(urlFile)));
                    var worksheets = excel.Workbook.Worksheets;

                    if (worksheets == null)
                    {
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        commonResponseDto.Message = "Không đọc được file";
                        return commonResponseDto;
                    }

                    foreach (var sheet in worksheets)
                    {
                        var table = sheet.Tables.FirstOrDefault();

                        if (table != null)
                        {
                            var tableData = table.ToDataTable();
                            var jArray = JArray.FromObject(tableData);
                            var year = jArray[0].Value<long>("Year");
                            var allDvhc = await _dvhcRepos.GetAll().Where(x => x.Year == year).ToListAsync();

                            foreach (var item in jArray)
                            {
                                if (item != null)
                                {
                                    if (item.Value<string>("CapDvhc") == null)
                                    {
                                        commonResponseDto.Message = "Chưa có cấp đơn vị hành chính";
                                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                                        return commonResponseDto;
                                    }

                                    var capDvhc = item.Value<long>("CapDvhc");
                                    
                                    var input = new DVHCInputDto()
                                    {
                                        TenVung = item.Value<string>("TenVung"),
                                        MaVung = item.Value<string>("MaVung"),
                                        TenTinh = item.Value<string>("TenTinh"),
                                        MaTinh = item.Value<string>("MaTinh"),
                                        TenHuyen = item.Value<string>("TenHuyen"),
                                        MaHuyen = item.Value<string>("MaHuyen"),
                                        TenXa = item.Value<string>("TenXa"),
                                        CapDVHCId = capDvhc
                                    };

                                    switch (capDvhc)
                                    {
                                        case (int)CAP_DVHC.XA:
                                            input.Name = input.TenXa;
                                            input.Ma = input.MaXa;
                                            break;
                                        case (int)CAP_DVHC.HUYEN:
                                            input.Name = input.TenHuyen;
                                            input.Ma = input.MaHuyen;
                                            break;
                                        case (int)CAP_DVHC.TINH:
                                            input.Name = input.TenTinh;
                                            input.Ma = input.MaTinh;
                                            break;
                                    }

                                    commonResponseDto = await Create(input, allDvhc);
                                }
                            }

                            uow.Complete();
                        }
                    }
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

        public async Task<FileStreamResult> DownloadTemplateDVHC()
        {
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
    }
}
