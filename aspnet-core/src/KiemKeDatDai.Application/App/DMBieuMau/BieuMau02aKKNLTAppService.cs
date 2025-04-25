using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using KiemKeDatDai.App.DMBieuMau.Dto;
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
    public class BieuMau02aKKNLTAppService : KiemKeDatDaiAppServiceBase, IBieuMau02aKKNLTAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DM_BieuMau, long> _dmbmRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<DM_DVCH_BM, long> _dmdvhcbmRepos;
        private readonly IRepository<Data_Commune, long> _dcRepos;
        private readonly IRepository<Data_BienDong, long> _dbdRepos;

        #region biểu mẫu
        private readonly IRepository<Bieu02aKKNLT, long> _bieu02aKKNLTRepos;
        private readonly IRepository<Bieu02aKKNLT_Vung, long> _bieu02aKKNLT_VungRepos;
        private readonly IRepository<Bieu02aKKNLT_Tinh, long> _bieu02aKKNLT_TinhRepos;
        #endregion

        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public BieuMau02aKKNLTAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DM_BieuMau, long> dmbmRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<DM_DVCH_BM, long> dmdvhcbmRepos,
            IRepository<Data_Commune, long> dcRepos,
            IRepository<Data_BienDong, long> dbdRepos,

        #region biểu mẫu
            IRepository<Bieu02aKKNLT, long> bieu02aKKNLTRepos,
            IRepository<Bieu02aKKNLT_Vung, long> bieu02aKKNLT_VungRepos,
            IRepository<Bieu02aKKNLT_Tinh, long> bieu02aKKNLT_TinhRepos,
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
            _bieu02aKKNLTRepos = bieu02aKKNLTRepos;
            _bieu02aKKNLT_VungRepos = bieu02aKKNLT_VungRepos;
            _bieu02aKKNLT_TinhRepos = bieu02aKKNLT_TinhRepos;

            #endregion

            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        public async Task<CommonResponseDto> CreateOrUpdate(Bieu02aKKNLT_TinhInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _bieu02aKKNLT_TinhRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        data.STT = input.STT;
                        data.TenDonVi = input.TenDonVi;
                        data.DienTichTheoQDGiaoThue = input.DienTichTheoQDGiaoThue;
                        data.DienTichGiaoDat = input.DienTichGiaoDat;
                        data.DienTichChoThueDat = input.DienTichChoThueDat;
                        data.DienTichChuaXacDinhGiaoThue = input.DienTichChuaXacDinhGiaoThue;
                        data.DienTichDoDacTL1000 = input.DienTichDoDacTL1000;
                        data.DienTichDoDacTL2000 = input.DienTichDoDacTL2000;
                        data.DienTichDoDacTL5000 = input.DienTichDoDacTL5000;
                        data.DienTichDoDacTL10000 = input.DienTichDoDacTL10000;
                        data.SoGCNDaCap = input.SoGCNDaCap;
                        data.DienTichGCNDaCap = input.DienTichGCNDaCap;
                        data.DienTichDaBanGiao = input.DienTichDaBanGiao;
                        data.GhiChu = input.GhiChu;
                        data.Year = input.Year;
                        data.MaTinh = input.MaTinh;
                        data.TinhId = input.TinhId;
                        data.sequence = input.sequence; 
                        data.Active = input.Active;
                        await _bieu02aKKNLT_TinhRepos.UpdateAsync(data);
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
                    var bieu02a = input.MapTo<Bieu02aKKNLT_Tinh>();
                    await _bieu02aKKNLT_TinhRepos.InsertAsync(bieu02a);
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
                var bieu06 = await _bieu02aKKNLT_TinhRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (bieu06 != null)
                {
                    await _bieu02aKKNLT_TinhRepos.DeleteAsync(bieu06);
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
                commonResponseDto.ReturnValue = await _bieu02aKKNLT_TinhRepos.FirstOrDefaultAsync(x => x.TinhId == dvhcId && x.Year == year); ;
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
