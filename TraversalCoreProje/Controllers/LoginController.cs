﻿using EntityLayer.Concrete;
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

        public LoginController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
        //[HttpPost]
        //public IActionResult SignIn()
        //{
        //    return View();
        //}

    }
}
