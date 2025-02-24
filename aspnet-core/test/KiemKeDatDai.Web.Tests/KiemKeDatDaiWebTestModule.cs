using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KiemKeDatDai.EntityFrameworkCore;
using KiemKeDatDai.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace KiemKeDatDai.Web.Tests;

[DependsOn(
    typeof(KiemKeDatDaiWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class KiemKeDatDaiWebTestModule : AbpModule
{
    public KiemKeDatDaiWebTestModule(KiemKeDatDaiEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(KiemKeDatDaiWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(KiemKeDatDaiWebMvcModule).Assembly);
    }
}