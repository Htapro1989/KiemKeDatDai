using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using KiemKeDatDai.EntityFrameworkCore.Seed;

namespace KiemKeDatDai.EntityFrameworkCore;

[DependsOn(
    typeof(KiemKeDatDaiCoreModule),
    typeof(AbpZeroCoreEntityFrameworkCoreModule))]
public class KiemKeDatDaiEntityFrameworkModule : AbpModule
{
    /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
    public bool SkipDbContextRegistration { get; set; }

    public bool SkipDbSeed { get; set; }

    public override void PreInitialize()
    {
        if (!SkipDbContextRegistration)
        {
            Configuration.Modules.AbpEfCore().AddDbContext<KiemKeDatDaiDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    KiemKeDatDaiDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    KiemKeDatDaiDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(KiemKeDatDaiEntityFrameworkModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        if (!SkipDbSeed)
        {
            SeedHelper.SeedHostDb(IocManager);
        }
    }
}
