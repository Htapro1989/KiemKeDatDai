using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace KiemKeDatDai.Controllers
{
    public abstract class KiemKeDatDaiControllerBase : AbpController
    {
        protected KiemKeDatDaiControllerBase()
        {
            LocalizationSourceName = KiemKeDatDaiConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
