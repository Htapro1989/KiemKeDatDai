using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Abp.Threading;
using KiemKeDatDai.AppCore.Log.Dto;
using KiemKeDatDai.AppCore.Log;
using KiemKeDatDai.AppCore.Utility;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace KiemKeDatDai.App.Log
{
    [AbpAuthorize]
    public class LogAppService : KiemKeDatDaiAppServiceBase, ILogAppService
    {
        private readonly IRepository<KKDD_Log, long> _logRepos;
        private readonly IRepository<User, long> _userRepos;

        private readonly ICache mainCache;

        public LogAppService(
            IRepository<KKDD_Log, long> logRepos,
            IRepository<User, long> userRepos
            )
        {
            _logRepos = logRepos;
            _userRepos = userRepos;
        }


        [AbpAuthorize]
        public CommonResponseDto GetAll(LogDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from log in _logRepos.GetAll()
                             join user in _userRepos.GetAll() on log.Id equals user.Id
                             select new LogOutputDto
                             {
                                 Id = log.Id,
                                 UserId = log.UserId,
                                 UserName = user.Name,
                                 Describle = log.Describle,
                                 CreationTime = log.CreationTime
                             });
                var lstlog = query.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x => x.CreationTime).ToList();
                var totalCout = lstlog.Count;
                PagedResultDto<LogOutputDto> pagedResultDto = new PagedResultDto<LogOutputDto>();
                pagedResultDto.TotalCount = totalCout;
                pagedResultDto.Items = lstlog;
                commonResponseDto.ReturnValue = pagedResultDto;
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
        public CommonResponseDto Create(LogInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var log = input.MapTo<LogInputDto>();
                _logRepos.InsertAsync(log);
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
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var log = await _logRepos.FirstOrDefaultAsync(x => x.Id == id);
                commonResponseDto.ReturnValue = log;
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
                var log = await _logRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (log != null)
                {
                    await _logRepos.DeleteAsync(log);
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Log không tồn tại";
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
