using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai;

[DependsOn(
    typeof(KiemKeDatDaiCoreModule),
    typeof(AbpAutoMapperModule))]
public class KiemKeDatDaiApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<KiemKeDatDaiAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(KiemKeDatDaiApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
