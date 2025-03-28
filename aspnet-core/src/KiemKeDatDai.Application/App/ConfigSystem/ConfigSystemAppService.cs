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
using Microsoft.Extensions.Configuration;

namespace KiemKeDatDai.App.DMBieuMau
{
    public class ConfigSystemAppService : KiemKeDatDaiAppServiceBase, IConfigSystemAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<ConfigSystem, long> _configSystemRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;
        private readonly IConfigurationRoot _appConfiguration;

        private readonly ICache mainCache;

        public ConfigSystemAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<ConfigSystem, long> configSystemRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _configSystemRepos = configSystemRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetAll(string filter)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from con in _configSystemRepos.GetAll()
                             select new ConfigSystem
                             {
                                 Id = con.Id,
                                 expired_auth = con.expired_auth,
                                 server_file_upload = con.server_file_upload,
                                 Active = con.Active,
                                 CreationTime = con.CreationTime
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
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objdata = await _configSystemRepos.FirstOrDefaultAsync(id);
                commonResponseDto.ReturnValue = objdata;
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
        public async Task<CommonResponseDto> CreateOrUpdate(ConfigSystem input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _configSystemRepos.FirstOrDefaultAsync(input.Id);
                    if (data != null)
                    {
                        //var section = _appConfiguration.GetSection("App");
                        //section["ExpiredTimeToken"] = data.expired_auth.ToString();
                        //_appConfiguration.Reload();
                        data.expired_auth = input.expired_auth;
                        data.server_file_upload = input.server_file_upload;
                        data.Active = input.Active;
                        await _configSystemRepos.UpdateAsync(data);
                    }
                }
                else
                {
                    var objdata = input.MapTo<ConfigSystem>();
                    await _configSystemRepos.InsertAsync(objdata);
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

        [AbpAuthorize]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var objdata = await _configSystemRepos.FirstOrDefaultAsync(id);
                if (objdata != null)
                {
                    await _configSystemRepos.DeleteAsync(objdata);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Cấu hình này không tồn tại";
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
        public async Task<int> GetByActive()
        {
            try
            {
                var objdata = await _configSystemRepos.FirstOrDefaultAsync(x => x.Active == true);
                return objdata.expired_auth.Value;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return 0;
        }
    }
}
