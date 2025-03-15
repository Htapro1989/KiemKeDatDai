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
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Transactions;

namespace KiemKeDatDai.App.DMBieuMau
{
    public class BaoCaoAppService : KiemKeDatDaiAppServiceBase, IBaoCaoAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public BaoCaoAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _dvhcRepos = dvhcRepos;
            _dmKyThongKeKiemKeRepos = dmKyThongKeKiemKeRepos;
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize]
        public async Task<CommonResponseDto> NopBaoCao(long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(x=>x.Ma == currentUser.DonViHanhChinhCode && x.Year == year);
                    if (objdata != null)
                    {
                        //if (objdata.SoDVHCDaDuyet < objdata.SoDVHCCon && objdata.CapDVHCId != 4)
                        //{
                        //    commonResponseDto.Message = "Chưa duyệt hết các ĐVHC trực thuộc";
                        //    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        //}
                        objdata.NgayGui = DateTime.Now;
                        objdata.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHO_DUYET;
                        await _dvhcRepos.UpdateAsync(objdata);
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    }
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }
    }
}
