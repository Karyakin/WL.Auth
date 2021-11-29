using Microsoft.AspNetCore.Mvc;

namespace WL.Auth.MVC.Controllers
{
    public class AuthController : Controller
    {
        // GET
        public IActionResult SigninGoogle()
        {
            return View();
        }
    }
}