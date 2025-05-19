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
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Hosting;
using NuGet.Protocol;
using KiemKeDatDai.Authorization;
using KiemKeDatDai.Configuration;

namespace KiemKeDatDai.RisApplication
{
    [AbpAuthorize(PermissionNames.Pages_Administration_System_ConfigSystem)]
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
        private readonly IWritableOptions<ConfigSystemTime> _options;

        private readonly ICache mainCache;

        public ConfigSystemAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<ConfigSystem, long> configSystemRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            IWritableOptions<ConfigSystemTime> options
            )
        {
            _configSystemRepos = configSystemRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _options = options;
        }
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
                                 JsonConfigSystem = con.JsonConfigSystem,
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

        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                commonResponseDto.ReturnValue = await _configSystemRepos.FirstOrDefaultAsync(id);
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

        public async Task<CommonResponseDto> CreateOrUpdate(ConfigSytemInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (input.Id != 0)
                {
                    var data = await _configSystemRepos.FirstOrDefaultAsync(input.Id);

                    if (data != null)
                    {
                        if (input.expired_auth != null)
                        {
                            _options.Update(opt =>
                            {
                                opt.ExpiredTimeToken = input.expired_auth.Value.ToString();
                            });

                            data.expired_auth = input.expired_auth;
                        }

                        var jsonConfigSystem = JsonConvert.DeserializeObject<JsonConfigSytem>(data.JsonConfigSystem);

                        if (jsonConfigSystem != null)
                        {
                            jsonConfigSystem.IsRequiredFileDGN = input.IsRequiredFileDGN;
                            jsonConfigSystem.TimeUpload = input.TimeUpload != null ? input.TimeUpload : 1;
                        }
                        else
                            jsonConfigSystem = new JsonConfigSytem
                            {
                                IsRequiredFileDGN = input.IsRequiredFileDGN,
                                TimeUpload = input.TimeUpload != null ? input.TimeUpload : 1
                            };

                        data.JsonConfigSystem = jsonConfigSystem.ToJson();
                        data.Active = input.Active;

                        await _configSystemRepos.UpdateAsync(data);
                    }
                }
                else
                {
                    var objdata = input.MapTo<ConfigSystem>();

                    objdata.JsonConfigSystem = (new JsonConfigSytem { 
                        IsRequiredFileDGN = input.IsRequiredFileDGN,
                        TimeUpload = input.TimeUpload != null ? input.TimeUpload : 1
                    }).ToJson();

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

        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
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
