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

namespace KiemKeDatDai.App.DMBieuMau
{
    public class DMBieuMauAppService : KiemKeDatDaiAppServiceBase, IDMBieuMauAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DM_BieuMau, long> _dmbmRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<DM_DVCH_BM, long> _dmdvhcbmRepos;

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
                                        var data = await _bieu01TKKKRepos.GetAllListAsync(x => x.Year == input.Year);
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
                                        var data = await _bieu01TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);
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
                                        var data = await _bieu01TKKK_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);
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
                                        var data = await _bieu01TKKK_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);
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
                                        var data = await _bieu01TKKK_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);
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
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu02TKKKRepos.GetAllListAsync(x => x.Year == input.Year);
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
                                    var data = await _bieu02TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);
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
                                    var data = await _bieu02TKKK_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);
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
                                    var data = await _bieu02TKKK_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);
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
                                    var data = await _bieu02TKKK_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);
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
                    case "03/TKKK":
                        {
                            switch (input.CapDVHC)
                            {
                                case (int)CAP_DVHC.TRUNG_UONG:
                                    {
                                        var data = await _bieu03TKKKRepos.GetAllListAsync(x => x.Year == input.Year);
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
                                        var data = await _bieu03TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);
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
                                        var data = await _bieu03TKKK_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);
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
                                        var data = await _bieu03TKKK_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);
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
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu04TKKKRepos.GetAllListAsync(x => x.Year == input.Year);
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
                                    var data = await _bieu04TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

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
                                    var data = await _bieu04TKKK_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

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
                                    var data = await _bieu04TKKK_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

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
                                    var data = await _bieu04TKKK_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

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
                    case "05/TKKK":
                        switch (input.CapDVHC)
                        {
                            case (int)CAP_DVHC.TRUNG_UONG:
                                {
                                    var data = await _bieu05TKKKRepos.GetAllListAsync(x => x.Year == input.Year);

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
                                    var data = await _bieu05TKKK_VungRepos.GetAllListAsync(x => x.Year == input.Year && x.MaVung == input.MaDVHC);

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
                                    var data = await _bieu05TKKK_TinhRepos.GetAllListAsync(x => x.Year == input.Year && x.MaTinh == input.MaDVHC);

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
                                    var data = await _bieu05TKKK_HuyenRepos.GetAllListAsync(x => x.Year == input.Year && x.MaHuyen == input.MaDVHC);

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
                                    var data = await _bieu05TKKK_XaRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

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
                        var dataPLIII = await _bieuPhuLucIIIRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);
                        commonResponseDto.ReturnValue = new
                        {
                            tenXa = _tenxa,
                            tenHuyen = _tenHuyen,
                            tenTinh = _tenTinh,
                            dataPLIII
                        };
                        break;
                    case "PL.IV":
                        var dataPLIV = await _bieuPhuLucIVRepos.GetAllListAsync(x => x.Year == input.Year && x.MaXa == input.MaDVHC);

                        commonResponseDto.ReturnValue = new
                        {
                            tenXa = _tenxa,
                            tenHuyen = _tenHuyen,
                            tenTinh = _tenTinh,
                            dataPLIV
                        };
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
    }
}
