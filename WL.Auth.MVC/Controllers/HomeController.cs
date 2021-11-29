using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using WL.Auth.MVC.Models;

namespace WL.Auth.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       // [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        //[Authorize]

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
            
        }

         public async Task<IActionResult> GoogleResponse()
        {
            /* var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

             var claims = result.Principal.Identities
                 .FirstOrDefault().Claims.Select(claim => new
                 {
                     claim.Issuer,
                     claim.OriginalIssuer,
                     claim.Type,
                     claim.Value
                 });

             return Json(claims);*/


            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "User Id"));
            identity.AddClaim(new Claim(ClaimTypes.Name, "User Name"));

            var principal = new ClaimsPrincipal(identity);

            //var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
              var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
              var claims = result.Principal.Identities
                  .FirstOrDefault().Claims.Select(claim => new
                  {
                      claim.Issuer,
                      claim.OriginalIssuer,
                      claim.Type,
                      claim.Value
                  });

            return Json(claims);
        }
         [AllowAnonymous]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Privacy");
        }
    }
}
