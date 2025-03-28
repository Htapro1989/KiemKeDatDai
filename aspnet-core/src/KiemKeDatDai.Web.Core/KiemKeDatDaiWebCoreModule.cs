using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using KiemKeDatDai.Authentication.JwtBearer;
using KiemKeDatDai.Configuration;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.EntityFrameworkCore;
using KiemKeDatDai.RisApplication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace KiemKeDatDai
{
    [DependsOn(
         typeof(KiemKeDatDaiApplicationModule),
         typeof(KiemKeDatDaiEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
        , typeof(AbpAspNetCoreSignalRModule)
     )]
    public class KiemKeDatDaiWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IConfigSystemAppService _configSystem;

        public KiemKeDatDaiWebCoreModule(IWebHostEnvironment env/*, IRepository<ConfigSystem, long> configSystemRepos*/)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
            //_configSystemRepos = configSystemRepos;
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                KiemKeDatDaiConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(KiemKeDatDaiApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();
            
            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            //tokenAuthConfig.Expiration = _expired_token != null ? TimeSpan.FromMinutes(long.Parse(_expired_token.ToString())) : TimeSpan.FromMinutes(30);
            tokenAuthConfig.Expiration = TimeSpan.FromMinutes(30);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KiemKeDatDaiWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(KiemKeDatDaiWebCoreModule).Assembly);
        }
    }
}
