using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Data_Access_Layer.Concrete;
using Entity_Layer.Concrete;
using Business_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Business_Layer.ValidationRules;
using FluentValidation.Results;

namespace ProjectManagementTool.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        UserManager userManager = new UserManager(new EFUserRepo());
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
                ViewBag.Message = "Invalid email or password, try again.";
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserUpdate(int id)
        {
            var user = userManager.GetQueryById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult UserUpdate(User user)
        {
            UserValidator userValidator = new UserValidator();
            ValidationResult validationResult = userValidator.Validate(user);

            if (userManager.GetAllQuery().Any(x => x.Email == user.Email))
            {
                ViewBag.ErrorMessage = "Bu emaile sahip başka bir kullanıcı bulunmaktadır.";
                return View();
            }

            if (validationResult.IsValid)
            {
                userManager.UpdateT(user);
                return RedirectToAction("GetKullanıcı");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
