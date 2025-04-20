using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Authorization;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.RisApplication;
using KiemKeDatDai.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.App.DMBieuMau
{
    [AbpAuthorize(PermissionNames.Pages_Report_NhapBieu)]
    public class BieuMau06AppService : KiemKeDatDaiAppServiceBase, IBieuMau06AppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DM_BieuMau, long> _dmbmRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<DM_DVCH_BM, long> _dmdvhcbmRepos;
        private readonly IRepository<Data_Commune, long> _dcRepos;
        private readonly IRepository<Data_BienDong, long> _dbdRepos;

        #region biểu mẫu
        private readonly IRepository<Bieu06TKKKQPAN, long> _bieu06TKKKQPANRepos;
        private readonly IRepository<Bieu06TKKKQPAN_Vung, long> _bieu06TKKKQPAN_VungRepos;
        private readonly IRepository<Bieu06TKKKQPAN_Tinh, long> _bieu06TKKKQPAN_TinhRepos;
        #endregion

        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;

        private readonly ICache mainCache;

        public BieuMau06AppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DM_BieuMau, long> dmbmRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<DM_DVCH_BM, long> dmdvhcbmRepos,
            IRepository<Data_Commune, long> dcRepos,
            IRepository<Data_BienDong, long> dbdRepos,

        #region biểu mẫu
            IRepository<Bieu06TKKKQPAN, long> bieu06TKKKQPANRepos,
            IRepository<Bieu06TKKKQPAN_Vung, long> bieu06TKKKQPAN_VungRepos,
            IRepository<Bieu06TKKKQPAN_Tinh, long> bieu06TKKKQPAN_TinhRepos,
        #endregion

            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
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
            _bieu06TKKKQPANRepos = bieu06TKKKQPANRepos;
            _bieu06TKKKQPAN_VungRepos = bieu06TKKKQPAN_VungRepos;
            _bieu06TKKKQPAN_TinhRepos = bieu06TKKKQPAN_TinhRepos;

            #endregion

            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        public async Task<CommonResponseDto> CreateOrUpdate(Bieu06TKKKQPAN_TinhDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _bieu06TKKKQPAN_TinhRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        data.STT = input.STT;
                        data.DonVi = input.DonVi;
                        data.DiaChi = input.DiaChi;
                        data.DienTichDatQuocPhong = input.DienTichDatQuocPhong;
                        data.DienTichKetHopKhac = input.DienTichKetHopKhac;
                        data.DienTichDaDoDac = input.DienTichDaDoDac;
                        data.SoGCNDaCap = input.SoGCNDaCap;
                        data.DienTichDaCapGCN = input.DienTichDaCapGCN;
                        data.GhiChu = input.GhiChu;
                        data.MaTinh = input.MaTinh;
                        data.TinhId = input.TinhId;
                        data.Year = input.Year;
                        data.Active = input.Active;
                        await _bieu06TKKKQPAN_TinhRepos.UpdateAsync(data);
                    }
                    else
                    {
                        commonResponseDto.Message = "Biểu mẫu này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                }
                else
                {
                    var bieu06 = input.MapTo<Bieu06TKKKQPAN_Tinh>();
                    await _bieu06TKKKQPAN_TinhRepos.InsertAsync(bieu06);
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
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var bieu06 = await _bieu06TKKKQPAN_TinhRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (bieu06 != null)
                {
                    await _bieu06TKKKQPAN_TinhRepos.DeleteAsync(bieu06);
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Dữ liệu không tồn tại";
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
        public async Task<CommonResponseDto> GetByDVHC(long dvhcId, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                commonResponseDto.ReturnValue = await _bieu06TKKKQPAN_TinhRepos.FirstOrDefaultAsync(x => x.TinhId == dvhcId && x.Year == year); ;
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
    }
}
