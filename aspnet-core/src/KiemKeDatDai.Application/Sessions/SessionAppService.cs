using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using KiemKeDatDai.Authorization.Roles;
using KiemKeDatDai.Sessions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KiemKeDatDai.Sessions;

public class SessionAppService : KiemKeDatDaiAppServiceBase, ISessionAppService
{
    private readonly IRepository<UserRole, long> _userRoleRepos;
    private readonly RoleManager _roleManager;
    public SessionAppService(IRepository<UserRole, long> userRoleRepos, RoleManager roleManager)
    {
        _userRoleRepos = userRoleRepos;
        _roleManager = roleManager;
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
        }

        return output;
    }
}
