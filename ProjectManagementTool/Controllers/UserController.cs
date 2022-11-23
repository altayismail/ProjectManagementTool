using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Data_Access_Layer.Concrete;
using Entity_Layer.Concrete;

namespace ProjectManagementTool.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(User user)
        {
            Context context = new Context();
            var _user = context.Users.FirstOrDefault(x => x.Email == user.Email
                                                            && x.Password == user.Password);

            if (_user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.Email)
                };
                var userIndetity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIndetity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Hatalı giriş bilgileri, lütfen tekrar deneyiniz.";
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
