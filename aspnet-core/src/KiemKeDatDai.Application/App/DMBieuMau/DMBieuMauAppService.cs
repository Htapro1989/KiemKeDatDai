﻿using Abp.Application.Services.Dto;
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
    public class DMBieuMauAppService: KiemKeDatDaiAppServiceBase, IDMBieuMauAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DM_BieuMau, long> _dmbmRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<DM_DVCH_BM, long> _dmdvhcbmRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public DMBieuMauAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DM_BieuMau, long> dmbmRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<DM_DVCH_BM, long> dmdvhcbmRepos,
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
            _dmbmRepos = dmbmRepos;
            _dmbmRepos = dmbmRepos;
            _dvhcRepos = dvhcRepos;
            _dmdvhcbmRepos = dmdvhcbmRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetAll(DMBieuMauDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstBM = new List<DMBieuMauOuputDto>();
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var query = (from bm in _dmbmRepos.GetAll()
                             select new DMBieuMauOuputDto
                             {
                                 Id = bm.Id,
                                 KyHieu = bm.KyHieu,
                                 NoiDung = bm.NoiDung,
                                 CapDVHC = bm.CapDVHC,
                                 Activated = bm.Activated
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.NoiDung.ToLower().Contains(input.Filter.ToLower()));
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
        public async Task<CommonResponseDto> GetByDVHC(long dvhcId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstBM = new List<DMBieuMauOuputDto>();
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var dvhcObj = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == dvhcId);
                var dvhcLevel = dvhcObj != null ? dvhcObj.CapDVHCId : 0;
                var query = (from bm in _dmbmRepos.GetAll()
                             join dmdvhcbm in _dmdvhcbmRepos.GetAll() on bm.Id equals dmdvhcbm.BieuMauId
                             where dmdvhcbm.CapDVHCId == dvhcLevel
                             select new DMBieuMauOuputDto
                             {
                                 Id = bm.Id,
                                 KyHieu = bm.KyHieu,
                                 NoiDung = bm.NoiDung,
                                 CapDVHC = bm.CapDVHC,
                                 Activated = bm.Activated
                             });
                lstBM = await query.ToListAsync();
                commonResponseDto.ReturnValue = lstBM;
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
        public async Task<CommonResponseDto> CreateOrUpdate(DMBieuMauInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _dmbmRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        data.KyHieu = input.KyHieu;
                        data.NoiDung = input.NoiDung;
                        data.CapDVHC = input.CapDVHC;
                        data.Activated = input.Activated;
                        await _dmbmRepos.UpdateAsync(data);
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
                    var objBM = input.MapTo<DMBieuMauInputDto>();
                    await _dmbmRepos.InsertAsync(objBM);
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
                var objBM = await _dmbmRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (objBM != null)
                {
                    await _dmbmRepos.DeleteAsync(objBM);
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
                    commonResponseDto.Message = "Biểu mẫu này không tồn tại";
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
