using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous] //Gecerli butun kurallardan muaf tutmaktır.

    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel u)
        {
            AppUser appUser = new AppUser()
            {
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Mail,
                UserName = u.Username
            };
            if (u.Password == u.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, u.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var x in result.Errors)
                    {
                        ModelState.AddModelError("", x.Description);
                    }
                }
            }
            return View(u);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel u)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(u.username, u.password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Profile", new { area = "Member" });
                }
                else
                {
                    return RedirectToAction("SignIn", "Login");
                }
            }
            return View();
        }

    }
}
