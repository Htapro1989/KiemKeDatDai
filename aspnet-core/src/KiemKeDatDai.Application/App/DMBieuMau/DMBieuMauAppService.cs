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
        private readonly IRepository<Bieu01TKKK, long> _bieu01TKKKRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Tinh, long> _bieu01TKKK_TinhRepos;
        private readonly IRepository<Bieu01TKKK_Vung, long> _bieu01TKKK_VungRepos;
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
            IRepository<Bieu01TKKK, long> bieu01TKKKRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Tinh, long> bieu01TKKK_TinhRepos,
            IRepository<Bieu01TKKK_Vung, long> bieu01TKKK_VungRepos,
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
            _bieu01TKKKRepos = bieu01TKKKRepos;
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_TinhRepos = bieu01TKKK_TinhRepos;
            _bieu01TKKK_VungRepos = bieu01TKKK_VungRepos;
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
                PagedResultDto<DMBieuMauOuputDto> pagedResultDto = new PagedResultDto<DMBieuMauOuputDto>();
                var query = (from bm in _dmbmRepos.GetAll()
                             select new DMBieuMauOuputDto
                             {
                                 Id = bm.Id,
                                 KyHieu = bm.KyHieu,
                                 NoiDung = bm.NoiDung,
                                 CapDVHC = bm.CapDVHC,
                                 //Active = bm.Active
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.NoiDung.ToLower().Contains(input.Filter.ToLower()));
                pagedResultDto.Items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x => x.CreationTime).ToListAsync();
                pagedResultDto.TotalCount = await query.CountAsync(); 
                commonResponseDto.ReturnValue = pagedResultDto;
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);   
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
                var dvhcObj = await _dvhcRepos.FirstOrDefaultAsync(dvhcId);
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
                Logger.Error(ex.Message);
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
                    var data = await _dmbmRepos.FirstOrDefaultAsync(input.Id);
                    if (data != null)
                    {
                        data.KyHieu = input.KyHieu;
                        data.NoiDung = input.NoiDung;
                        data.CapDVHC = input.CapDVHC;
                        //data.Active = input.Active;
                        await _dmbmRepos.UpdateAsync(data);
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
                }
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
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
                var objBM = await _dmbmRepos.FirstOrDefaultAsync(id);
                if (objBM != null)
                {
                    await _dmbmRepos.DeleteAsync(objBM);
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
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
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetDetailBieuByKyHieu(BieuMauDetailInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                switch (input.KyHieu)
                {
                    case "01/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case 1:
                                    {
                                        var data = await _bieu01TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);
                                        commonResponseDto.ReturnValue = data;
                                        break;
                                    }
                                case 2:
                                    {
                                        var data = await _bieu01TKKK_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);
                                        commonResponseDto.ReturnValue = data;
                                        break;
                                    }
                                case 3:
                                    {
                                        var data = await _bieu01TKKK_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);
                                        commonResponseDto.ReturnValue = data;
                                        break;
                                    }
                                case 4:
                                    {
                                        var data = await _bieu01TKKK_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);
                                        commonResponseDto.ReturnValue = data;
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
    }
}
