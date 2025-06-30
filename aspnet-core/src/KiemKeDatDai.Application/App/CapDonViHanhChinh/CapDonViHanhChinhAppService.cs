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
using KiemKeDatDai.AppCore.Utility;
using static KiemKeDatDai.CommonEnum;
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai.RisApplication
{
    [AbpAuthorize(PermissionNames.Pages_Administration_System_CapDvhc)]
    public class CapDonViHanhChinhAppService : KiemKeDatDaiAppServiceBase, ICapDonViHanhChinhAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<CapDVHC, long> _capDVHCRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public CapDonViHanhChinhAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<CapDVHC, long> capDVHCRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _capDVHCRepos = capDVHCRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        
        public async Task<CommonResponseDto> GetAll(CapDVHCDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var query = (from cap in _capDVHCRepos.GetAll()
                             select new CapDVHCOuputDto
                             {
                                 Id = cap.Id,
                                 MaCapDVHC = cap.MaCapDVHC,
                                 Name = cap.Name,
                                 Year = cap.Year,
                                 CapDVHCMin = cap.CapDVHCMin,
                                 Active = cap.Active,
                                 CreationTime = cap.CreationTime
                             })
                             .WhereIf((input.MaCapDVHC != null), x => x.MaCapDVHC == input.MaCapDVHC)
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.ToLower().Contains(input.Filter.ToLower()));
                
                var lstCapDvhc = await query.ToListAsync();

                if (lstCapDvhc.Count > 0 && input.LoaiCapDvhc == (int)LOAI_CAP_DVHC.BA_CAP) { 
                    lstCapDvhc = lstCapDvhc.Where(x => x.MaCapDVHC != (int)CAP_DVHC.HUYEN && x.MaCapDVHC != (int)CAP_DVHC.VUNG).ToList();
                }

                commonResponseDto.ReturnValue = lstCapDvhc;
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
                var obj = await _capDVHCRepos.FirstOrDefaultAsync(id);

                commonResponseDto.ReturnValue = obj;
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
        public async Task<CommonResponseDto> CreateOrUpdate(CapDVHCInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (input.Id != 0)
                {
                    var data = await _capDVHCRepos.FirstOrDefaultAsync(input.Id);

                    if (data != null)
                    {
                        data.MaCapDVHC = input.MaCapDVHC;
                        data.Name = input.Name;
                        data.Year = input.Year;
                        data.CapDVHCMin = input.CapDVHCMin;
                        data.Active = input.Active;

                        await _capDVHCRepos.UpdateAsync(data);
                    }
                }
                else
                {
                    var objdata = input.MapTo<CapDVHC>();

                    await _capDVHCRepos.InsertAsync(objdata);
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
                var objdata = await _capDVHCRepos.FirstOrDefaultAsync(id);

                if (objdata != null)
                {
                    await _capDVHCRepos.DeleteAsync(objdata);

                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Cấp đơn vị hành chính này không tồn tại";
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
        public async Task<CommonResponseDto> GetCapDVHC()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from cap in _capDVHCRepos.GetAll()
                             select new DropDownListDVHCDto
                             {
                                 Id = cap.Id,
                                 Ma = cap.MaCapDVHC.ToString(),
                                 Name = cap.Name,
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
    }
}
