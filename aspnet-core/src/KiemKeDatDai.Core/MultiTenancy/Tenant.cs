using Abp.MultiTenancy;
using KiemKeDatDai.Authorization.Users;

namespace KiemKeDatDai.MultiTenancy;

public class Tenant : AbpTenant<User>
{
    public Tenant()
    {
    }

    public Tenant(string tenancyName, string name)
        : base(tenancyName, name)
    {
    }
}
