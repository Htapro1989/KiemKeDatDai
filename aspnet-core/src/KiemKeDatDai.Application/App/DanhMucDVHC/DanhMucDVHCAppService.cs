using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Abp.Threading;
using KiemKeDatDai.AppCore.DanhMucDVHC;
using KiemKeDatDai.AppCore.DanhMucDVHC.Dto;
using KiemKeDatDai.AppCore.Log;
using KiemKeDatDai.AppCore.Log.Dto;
using KiemKeDatDai.AppCore.Utility;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace KiemKeDatDai.App.DanhMucDVHC
{
    [AbpAuthorize]
    public class DanhMucDVHCAppService : KiemKeDatDaiAppServiceBase, IDonViHanhChinhAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public DanhMucDVHCAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            ILogAppService iLogAppService
            )
        {
            _cacheManager = cacheManager;
            _iocResolver = iocResolver;
            _dvhcRepos = dvhcRepos;
            _userRepos = userRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _iLogAppService = iLogAppService;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> GetByUser(long userId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstDVHC = new List<DVHCOutputDto>();
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var query = (from up in _userRepos.GetAll()
                             join dvhc in _dvhcRepos.GetAll() on up.DonViHanhChinhId equals dvhc.Id
                             select new DVHCOutputDto
                             {
                                 Id = dvhc.Id,
                                 TenTinh = dvhc.TenTinh,
                                 MaTinh = dvhc.MaTinh,
                                 TenHuyen = dvhc.TenHuyen,
                                 MaHuyen = dvhc.MaHuyen,
                                 TenXa = dvhc.TenXa,
                                 MaXa = dvhc.MaXa,
                                 Name = dvhc.Name,
                                 Parent_id = dvhc.Parent_id,
                                 Level = dvhc.Level,
                                 Active = dvhc.Active,
                                 Year_Id = dvhc.Year_Id,
                                 TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                 ChildStatus = dvhc.Level == 4 ? 0 : 1
                             })
                             .WhereIf(userId > 0, x => x.Id.Equals(userId));
                lstDVHC = await query.ToListAsync();
                commonResponseDto.ReturnValue = lstDVHC;
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> CreateOrUpdate(DVHCInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        data.TenTinh = input.TenTinh;
                        data.MaTinh = input.MaTinh;
                        data.TenHuyen = input.TenHuyen;
                        data.MaHuyen = input.MaHuyen;
                        data.TenXa = input.TenXa;
                        data.MaXa = input.MaXa;
                        data.Name = input.Name;
                        data.Parent_id = input.Parent_id;
                        data.Level = input.Level;
                        data.Active = input.Active;
                        data.Year_Id = input.Year_Id;
                        data.TrangThaiDuyet = input.TrangThaiDuyet;
                        await _dvhcRepos.UpdateAsync(data);
                        //insert log
                        //var log = new LogInputDto
                        //{
                        //    UserId = currentUser.Id,
                        //    Describle = "sửa dữ liệu thông tin hồ chứa"
                        //};
                        //_iLogAppService.Create(log);
                    }
                    else
                    {
                        commonResponseDto.Message = "Đơn vị hành chính này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                }
                else
                {
                    var dvhc = input.MapTo<DVHCOutputDto>();
                    await _dvhcRepos.InsertAsync(dvhc);
                    //insert log
                    var log = new LogInputDto
                    {
                        UserId = currentUser.Id,
                        Describle = "Thêm dữ liệu thông tin hồ chứa"
                    };
                    _iLogAppService.Create(log);
                }
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var HoChua = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (HoChua != null)
                {
                    await _dvhcRepos.DeleteAsync(HoChua);
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                    //insert log
                    var log = new LogInputDto
                    {
                        UserId = currentUser.Id,
                        Describle = "Xoá dữ liệu đơn vị hành chính"
                    };
                    _iLogAppService.Create(log);
                }
                else
                {
                    commonResponseDto.Message = "Dơn vị hành chínhnày không tồn tại";
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
       
    }
}
