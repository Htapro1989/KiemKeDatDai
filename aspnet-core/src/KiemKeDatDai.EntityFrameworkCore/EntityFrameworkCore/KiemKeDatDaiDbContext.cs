using Abp.Zero.EntityFrameworkCore;
using KiemKeDatDai.Authorization.Roles;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace KiemKeDatDai.EntityFrameworkCore;

public class KiemKeDatDaiDbContext : AbpZeroDbContext<Tenant, Role, User, KiemKeDatDaiDbContext>
{
    /* Define a DbSet for each entity of the application */

    public KiemKeDatDaiDbContext(DbContextOptions<KiemKeDatDaiDbContext> options)
        : base(options)
    {
    }
}
