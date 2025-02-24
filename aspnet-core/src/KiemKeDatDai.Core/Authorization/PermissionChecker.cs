using Abp.Authorization;
using KiemKeDatDai.Authorization.Roles;
using KiemKeDatDai.Authorization.Users;

namespace KiemKeDatDai.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
