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
using static System.Runtime.InteropServices.JavaScript.JSType;
using KiemKeDatDai.App.Huyen.Dto;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using Microsoft.VisualBasic;
using Aspose.Cells;
using System.Dynamic;
using KiemKeDatDai.App.DMBieuMau.Dto;
using System.Reflection.PortableExecutable;

namespace KiemKeDatDai.App.DMBieuMau
{
    public class DMBieuMauAppService : KiemKeDatDaiAppServiceBase, IDMBieuMauAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DM_BieuMau, long> _dmbmRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<DM_DVCH_BM, long> _dmdvhcbmRepos;
        private readonly IRepository<Data_Commune, long> _dcRepos;
        private readonly IRepository<Data_BienDong, long> _dbdRepos;

        #region biểu mẫu
        private readonly IRepository<Bieu01TKKK, long> _bieu01TKKKRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Tinh, long> _bieu01TKKK_TinhRepos;
        private readonly IRepository<Bieu01TKKK_Vung, long> _bieu01TKKK_VungRepos;

        private readonly IRepository<Bieu02TKKK, long> _bieu02TKKKRepos;
        private readonly IRepository<Bieu02TKKK_Xa, long> _bieu02TKKK_XaRepos;
        private readonly IRepository<Bieu02TKKK_Huyen, long> _bieu02TKKK_HuyenRepos;
        private readonly IRepository<Bieu02TKKK_Tinh, long> _bieu02TKKK_TinhRepos;
        private readonly IRepository<Bieu02TKKK_Vung, long> _bieu02TKKK_VungRepos;

        private readonly IRepository<Bieu03TKKK, long> _bieu03TKKKRepos;
        private readonly IRepository<Bieu03TKKK_Huyen, long> _bieu03TKKK_HuyenRepos;
        private readonly IRepository<Bieu03TKKK_Tinh, long> _bieu03TKKK_TinhRepos;
        private readonly IRepository<Bieu03TKKK_Vung, long> _bieu03TKKK_VungRepos;

        private readonly IRepository<Bieu04TKKK, long> _bieu04TKKKRepos;
        private readonly IRepository<Bieu04TKKK_Xa, long> _bieu04TKKK_XaRepos;
        private readonly IRepository<Bieu04TKKK_Huyen, long> _bieu04TKKK_HuyenRepos;
        private readonly IRepository<Bieu04TKKK_Tinh, long> _bieu04TKKK_TinhRepos;
        private readonly IRepository<Bieu04TKKK_Vung, long> _bieu04TKKK_VungRepos;

        private readonly IRepository<Bieu05TKKK, long> _bieu05TKKKRepos;
        private readonly IRepository<Bieu05TKKK_Xa, long> _bieu05TKKK_XaRepos;
        private readonly IRepository<Bieu05TKKK_Huyen, long> _bieu05TKKK_HuyenRepos;
        private readonly IRepository<Bieu05TKKK_Tinh, long> _bieu05TKKK_TinhRepos;
        private readonly IRepository<Bieu05TKKK_Vung, long> _bieu05TKKK_VungRepos;

        private readonly IRepository<Bieu01KKSL, long> _bieu01KKSLRepos;
        private readonly IRepository<Bieu01KKSL_Xa, long> _bieu01KKSL_XaRepos;
        private readonly IRepository<Bieu01KKSL_Huyen, long> _bieu01KKSL_HuyenRepos;
        private readonly IRepository<Bieu01KKSL_Tinh, long> _bieu01KKSL_TinhRepos;
        private readonly IRepository<Bieu01KKSL_Vung, long> _bieu01KKSL_VungRepos;

        private readonly IRepository<Bieu02KKSL, long> _bieu02KKSLRepos;
        private readonly IRepository<Bieu02KKSL_Xa, long> _bieu02KKSL_XaRepos;
        private readonly IRepository<Bieu02KKSL_Huyen, long> _bieu02KKSL_HuyenRepos;
        private readonly IRepository<Bieu02KKSL_Tinh, long> _bieu02KKSL_TinhRepos;
        private readonly IRepository<Bieu02KKSL_Vung, long> _bieu02KKSL_VungRepos;

        private readonly IRepository<Bieu01aKKNLT, long> _bieu01aKKNLTRepos;
        private readonly IRepository<Bieu01aKKNLT_Xa, long> _bieu01aKKNLT_XaRepos;
        private readonly IRepository<Bieu01aKKNLT_Huyen, long> _bieu01aKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01aKKNLT_Tinh, long> _bieu01aKKNLT_TinhRepos;
        private readonly IRepository<Bieu01aKKNLT_Vung, long> _bieu01aKKNLT_VungRepos;

        private readonly IRepository<Bieu01bKKNLT, long> _bieu01bKKNLTRepos;
        private readonly IRepository<Bieu01bKKNLT_Xa, long> _bieu01bKKNLT_XaRepos;
        private readonly IRepository<Bieu01bKKNLT_Huyen, long> _bieu01bKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01bKKNLT_Tinh, long> _bieu01bKKNLT_TinhRepos;
        private readonly IRepository<Bieu01bKKNLT_Vung, long> _bieu01bKKNLT_VungRepos;

        private readonly IRepository<Bieu01cKKNLT, long> _bieu01cKKNLTRepos;
        private readonly IRepository<Bieu01cKKNLT_Xa, long> _bieu01cKKNLT_XaRepos;
        private readonly IRepository<Bieu01cKKNLT_Huyen, long> _bieu01cKKNLT_HuyenRepos;
        private readonly IRepository<Bieu01cKKNLT_Tinh, long> _bieu01cKKNLT_TinhRepos;
        private readonly IRepository<Bieu01cKKNLT_Vung, long> _bieu01cKKNLT_VungRepos;

        private readonly IRepository<Bieu06TKKKQPAN, long> _bieu06TKKKQPANRepos;
        private readonly IRepository<Bieu06TKKKQPAN_Tinh, long> _bieu06TKKKQPAN_TinhRepos;
        private readonly IRepository<BieuPhuLucIII, long> _bieuPhuLucIIIRepos;
        private readonly IRepository<BieuPhuLucIV, long> _bieuPhuLucIVRepos;
        #endregion

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
            IRepository<Data_Commune, long> dcRepos,
            IRepository<Data_BienDong, long> dbdRepos,

        #region biểu mẫu
            IRepository<Bieu01TKKK, long> bieu01TKKKRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Tinh, long> bieu01TKKK_TinhRepos,
            IRepository<Bieu01TKKK_Vung, long> bieu01TKKK_VungRepos,

            IRepository<Bieu02TKKK, long> bieu02TKKKRepos,
            IRepository<Bieu02TKKK_Xa, long> bieu02TKKK_XaRepos,
            IRepository<Bieu02TKKK_Huyen, long> bieu02TKKK_HuyenRepos,
            IRepository<Bieu02TKKK_Tinh, long> bieu02TKKK_TinhRepos,
            IRepository<Bieu02TKKK_Vung, long> bieu02TKKK_VungRepos,

            IRepository<Bieu03TKKK, long> bieu03TKKKRepos,
            IRepository<Bieu03TKKK_Huyen, long> bieu03TKKK_HuyenRepos,
            IRepository<Bieu03TKKK_Tinh, long> bieu03TKKK_TinhRepos,
            IRepository<Bieu03TKKK_Vung, long> bieu03TKKK_VungRepos,

            IRepository<Bieu04TKKK, long> bieu04TKKKRepos,
            IRepository<Bieu04TKKK_Xa, long> bieu04TKKK_XaRepos,
            IRepository<Bieu04TKKK_Huyen, long> bieu04TKKK_HuyenRepos,
            IRepository<Bieu04TKKK_Tinh, long> bieu04TKKK_TinhRepos,
            IRepository<Bieu04TKKK_Vung, long> bieu04TKKK_VungRepos,

            IRepository<Bieu05TKKK, long> bieu05TKKKRepos,
            IRepository<Bieu05TKKK_Xa, long> bieu05TKKK_XaRepos,
            IRepository<Bieu05TKKK_Huyen, long> bieu05TKKK_HuyenRepos,
            IRepository<Bieu05TKKK_Tinh, long> bieu05TKKK_TinhRepos,
            IRepository<Bieu05TKKK_Vung, long> bieu05TKKK_VungRepos,

            IRepository<Bieu01KKSL, long> bieu01KKSLRepos,
            IRepository<Bieu01KKSL_Xa, long> bieu01KKSL_XaRepos,
            IRepository<Bieu01KKSL_Huyen, long> bieu01KKSL_HuyenRepos,
            IRepository<Bieu01KKSL_Tinh, long> bieu01KKSL_TinhRepos,
            IRepository<Bieu01KKSL_Vung, long> bieu01KKSL_VungRepos,

            IRepository<Bieu02KKSL, long> bieu02KKSLRepos,
            IRepository<Bieu02KKSL_Xa, long> bieu02KKSL_XaRepos,
            IRepository<Bieu02KKSL_Huyen, long> bieu02KKSL_HuyenRepos,
            IRepository<Bieu02KKSL_Tinh, long> bieu02KKSL_TinhRepos,
            IRepository<Bieu02KKSL_Vung, long> bieu02KKSL_VungRepos,

            IRepository<Bieu01aKKNLT, long> bieu01aKKNLTRepos,
            IRepository<Bieu01aKKNLT_Xa, long> bieu01aKKNLT_XaRepos,
            IRepository<Bieu01aKKNLT_Huyen, long> bieu01aKKNLT_HuyenRepos,
            IRepository<Bieu01aKKNLT_Tinh, long> bieu01aKKNLT_TinhRepos,
            IRepository<Bieu01aKKNLT_Vung, long> bieu01aKKNLT_VungRepos,

            IRepository<Bieu01cKKNLT, long> bieu01cKKNLTRepos,
            IRepository<Bieu01cKKNLT_Xa, long> bieu01cKKNLT_XaRepos,
            IRepository<Bieu01cKKNLT_Huyen, long> bieu01cKKNLT_HuyenRepos,
            IRepository<Bieu01cKKNLT_Tinh, long> bieu01cKKNLT_TinhRepos,
            IRepository<Bieu01cKKNLT_Vung, long> bieu01cKKNLT_VungRepos,

            IRepository<Bieu01bKKNLT, long> bieu01bKKNLTRepos,
            IRepository<Bieu01bKKNLT_Xa, long> bieu01bKKNLT_XaRepos,
            IRepository<Bieu01bKKNLT_Huyen, long> bieu01bKKNLT_HuyenRepos,
            IRepository<Bieu01bKKNLT_Tinh, long> bieu01bKKNLT_TinhRepos,
            IRepository<Bieu01bKKNLT_Vung, long> bieu01bKKNLT_VungRepos,

            IRepository<Bieu06TKKKQPAN, long> bieu06TKKKQPANRepos,
            IRepository<Bieu06TKKKQPAN_Tinh, long> bieu06TKKKQPAN_TinhRepos,

            IRepository<BieuPhuLucIII, long> bieuPhuLucIIIRepos,
            IRepository<BieuPhuLucIV, long> bieuPhuLucIVRepos,
        #endregion

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
            _dvhcRepos = dvhcRepos;
            _dmdvhcbmRepos = dmdvhcbmRepos;
            _dcRepos = dcRepos;
            _dbdRepos = dbdRepos;

            #region biểu mẫu
            _bieu01TKKKRepos = bieu01TKKKRepos;
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_TinhRepos = bieu01TKKK_TinhRepos;
            _bieu01TKKK_VungRepos = bieu01TKKK_VungRepos;

            _bieu02TKKKRepos = bieu02TKKKRepos;
            _bieu02TKKK_XaRepos = bieu02TKKK_XaRepos;
            _bieu02TKKK_HuyenRepos = bieu02TKKK_HuyenRepos;
            _bieu02TKKK_TinhRepos = bieu02TKKK_TinhRepos;
            _bieu02TKKK_VungRepos = bieu02TKKK_VungRepos;

            _bieu03TKKKRepos = bieu03TKKKRepos;
            _bieu03TKKK_HuyenRepos = bieu03TKKK_HuyenRepos;
            _bieu03TKKK_TinhRepos = bieu03TKKK_TinhRepos;
            _bieu03TKKK_VungRepos = bieu03TKKK_VungRepos;

            _bieu04TKKKRepos = bieu04TKKKRepos;
            _bieu04TKKK_XaRepos = bieu04TKKK_XaRepos;
            _bieu04TKKK_HuyenRepos = bieu04TKKK_HuyenRepos;
            _bieu04TKKK_TinhRepos = bieu04TKKK_TinhRepos;
            _bieu04TKKK_VungRepos = bieu04TKKK_VungRepos;

            _bieu05TKKKRepos = bieu05TKKKRepos;
            _bieu05TKKK_XaRepos = bieu05TKKK_XaRepos;
            _bieu05TKKK_HuyenRepos = bieu05TKKK_HuyenRepos;
            _bieu05TKKK_TinhRepos = bieu05TKKK_TinhRepos;
            _bieu05TKKK_VungRepos = bieu05TKKK_VungRepos;

            _bieu01KKSLRepos = bieu01KKSLRepos;
            _bieu01KKSL_XaRepos = bieu01KKSL_XaRepos;
            _bieu01KKSL_HuyenRepos = bieu01KKSL_HuyenRepos;
            _bieu01KKSL_TinhRepos = bieu01KKSL_TinhRepos;
            _bieu01KKSL_VungRepos = bieu01KKSL_VungRepos;

            _bieu02KKSLRepos = bieu02KKSLRepos;
            _bieu02KKSL_XaRepos = bieu02KKSL_XaRepos;
            _bieu02KKSL_HuyenRepos = bieu02KKSL_HuyenRepos;
            _bieu02KKSL_TinhRepos = bieu02KKSL_TinhRepos;
            _bieu02KKSL_VungRepos = bieu02KKSL_VungRepos;

            _bieu01aKKNLTRepos = bieu01aKKNLTRepos;
            _bieu01aKKNLT_XaRepos = bieu01aKKNLT_XaRepos;
            _bieu01aKKNLT_HuyenRepos = bieu01aKKNLT_HuyenRepos;
            _bieu01aKKNLT_TinhRepos = bieu01aKKNLT_TinhRepos;
            _bieu01aKKNLT_VungRepos = bieu01aKKNLT_VungRepos;

            _bieu01cKKNLTRepos = bieu01cKKNLTRepos;
            _bieu01cKKNLT_XaRepos = bieu01cKKNLT_XaRepos;
            _bieu01cKKNLT_HuyenRepos = bieu01cKKNLT_HuyenRepos;
            _bieu01cKKNLT_TinhRepos = bieu01cKKNLT_TinhRepos;
            _bieu01cKKNLT_VungRepos = bieu01cKKNLT_VungRepos;

            _bieu01bKKNLTRepos = bieu01bKKNLTRepos;
            _bieu01bKKNLT_XaRepos = bieu01bKKNLT_XaRepos;
            _bieu01bKKNLT_HuyenRepos = bieu01bKKNLT_HuyenRepos;
            _bieu01bKKNLT_TinhRepos = bieu01bKKNLT_TinhRepos;
            _bieu01bKKNLT_VungRepos = bieu01bKKNLT_VungRepos;

            _bieu06TKKKQPANRepos = bieu06TKKKQPANRepos;
            _bieu06TKKKQPAN_TinhRepos = bieu06TKKKQPAN_TinhRepos;
            _bieuPhuLucIIIRepos = bieuPhuLucIIIRepos;
            _bieuPhuLucIVRepos = bieuPhuLucIVRepos;
            #endregion

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
                                 CreationTime = bm.CreationTime,
                                 //Active = bm.Active
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.NoiDung.ToLower().Contains(input.Filter.ToLower()));
                pagedResultDto.Items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x => x.CreationTime).ToListAsync();
                pagedResultDto.TotalCount = await query.CountAsync();
                commonResponseDto.ReturnValue = pagedResultDto;
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
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                }
                else
                {
                    var objBM = input.MapTo<DMBieuMauInputDto>();
                    await _dmbmRepos.InsertAsync(objBM);
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
                var objBM = await _dmbmRepos.FirstOrDefaultAsync(id);
                if (objBM != null)
                {
                    await _dmbmRepos.DeleteAsync(objBM);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Biểu mẫu này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
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
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetDetailBieuByKyHieu(BieuMauDetailInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                string _tenxa = "";
                string _tenHuyen = "";
                string _tenTinh = "";
                switch ((input.CapDVHC))
                {
                    case (int)CAP_DVHC.TINH:
                        _tenTinh = _dvhcRepos.Single(x => x.Ma == input.MaDVHC && x.Year == input.Year).Name;
                        break;
                    case (int)CAP_DVHC.HUYEN:
                        var _huyen = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.MaDVHC && x.Year == input.Year);
                        _tenHuyen = _huyen != null ? _huyen.Name : "";
                        _tenTinh = _huyen != null ? _dvhcRepos.Single(x => x.Ma == _huyen.MaTinh && x.Year == input.Year).Name : "";
                        break;
                    case (int)CAP_DVHC.XA:
                        var _xa = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.MaDVHC && x.Year == input.Year);
                        _tenxa = _xa != null ? _xa.Name : "";
                        var huyen = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == _xa.MaHuyen && x.Year == input.Year);
                        _tenHuyen = huyen != null ? huyen.Name : "";
                        _tenTinh = huyen != null ? _dvhcRepos.Single(x => x.Ma == huyen.MaTinh && x.Year == input.Year).Name : "";
                        break;
                    default:
                        break;
                }
                switch (input.KyHieu)
                {
                    case "01/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu01TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu01TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu01TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data 
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu01TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu01TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "02/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu02TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu02TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu02TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data 
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu02TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu02TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "03/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu03TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu03TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu03TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data 
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu03TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "04/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu04TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu04TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu04TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data 
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu04TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu04TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "05/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu05TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu05TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu05TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data 
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu05TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu05TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "06/TKKKQPAN":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu06TKKKQPANRepos.GetAllListAsync(x => x.Year == input.Year);
                                    commonResponseDto.ReturnValue = data;
                                    break;
                                }
                            //case (int)CAP_DVHC.VUNG:
                            //    {
                            //        var data = await _bieu05TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);
                            //        commonResponseDto.ReturnValue = data;
                            //        break;
                            //    }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu06TKKKQPAN_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01/KKSL":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01KKSLRepos.GetAllListAsync(x => x.Year == input.Year);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01KKSL_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01KKSL_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01KKSL_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01KKSL_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "02/KKSL":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu02KKSLRepos.GetAllListAsync(x => x.Year == input.Year);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu02KKSL_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu02KKSL_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu02KKSL_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu02KKSL_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01a/KKNLT":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01aKKNLTRepos.GetAllListAsync(x => x.Year == input.Year);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01aKKNLT_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01aKKNLT_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01aKKNLT_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);
                                        commonResponseDto.ReturnValue = new
                                        {
                                            tenXa = _tenxa,
                                            tenHuyen = _tenHuyen,
                                            tenTinh = _tenTinh,
                                            data
                                        };
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01aKKNLT_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01b/KKNLT":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01bKKNLTRepos.GetAllListAsync(x => x.Year == input.Year);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01bKKNLT_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01bKKNLT_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01bKKNLT_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01bKKNLT_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01c/KKNLT":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01cKKNLTRepos.GetAllListAsync(x => x.Year == input.Year);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01cKKNLT_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01cKKNLT_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01cKKNLT_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01cKKNLT_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "PL.III":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = new BieuPhuLucIIIOutputDto();
                                    //var dvhcPL3 = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.MaDVHC && x.Year == input.Year);
                                    var queryPL3 = (from item in _dcRepos.GetAll()
                                                    where item.MaXa == input.MaDVHC && item.Year == input.Year
                                                    select new BieuPhuLucIIIDto()
                                                    {
                                                        DienTich = item.DienTich,
                                                        MaLoaiDatHienTrang = item.MucDichSuDung,
                                                        MaLoaiDatKyTruoc = item.MucDichSuDungKyTruoc,
                                                        MaLoaiDatSuDungKetHop = "",
                                                        MaDoiTuongHienTrang = item.MaDoiTuong,
                                                        MaDoiTuongKyTruoc = item.MaDoiTuongKyTruoc,
                                                        MaKhuVucTongHop = "",
                                                        GhiChu = "",
                                                        MaXa = item.MaXa
                                                    });
                                    data.BieuPhuLucIIIDtos = await queryPL3.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
                                    //result.BieuPhuLucIIIs = await query.ToListAsync();
                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    //var dataPLIII = await _bieuPhuLucIIIRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);
                                    //commonResponseDto.ReturnValue = new
                                    //{
                                    //    tenXa = _tenxa,
                                    //    tenHuyen = _tenHuyen,
                                    //    tenTinh = _tenTinh,
                                    //    dataPLIII
                                    //};
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "PL.IV":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = new BieuPhuLucIVOutputDto();
                                    //var dvhcPL4 = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.MaDVHC && x.Year == input.Year);
                                    var queryPL4 = (from item in _dbdRepos.GetAll()
                                                    where item.MaXa == input.MaDVHC
                                                    select new BieuPhuLucIVDto()
                                                    {
                                                        SHTDTruocBD = item.SHKDTruocBienDong,
                                                        SHTDSauBD = item.SHKDSauBienDong,
                                                        TenNguoiSDDat = item.TenChuSuDung,
                                                        DiaChiThuaDat = item.DiaChiThuaDat,
                                                        DienTichBD = item.DienTichBienDong,
                                                        MaLoaiDatTruocBD = item.MDSDTruocBienDong,
                                                        MaLoaiDatSauBD = item.MDSDSauBienDong,
                                                        MDSDTruocBienDong = item.MDSDTruocBienDong,
                                                        MDSDSauBienDong = item.MDSDSauBienDong,
                                                        DTTruocBienDong = item.DTTruocBienDong,
                                                        DTSauBienDong = item.DTSauBienDong,
                                                        NDTD = item.NDThayDoi
                                                    });
                                    data.BieuPhuLucIVDtos = await queryPL4.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
                                    //result.BieuPhuLucIIIs = await query.ToListAsync();
                                    commonResponseDto.ReturnValue = new
                                    {
                                        tenXa = _tenxa,
                                        tenHuyen = _tenHuyen,
                                        tenTinh = _tenTinh,
                                        data
                                    };
                                    //var dataPLIV = await _bieuPhuLucIVRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    //commonResponseDto.ReturnValue = new
                                    //{
                                    //    tenXa = _tenxa,
                                    //    tenHuyen = _tenHuyen,
                                    //    tenTinh = _tenTinh,
                                    //    dataPLIV
                                    //};
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                        
                    default:
                        break;
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
        public async Task<FileStreamResult> DownloadBieuMau(BieuMauDetailInputDto input)
        {
            try
            {
                string _tenxa = "";
                string _tenHuyen = "";
                string _tenTinh = "";
                var excelMemoryStream = new MemoryStream();
                string template = "";
                switch ((input.CapDVHC))
                {
                    case (int)CAP_DVHC.TINH:
                        _tenTinh = _dvhcRepos.Single(x => x.Ma == input.MaDVHC && x.Year == input.Year).Name;
                        break;
                    case (int)CAP_DVHC.HUYEN:
                        var _huyen = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.MaDVHC && x.Year == input.Year);
                        _tenHuyen = _huyen != null ? _huyen.Name : "";
                        _tenTinh = _huyen != null ? _dvhcRepos.Single(x => x.Ma == _huyen.MaTinh && x.Year == input.Year).Name : "";
                        break;
                    case (int)CAP_DVHC.XA:
                        var _xa = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.MaDVHC && x.Year == input.Year);
                        _tenxa = _xa != null ? _xa.Name : "";
                        var huyen = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == _xa.MaHuyen && x.Year == input.Year);
                        _tenHuyen = huyen != null ? huyen.Name : "";
                        _tenTinh = huyen != null ? _dvhcRepos.Single(x => x.Ma == huyen.MaTinh && x.Year == input.Year).Name : "";
                        break;
                    default:
                        break;
                }
                switch (input.KyHieu)
                {
                    case "01/TKKK":
                        {
                            template = "Template_Bieu01TKKK.xlsx";
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu01TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count == 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu01TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count == 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu01TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count == 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu01TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count == 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu01TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "02/TKKK":
                        {
                            template = "Template_Bieu02TKKK.xlsx";
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu02TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu02TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu02TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu02TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu02TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "03/TKKK":
                        {
                            template = "Template_Bieu03TKKK.xlsx";
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu03TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu03TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu03TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        //var data = await _bieu03TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        var data = new List<Bieu03TKKKDto>();
                                        var datadvhc = await _dvhcRepos.GetAll().Where(x => x.Parent_Code == input.MaDVHC).OrderBy(x => x.Id).ToListAsync();
                                        var databieu03 = await _bieu03TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        for (int i = 0; i < databieu03.Count; i++)
                                        {
                                            var dataxa = JsonConvert.DeserializeObject<List<DVHCBieu03TKKKDto>>(databieu03[i].DienTichTheoDVHC);
                                            var dto = new Bieu03TKKKDto()
                                            {
                                                STT = databieu03[i].STT,
                                                LoaiDat = databieu03[i].LoaiDat,
                                                Ma = databieu03[i].Ma,
                                                sequence = databieu03[i].sequence
                                            };
                                            for (int j = 0; j < dataxa.Count; j++)
                                            {
                                                switch (datadvhc.FindIndex(x => x.Ma == dataxa[j].MaDVHC))
                                                {
                                                    case 0: dto.DienTichDVHC1 = dataxa[j].DienTich; break;
                                                    case 1: dto.DienTichDVHC2 = dataxa[j].DienTich; break;
                                                    case 2: dto.DienTichDVHC3 = dataxa[j].DienTich; break;
                                                    case 3: dto.DienTichDVHC4 = dataxa[j].DienTich; break;
                                                    case 4: dto.DienTichDVHC5 = dataxa[j].DienTich; break;
                                                    case 5: dto.DienTichDVHC6 = dataxa[j].DienTich; break;
                                                    case 6: dto.DienTichDVHC7 = dataxa[j].DienTich; break;
                                                    case 7: dto.DienTichDVHC8 = dataxa[j].DienTich; break;
                                                    case 8: dto.DienTichDVHC9 = dataxa[j].DienTich; break;
                                                    case 9: dto.DienTichDVHC10 = dataxa[j].DienTich; break;
                                                    case 10: dto.DienTichDVHC11 = dataxa[j].DienTich; break;
                                                    case 11: dto.DienTichDVHC12 = dataxa[j].DienTich; break;
                                                    case 12: dto.DienTichDVHC13 = dataxa[j].DienTich; break;
                                                    case 13: dto.DienTichDVHC14 = dataxa[j].DienTich; break;
                                                    case 14: dto.DienTichDVHC15 = dataxa[j].DienTich; break;
                                                    case 15: dto.DienTichDVHC16 = dataxa[j].DienTich; break;
                                                    case 16: dto.DienTichDVHC17 = dataxa[j].DienTich; break;
                                                    case 17: dto.DienTichDVHC18 = dataxa[j].DienTich; break;
                                                    case 18: dto.DienTichDVHC19 = dataxa[j].DienTich; break;
                                                    case 19: dto.DienTichDVHC20 = dataxa[j].DienTich; break;
                                                    case 20: dto.DienTichDVHC21 = dataxa[j].DienTich; break;
                                                    case 21: dto.DienTichDVHC22 = dataxa[j].DienTich; break;
                                                    case 22: dto.DienTichDVHC23 = dataxa[j].DienTich; break;
                                                    case 23: dto.DienTichDVHC24 = dataxa[j].DienTich; break;
                                                    case 24: dto.DienTichDVHC25 = dataxa[j].DienTich; break;
                                                    case 25: dto.DienTichDVHC26 = dataxa[j].DienTich; break;
                                                    case 26: dto.DienTichDVHC27 = dataxa[j].DienTich; break;
                                                    case 27: dto.DienTichDVHC28 = dataxa[j].DienTich; break;
                                                    case 28: dto.DienTichDVHC29 = dataxa[j].DienTich; break;
                                                    case 29: dto.DienTichDVHC30 = dataxa[j].DienTich; break;
                                                }

                                            }
                                            data.Add(dto);
                                        }
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "04/TKKK":
                        {
                            template = "Template_Bieu04TKKK.xlsx";
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu04TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu04TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu04TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu04TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu04TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "05/TKKK":
                        {
                            template = "Template_Bieu05TKKK.xlsx";
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu05TKKKRepos.GetAll().Where(x => x.Year == input.Year).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.VUNG:
                                    {
                                        var data = await _bieu05TKKK_VungRepos.GetAll().Where(x => x.Year == input.Year && x.MaVung == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.TINH:
                                    {
                                        var data = await _bieu05TKKK_TinhRepos.GetAll().Where(x => x.Year == input.Year && x.MaTinh == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.HUYEN:
                                    {
                                        var data = await _bieu05TKKK_HuyenRepos.GetAll().Where(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                case (int)CAP_DVHC.XA:
                                    {
                                        var data = await _bieu05TKKK_XaRepos.GetAll().Where(x => x.Year == input.Year && x.MaXa == input.MaDVHC).OrderBy(x => x.sequence).ToListAsync();
                                        if (data.Count > 0)
                                        {
                                            excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "06/TKKKQPAN":
                        template = "Template_Bieu06TKKK.xlsx";
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu06TKKKQPANRepos.GetAllListAsync(x => x.Year == input.Year);
                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            //case (int)CAP_DVHC.VUNG:
                            //    {
                            //        var data = await _bieu05TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);
                            //        commonResponseDto.ReturnValue = data;
                            //        break;
                            //    }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu06TKKKQPAN_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01/KKSL":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01KKSLRepos.GetAllListAsync(x => x.Year == input.Year);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01KKSL_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01KKSL_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01KKSL_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01KKSL_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "02/KKSL":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu02KKSLRepos.GetAllListAsync(x => x.Year == input.Year);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu02KKSL_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu02KKSL_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu02KKSL_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu02KKSL_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01a/KKNLT":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01aKKNLTRepos.GetAllListAsync(x => x.Year == input.Year);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01aKKNLT_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01aKKNLT_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01aKKNLT_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);
                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01aKKNLT_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01b/KKNLT":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01bKKNLTRepos.GetAllListAsync(x => x.Year == input.Year);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01bKKNLT_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01bKKNLT_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01bKKNLT_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01bKKNLT_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "01c/KKNLT":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu01cKKNLTRepos.GetAllListAsync(x => x.Year == input.Year);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.VUNG:
                                {
                                    var data = await _bieu01cKKNLT_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.TINH:
                                {
                                    var data = await _bieu01cKKNLT_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.HUYEN:
                                {
                                    var data = await _bieu01cKKNLT_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            case (int)CAP_DVHC.XA:
                                {
                                    var data = await _bieu01cKKNLT_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "PL.III":
                        template = "Template_PL3.xlsx";
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.XA:
                                {
                                    var resultPL3 = new BieuPhuLucIIIOutputDto();
                                    var data = await (from item in _dcRepos.GetAll()
                                                    where item.MaXa == input.MaDVHC && item.Year == input.Year
                                                    select new BieuPhuLucIIIDto()
                                                    {
                                                        DienTich = item.DienTich,
                                                        MaLoaiDatHienTrang = item.MucDichSuDung,
                                                        MaLoaiDatKyTruoc = item.MucDichSuDungKyTruoc,
                                                        MaLoaiDatSuDungKetHop = "",
                                                        MaDoiTuongHienTrang = item.MaDoiTuong,
                                                        MaDoiTuongKyTruoc = item.MaDoiTuongKyTruoc,
                                                        MaKhuVucTongHop = "",
                                                        GhiChu = "",
                                                        MaXa = item.MaXa
                                                    }).ToListAsync();
                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    case "PL.IV":
                        template = "Template_PL4.xlsx";
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.XA:
                                {
                                    var resultPL4 = new BieuPhuLucIVOutputDto();
                                    var data = await (from item in _dbdRepos.GetAll()
                                                    where item.MaXa == input.MaDVHC
                                                    select new BieuPhuLucIVDto()
                                                    {
                                                        SHTDTruocBD = item.SHKDTruocBienDong,
                                                        SHTDSauBD = item.SHKDSauBienDong,
                                                        TenNguoiSDDat = item.TenChuSuDung,
                                                        DiaChiThuaDat = item.DiaChiThuaDat,
                                                        DienTichBD = item.DienTichBienDong,
                                                        MaLoaiDatTruocBD = item.MDSDTruocBienDong,
                                                        MaLoaiDatSauBD = item.MDSDSauBienDong,
                                                        MDSDTruocBienDong =item.MDSDTruocBienDong,
                                                        MDSDSauBienDong = item.MDSDSauBienDong,
                                                        DTTruocBienDong = item.DTTruocBienDong,
                                                        DTSauBienDong = item.DTSauBienDong,
                                                        NDTD = item.NDThayDoi
                                                    }).ToListAsync();
                                    if (data.Count > 0)
                                    {
                                        excelMemoryStream = DownloadBieuMauByCap(data, input.CapDVHC, input.Year, input.MaDVHC, _tenTinh, _tenHuyen, _tenxa, template);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
                return new FileStreamResult(excelMemoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "Bieu" + input.KyHieu + ".xlsx"
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return null;
            //var _GetFilePath = filePath;
            //var provider = new FileExtensionContentTypeProvider();
            //if (!provider.TryGetContentType(filePath, out var contentType))
            //{
            //    contentType = "application/octet-stream";
            //}
            //return new FileStreamResult(new FileStream(filePath, FileMode.Open), contentType)
            //{
            //    FileDownloadName = Path.GetFileName(filePath)
            //};

        }
        private MemoryStream DownloadBieuMauByCap(object data, int? capDVHC, long? year, string _ma, string _tenTinh, string _tenHuyen, string _tenXa, string template)
        {
            try
            {
                var excelMemoryStream = new MemoryStream();
                Workbook wb = new Workbook(new MemoryStream(System.IO.File.ReadAllBytes(Path.Combine("wwwroot/Templates/excels", template))));
                WorkbookDesigner wd = new WorkbookDesigner(wb);

                wd.SetDataSource("data", data);
                if (template == "Template_PL3.xlsx" || template == "Template_PL4.xlsx")
                    wd.SetDataSource("Header", new[] { new
                                            {
                                                tinh = _tenTinh,
                                                huyen = _tenHuyen,
                                                xa = "Tỉnh: " + _tenTinh + " Huyện: " + _tenTinh + " xã: " + _tenXa,
                                                year = "(Đến ngày 31/12/" + year.ToString() + ")"
                                            }});
                else if (template == "Template_Bieu05TKKK.xlsx")
                {
                    if (year != null)
                    {
                        var namKyTruoc = year%10 == 4 || year % 10 == 9 ? year - 4 : year - 1;
                        wd.SetDataSource("Header", new[] { new
                                            {
                                                tinh = _tenTinh,
                                                huyen = _tenHuyen,
                                                xa = _tenXa,
                                                namkytruoc = "Năm " + namKyTruoc.ToString(),
                                                year = "(Đến ngày 31/12/" + year.ToString() + ")"
                                            }});
                    }
                    
                }
                else if (template == "Template_Bieu03TKKK.xlsx")
                {
                    var lstDVHC = _dvhcRepos.GetAllListAsync(x => x.Parent_Code == _ma).Result;
                    if (lstDVHC != null && lstDVHC.Count > 0)
                    {
                        Worksheet worksheet = wb.Worksheets[0];
                        int start = 34 - lstDVHC.Count;
                        while (start > 3)
                        {
                            worksheet.Cells.DeleteColumn(start);
                            start = start - 1;
                        }
                        var dsHeader = new Dictionary<string, object>();
                        dsHeader["tinh"] = _tenTinh;
                        dsHeader["huyen"] = _tenHuyen;
                        dsHeader["year"] = "(Đến ngày 31/12/" + year.ToString() + ")";

                        // Convert sang Dictionary để thêm các giá trị động
                        //var dictHeader = (IDictionary<string, object>)dsHeader;
                        for (int i = 0; i < lstDVHC.Count; i++)
                        {
                            string dvhc = "dvhc" + (i+1).ToString();
                            string colnum = "colnum" + (i + 1).ToString();
                            dsHeader[dvhc] = lstDVHC[i].Name;
                            dsHeader[colnum] = -(5 + lstDVHC.Count - i);
                        }
                        wd.SetDataSource("Header", new[] { dsHeader });
                        
                    }
                }
                else
                    wd.SetDataSource("Header", new[] { new
                                            {
                                                tinh = _tenTinh,
                                                huyen = _tenHuyen,
                                                xa = _tenXa,
                                                year = "(Đến ngày 31/12/" + year.ToString() + ")"
                                            }});
                wd.Process();
                wb.Save(excelMemoryStream, SaveFormat.Xlsx);
                byte[] bytesInStream = excelMemoryStream.ToArray();
                excelMemoryStream.Position = 0;
                return excelMemoryStream;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }
        //private MemoryStream DownloadBieuMauByCap(object data, int? capDVHC, long? year, string _ma, string _tenTinh, string _tenHuyen, string _tenXa, string maDVHC, string template)
        //{
        //    try
        //    {

        //        var excelMemoryStream = new MemoryStream();
        //        Workbook wb = new Workbook(new MemoryStream(System.IO.File.ReadAllBytes(Path.Combine("wwwroot/Templates/excels", template))));
        //        WorkbookDesigner wd = new WorkbookDesigner(wb);

        //        wd.SetDataSource("data", data);
        //        if (template == "Template_Bieu03TKKK.xlsx")
        //        {
        //            var lstDVHC = _dvhcRepos.GetAllListAsync(x => x.Parent_Code == maDVHC).Result;
        //            if (lstDVHC != null && lstDVHC.Count > 0)
        //            {
        //                int start = 4 + lstDVHC.Count;
        //                var dsHeader = new Dictionary<string, object>();
        //                dsHeader["tinh"] = _tenTinh;
        //                dsHeader["huyen"] = _tenHuyen;
        //                dsHeader["year"] = "(Đến ngày 31/12/" + year.ToString() + ")";

        //                // Convert sang Dictionary để thêm các giá trị động
        //                //var dictHeader = (IDictionary<string, object>)dsHeader;
        //                Worksheet worksheet = wb.Worksheets[0];
        //                for (int i = 0; i < lstDVHC.Count; i++)
        //                {
        //                    string dvhc = "dvhc" + i.ToString();
        //                    dsHeader[dvhc] = lstDVHC[i].Name;
        //                }
        //                wd.SetDataSource("Header", dsHeader);
        //                while(start < 34) 
        //                {
        //                    worksheet.Cells.DeleteColumn(start);
        //                    start++;
        //                }
        //            }
                   
        //            wd.SetDataSource("Header", new[] { new
        //                                    {
        //                                        tinh = _tenTinh,
        //                                        huyen = _tenHuyen,
        //                                        xa = _tenXa,
        //                                        year = "(Đến ngày 31/12/" + year.ToString() + ")"
        //                                    }});

        //        }
        //        else
        //            wd.SetDataSource("Header", new[] { new
        //                                    {
        //                                        tinh = _tenTinh,
        //                                        huyen = _tenHuyen,
        //                                        xa = _tenXa,
        //                                        year = "(Đến ngày 31/12/" + year.ToString() + ")"
        //                                    }});
        //        wd.Process();
        //        wb.Save(excelMemoryStream, SaveFormat.Xlsx);
        //        byte[] bytesInStream = excelMemoryStream.ToArray();
        //        excelMemoryStream.Position = 0;
        //        return excelMemoryStream;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message);
        //        return null;
        //    }
        //}
    }
}
