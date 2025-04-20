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
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai.RisApplication
{
    [AbpAuthorize(PermissionNames.Pages_Administration_System_KyKiemKe)]
    public class DMKyKiemKeAppService : KiemKeDatDaiAppServiceBase, IDMKyKiemKeAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public DMKyKiemKeAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _dmKyThongKeKiemKeRepos = dmKyThongKeKiemKeRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        public async Task<CommonResponseDto> GetAll(string filter)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstBM = new List<DMKyKiemKeOuputDto>();
                var query = (from ky in _dmKyThongKeKiemKeRepos.GetAll()
                             select new DMKyKiemKeOuputDto
                             {
                                 Id = ky.Id,
                                 Ma = ky.Ma,
                                 Name = ky.Name,
                                 Year = ky.Year,
                                 Active = ky.Active,
                                 CreationTime = ky.CreationTime
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Ma.ToLower().Contains(filter.ToLower()))
                             .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.ToLower().Contains(filter.ToLower()));
                commonResponseDto.ReturnValue = await query.OrderByDescending(x => x.Year).ToListAsync();
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
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                commonResponseDto.ReturnValue = await _dmKyThongKeKiemKeRepos.FirstOrDefaultAsync(id);
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
        public async Task<CommonResponseDto> CreateOrUpdate(DMKyKiemKeInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _dmKyThongKeKiemKeRepos.FirstOrDefaultAsync(input.Id);
                    if (data != null)
                    {
                        data.Ma = input.Ma;
                        data.Name = input.Name;
                        data.Year = input.Year;
                        data.Active = input.Active;
                        await _dmKyThongKeKiemKeRepos.UpdateAsync(data);
                    }
                }
                else
                {
                    var objdata = input.MapTo<KyThongKeKiemKe>();
                    await _dmKyThongKeKiemKeRepos.InsertAsync(objdata);
                }
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

        [HttpDelete]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var objdata = await _dmKyThongKeKiemKeRepos.FirstOrDefaultAsync(id);
                if (objdata != null)
                {
                    await _dmKyThongKeKiemKeRepos.DeleteAsync(objdata);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Kỳ thống kê kiểm kê này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
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
