using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles="User")]
    public class UserController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult AllProducts()
        {
            return View();
        }
        public IActionResult MyCart()
        {
            return View();
        }
        public IActionResult MyProfile()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Home");
        }
    }
}
