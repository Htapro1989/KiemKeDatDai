using Abp.Modules;
using Abp.Reflection.Extensions;
using KiemKeDatDai.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KiemKeDatDai.Web.Host.Startup
{
    [DependsOn(
       typeof(KiemKeDatDaiWebCoreModule))]
    public class KiemKeDatDaiWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public KiemKeDatDaiWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KiemKeDatDaiWebHostModule).GetAssembly());
        }
    }
}
