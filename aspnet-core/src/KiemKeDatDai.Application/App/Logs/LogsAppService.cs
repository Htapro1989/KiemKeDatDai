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
    [AbpAuthorize]
    public class LogsAppService : KiemKeDatDaiAppServiceBase, ILogsAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<Logs, long> _logsRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public LogsAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<Logs, long> logsRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logsRepos = logsRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
        }

        public async Task<CommonResponseDto> GetAll(LogsDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                PagedResultDto<LogsOuputDto> pagedResultDto = new PagedResultDto<LogsOuputDto>();
                var query = (from log in _logsRepos.GetAll()
                             select new LogsOuputDto
                             {
                                 Id = log.Id,
                                 UserId = log.UserId,
                                 UserName = log.UserName,
                                 FullName = log.FullName,
                                 Action = log.Action,
                                 Description = log.Description,
                                 CreationTime = log.CreationTime,
                                 Timestamp = log.Timestamp
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.UserName.ToLower().Contains(input.Filter.ToLower())
                                                                                || x.Description.ToLower().Contains(input.Filter.ToLower()))
                             .WhereIf(input.TuNgay != null, x => x.Timestamp >= input.TuNgay)
                             .WhereIf(input.DenNgay != null, x => x.Timestamp <= input.DenNgay);

                var totalCount = await query.CountAsync();
                var lstData = await query.OrderBy(x => x.CreationTime)
                                    .Skip(input.SkipCount)
                                    .Take(input.MaxResultCount)
                                    .ToListAsync();

                commonResponseDto.ReturnValue = new PagedResultDto<LogsOuputDto>()
                {
                    Items = lstData,
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

        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (id <= 0)
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Id không hợp lệ";
                    return commonResponseDto;
                }

                var log = await _logsRepos.FirstOrDefaultAsync(id);

                if (log != null)
                {
                    commonResponseDto.ReturnValue = log;
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
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }

        public async Task<CommonResponseDto> CreateOrUpdate(LogsInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (input.Id != 0)
                {
                    var data = await _logsRepos.FirstOrDefaultAsync(input.Id);

                    if (data != null)
                    {
                        data.UserId = input.UserId;
                        data.UserName = input.UserName;
                        data.FullName = input.FullName;
                        data.Action = input.Action;
                        data.Description = input.Description;
                        data.Timestamp = input.Timestamp;

                        await _logsRepos.UpdateAsync(data);
                    }
                }
                else
                {
                    var objdata = input.MapTo<Logs>();

                    await _logsRepos.InsertAsync(objdata);
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
                var objdata = await _logsRepos.FirstOrDefaultAsync(id);

                if (objdata != null)
                {
                    await _logsRepos.DeleteAsync(objdata);

                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Nhật ký này không tồn tại";
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
