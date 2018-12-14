using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Controllers;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : BaseController
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;
        private readonly IDataService dataService;
        private readonly IMapper mapper;

        public AccountController(UserManager<CustomUser> userMgr,
            SignInManager<CustomUser> signinMgr, IDataService dataService, IMapper mapper)
        {
            this.userManager = userMgr;
            this.signInManager = signinMgr;
            this.dataService = dataService;
            this.mapper = mapper;
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
                    var isPasswordRight = userManager.CheckPasswordAsync(user, bindingModel.Password).Result;

                    if (isPasswordRight)
                    {
                        signInManager.SignOutAsync().GetAwaiter().GetResult();
                        Microsoft.AspNetCore.Identity.SignInResult result =
                            signInManager.PasswordSignInAsync(
                                user, bindingModel.Password, false, false).Result;

                        if (result.Succeeded)
                            return RedirectToAction(ActionsConts.Index, ControllersConts.Home);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(LoginBindingModel.Password),
                            ErrorMessages.UserWithGivenPasswordNotFound);
                        // Will returning empty password field
                        //bindingModel.Password = string.Empty;
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginBindingModel.Email),
                        ErrorMessages.UserWithGivenEmailNotFound);
                }
            }
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
                CustomUser user = mapper.Map<CustomUser>(bindingModel);
                user.MyInfo = dataService.AddUserInfo(userInfo);

                IdentityResult result
                    = userManager.CreateAsync(user, bindingModel.Password).Result;

                if (dataService.IsFirstUser())
                    userManager.AddToRoleAsync(user, Role.Admin).Wait();
                else
                    userManager.AddToRoleAsync(user, Role.User).Wait();

                if (result.Succeeded)
                {
                    return RedirectToAction(ActionsConts.ProfileSetUp, ControllersConts.Account);
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
        public IActionResult ProfileSetUp()
        {
            // Logic to add somewhere later
            //var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            //if (!user.IsProfileSettedUp)
            //{
            //
            //}
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileSetUp(UserInfoBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var userInfo = mapper.Map<UserInfo>(bindingModel);
                var result = dataService.UpdateUserInfo(User.Identity.Name, userInfo);
                if (result != null)
                {
                    return RedirectToAction(ActionsConts.Index, ControllersConts.Home);
                }
            }
            return View(bindingModel);
        }

        [Authorize]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction(ActionsConts.Index, ControllersConts.Home);
        }


        [Authorize]
        public IActionResult Profile()
        {


            return RedirectToAction(ActionsConts.Index, ControllersConts.Home);
        }

        [Authorize]
        public IActionResult ProfileSettings()
        {

            return RedirectToAction(ActionsConts.Index, ControllersConts.Home);
        }

    }
}