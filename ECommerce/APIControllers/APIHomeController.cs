using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIHomeController : ControllerBase
    {
        private readonly IHomeRepository _hservices;
        public APIHomeController(IHomeRepository hservices)
        {
            _hservices = hservices;
        }
        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] SignupModel reg)
        {
            _hservices.AddSignup(reg);
            return Ok("Regestration Success");
        }
        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody] SigninModel login)
        {
            var user = _hservices.GetSignup(login);

            if (user == null)
                return BadRequest("Invalid email or password");

            // Create Claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal);

            return Ok(new { role = user.Role });
        }
        [HttpPost]
        public IActionResult Signin()
        {
            return Ok();
        }
    }
}
