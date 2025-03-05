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

namespace KiemKeDatDai.App.DMBieuMau
{
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
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetAll(DMKyKiemKeDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstBM = new List<DMKyKiemKeOuputDto>();
                PagedResultDto<DMKyKiemKeDto> pagedResultDto = new PagedResultDto<DMKyKiemKeDto>();
                var query = (from ky in _dmKyThongKeKiemKeRepos.GetAll()
                             select new DMKyKiemKeOuputDto
                             {
                                 Id = ky.Id,
                                 Ma = ky.Ma,
                                 Name = ky.Name,
                                 Year = ky.Year,
                                 Active = ky.Active
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Ma.ToLower().Contains(input.Filter.ToLower()))
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.ToLower().Contains(input.Filter.ToLower()))
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Year == input.Year);
                lstBM = await query.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x => x.CreationTime).ToListAsync();
                var totalCout = await query.CountAsync();
                pagedResultDto.TotalCount = totalCout;
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
                var objKyKiemKe = await _dmKyThongKeKiemKeRepos.FirstOrDefaultAsync(x => x.Id == id);
                commonResponseDto.ReturnValue = objKyKiemKe;
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
        public async Task<CommonResponseDto> CreateOrUpdate(DMKyKiemKeInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _dmKyThongKeKiemKeRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        data.Ma = input.Ma;
                        data.Name = input.Name;
                        data.Year = input.Year;
                        data.Active = input.Active;
                        await _dmKyThongKeKiemKeRepos.UpdateAsync(data);
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
                        commonResponseDto.Message = "Xảy ra lỗi trong quá tình thêm mới!";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                }
                else
                {
                    var objdata = input.MapTo<KyThongKeKiemKe>();
                    await _dmKyThongKeKiemKeRepos.InsertAsync(objdata);
                    //insert log
                    //var log = new LogInputDto
                    //{
                    //    UserId = currentUser.Id,
                    //    Describle = "Thêm dữ liệu thông tin hồ chứa"
                    //};
                    //_iLogAppService.Create(log);
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
                var objdata = await _dmKyThongKeKiemKeRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (objdata != null)
                {
                    await _dmKyThongKeKiemKeRepos.DeleteAsync(objdata);
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                    //insert log
                    //var log = new LogInputDto
                    //{
                    //    UserId = currentUser.Id,
                    //    Describle = "Xoá dữ liệu đơn vị hành chính"
                    //};
                    //_iLogAppService.Create(log);
                }
                else
                {
                    commonResponseDto.Message = "Kỳ thống kê kiểm kê này không tồn tại";
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
