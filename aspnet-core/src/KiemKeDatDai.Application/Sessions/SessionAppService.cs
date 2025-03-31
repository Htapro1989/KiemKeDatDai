using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using KiemKeDatDai.Authorization.Roles;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.Sessions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using static KiemKeDatDai.CommonEnum;

namespace KiemKeDatDai.Sessions;

public class SessionAppService : KiemKeDatDaiAppServiceBase, ISessionAppService
{
    private readonly IRepository<UserRole, long> _userRoleRepos;
    private readonly RoleManager _roleManager;
    private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
    private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
    public SessionAppService(IRepository<UserRole, long> userRoleRepos, 
                            RoleManager roleManager,
                            IRepository<DonViHanhChinh, long> dvhcRepos,
                            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos)
    {
        _userRoleRepos = userRoleRepos;
        _roleManager = roleManager;
        _dvhcRepos = dvhcRepos;
        _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
    }
    [DisableAuditing]
    public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
    {
        var output = new GetCurrentLoginInformationsOutput
        {
            Application = new ApplicationInfoDto
            {
                Version = AppVersionHelper.Version,
                ReleaseDate = AppVersionHelper.ReleaseDate,
                Features = new Dictionary<string, bool>()
            }
        };

        if (AbpSession.TenantId.HasValue)
        {
            output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
        }

        if (AbpSession.UserId.HasValue)
        {
            output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            var _userRoleAdmin = await _userRoleRepos.FirstOrDefaultAsync(x => x.UserId == AbpSession.UserId && x.RoleId == 1);
            var _userRole = await _userRoleRepos.FirstOrDefaultAsync(x => x.UserId == AbpSession.UserId);
            if (_userRoleAdmin != null)
            {
                output.User.IsAdmin = true;
            }
            if (_userRole != null)
            {
                var _role = _roleManager.GetRoleByIdAsync(_userRole.RoleId);
                if (_role != null)
                {
                    output.User.Role = _role.Result.Name;
                    output.User.RoleDescription = _role.Result.Description;
                }
            }
            var _dvhc = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == output.User.DonViHanhChinhCode);
            if (_dvhc != null)
            {
                output.User.DonViHanhChinh = _dvhc.Name;
                if (_dvhc.CapDVHCId == (int)CAP_DVHC.XA)
                {
                    var _bieu01TKKK_Xa = await _bieu01TKKK_XaRepos.FirstOrDefaultAsync(x => x.MaXa == output.User.DonViHanhChinhCode);
                    if (_bieu01TKKK_Xa != null)
                    {
                        output.User.Message_Info = "Đã tiếp nhận dữ liệu " + _dvhc.TenXa + " ngày " + _bieu01TKKK_Xa.CreationTime.ToString("dd/MM/yyyy:hh:mm:ss");
                    }
                    else
                    {
                        output.User.Message_Info = "Chưa tiếp nhận dữ liệu " + _dvhc.TenXa;
                    }
                }
            }
        }

        return output;
    }
}
