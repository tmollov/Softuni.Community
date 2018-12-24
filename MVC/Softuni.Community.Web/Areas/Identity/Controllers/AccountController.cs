﻿using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly Cloudinary cloudinary;

        public AccountController(UserManager<CustomUser> userMgr,
            SignInManager<CustomUser> signinMgr, IUserService userService, IMapper mapper, Cloudinary cloudinary)
        {
            this.userManager = userMgr;
            this.signInManager = signinMgr;
            this.userService = userService;
            this.mapper = mapper;
            this.cloudinary = cloudinary;
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
                user.UserInfo = userService.AddUserInfo(userInfo);

                IdentityResult result
                    = userManager.CreateAsync(user, bindingModel.Password).Result;

                if (userService.IsFirstUser())
                    userManager.AddToRoleAsync(user, Role.Admin).Wait();
                else
                    userManager.AddToRoleAsync(user, Role.User).Wait();

                if (result.Succeeded)
                {
                    return RedirectToRoute("/Identity/Account/ProfileSetUp");
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
            // Logic to add: If user profile is setted up redirect to profile settings
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
                var result = userService
                    .UpdateUserInfo(
                        userManager.FindByNameAsync(User.Identity.Name).Result,
                        bindingModel
                        );
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
            var userInfoId = userManager.FindByNameAsync(User.Identity.Name).Result.UserInfoId;
            var bindingModel = userService.GetProfileSettingsBindingModel(userInfoId);

            return this.View(bindingModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileSettings(ProfilesSettingsBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                if (bindingModel.ProfilePicture != null)
                {
                    var picPath = this.CreateTempFile(bindingModel.ProfilePicture).Result;
                    var uploadResult = UploadImage(picPath);

                    // Pass uri to binding model
                    bindingModel.ProfilePictureUrl = uploadResult.Uri.AbsoluteUri;
                }
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;
                var userInfo = mapper.Map<UserInfoBindingModel>(bindingModel);
                userService.UpdateUserInfo(user, userInfo);
                return Redirect("/Identity/Account/ProfileSettings");
            }
            return View(bindingModel);
        }
        public ImageUploadResult UploadImage(string path)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path)
            };

            return cloudinary.Upload(uploadParams);
        }


        public async Task<string> CreateTempFile(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            var stream = new FileStream(filePath, FileMode.Create);
            using (stream)
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

    }
}