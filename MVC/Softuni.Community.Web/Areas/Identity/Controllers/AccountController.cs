using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Areas.Identity.Models;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Controllers;

namespace Softuni.Community.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : BaseController
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;
        private readonly IDataService dataService;

        public AccountController(UserManager<CustomUser> userMgr,
            SignInManager<CustomUser> signinMgr, IDataService dataService)
        {
            this.userManager = userMgr;
            this.signInManager = signinMgr;
            this.dataService = dataService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = userManager.FindByEmailAsync(bindingModel.Email).Result;
                if (user != null)
                {
                    signInManager.SignOutAsync().GetAwaiter().GetResult();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        signInManager.PasswordSignInAsync(
                            user, bindingModel.Password, false, false).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction(Actions.Index, Controllers.Home);
                    }
                }
                ModelState.AddModelError(nameof(LoginBindingModel.Email),
                    ErrorMessages.UserWithGivenEmailNotFound);
            }

            // Returning empty password field
            bindingModel.Password = String.Empty;
            return View(bindingModel);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterBindingModel bindingModel)
        {
            if (ModelState.IsValid && (bindingModel.Password == bindingModel.ConfirmPassword))
            {
                var userInfo = new UserInfo();

                CustomUser user = new CustomUser
                {
                    Email = bindingModel.Email,
                    MyInfo = dataService.AddUserInfo(userInfo)
                };

                IdentityResult result
                    = userManager.CreateAsync(user, bindingModel.Password).Result;
                

                if (dataService.IsFirstUser())
                    userManager.AddToRoleAsync(user, Role.Admin).Wait();
                else
                    userManager.AddToRoleAsync(user, Role.User).Wait();

                if (result.Succeeded)
                {
                    return RedirectToAction(Actions.FillYourProfile, Controllers.Account);
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

            }
            return View(bindingModel);
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Authorize]
        public IActionResult FillYourProfile()
        {
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction(Actions.Index, Controllers.Home);
        }
    }
}