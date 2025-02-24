using Abp.Web.Security.AntiForgery;
using KiemKeDatDai.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace KiemKeDatDai.Web.Host.Controllers
{
    public class AntiForgeryController : KiemKeDatDaiControllerBase
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

        public AntiForgeryController(IAntiforgery antiforgery, IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiforgery = antiforgery;
            _antiForgeryManager = antiForgeryManager;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }

        public void SetCookie()
        {
            _antiForgeryManager.SetCookie(HttpContext);
        }
    }
}
