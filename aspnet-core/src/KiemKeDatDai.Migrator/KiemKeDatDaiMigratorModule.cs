using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KiemKeDatDai.Configuration;
using KiemKeDatDai.EntityFrameworkCore;
using KiemKeDatDai.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace KiemKeDatDai.Migrator;

[DependsOn(typeof(KiemKeDatDaiEntityFrameworkModule))]
public class KiemKeDatDaiMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public KiemKeDatDaiMigratorModule(KiemKeDatDaiEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(KiemKeDatDaiMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            KiemKeDatDaiConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(KiemKeDatDaiMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
