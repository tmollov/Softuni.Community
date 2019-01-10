using System.IO;
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
using Softuni.Community.Web.Models.ViewModels;
using Error = Softuni.Community.Web.Common.Error;

namespace Softuni.Community.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : BaseController
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;
        private readonly IUserService userService;
        private readonly IDiscussionsService discussionsService;
        private readonly IMapper mapper;
        private readonly Cloudinary cloudinary;

        public AccountController(UserManager<CustomUser> userMgr,
            SignInManager<CustomUser> signinMgr,
            IUserService userService,
            IDiscussionsService discussionsService,
            IMapper mapper,
            Cloudinary cloudinary)
        {
            this.userManager = userMgr;
            this.signInManager = signinMgr;
            this.userService = userService;
            this.discussionsService = discussionsService;
            this.mapper = mapper;
            this.cloudinary = cloudinary;
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            var vm = new LoginBindingModel() { ReturnUrl = ReturnUrl };

            return View(vm);
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
                        {
                            if (bindingModel.ReturnUrl != null)
                            {
                                return Redirect(bindingModel.ReturnUrl);
                            }
                            return RedirectToAction(Actions.Index, Paths.Home);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(LoginBindingModel.Password),
                            Error.PasswordNotValid);
                        // Will returning empty password field
                        bindingModel.Password = string.Empty;
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginBindingModel.Email),
                        Error.UserWithGivenEmailNotFound);
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
                    return Redirect($"/{Area.Identity}/{Paths.Account}/{Actions.ProfileSetUp}");
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
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            if (!user.IsProfileSetUp)
            {
                return Redirect($"/{Area.Identity}/{Paths.Account}/{Actions.Profile}");
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ProfileSetUp(UserInfoBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;
                var result = userService.UpdateUserInfo(user, bindingModel);
                userService.FinishSetUp(user.Id);
                return RedirectToAction(Actions.Index, Paths.Home);
            }
            return View(bindingModel);
        }

        [Authorize]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction(Actions.Index, Paths.Home);
        }

        [Authorize]
        public IActionResult DeleteProfile()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteProfile(DeleteProfileBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                CustomUser user = userManager.FindByNameAsync(username).Result;
                if (user != null)
                {
                    var isPasswordRight = userManager.CheckPasswordAsync(user, bindingModel.Password).Result;

                    if (isPasswordRight)
                    {
                        signInManager.SignOutAsync().GetAwaiter().GetResult();
                        var res = userManager.DeleteAsync(user).Result;
                        return RedirectToAction(Actions.Index, Paths.Home);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(DeleteProfileBindingModel.Password),
                            Error.PasswordNotValid);
                        // Will returning empty password field
                        bindingModel.Password = string.Empty;
                    }
                }
                else
                {
                    return RedirectToAction(Actions.SomethingWentWrong, Paths.Error);
                }
            }
            return View(bindingModel);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            var questions = discussionsService.GetUserQuestionsVM(User.Identity.Name);
            var answers = discussionsService.GetUserAnswersVM(User.Identity.Name);
            var myProfile = userService.GetProfileViewModel(user.UserInfoId);
            var vm = new ProfileViewModel()
            {
                MyQuestions = questions,
                MyAnswers = answers,
                MyProfile = myProfile
            };
            return this.View(vm);
        }

        [Authorize]
        public IActionResult ProfileSettings()
        {
            var userInfoId = userManager.FindByNameAsync(User.Identity.Name).Result.UserInfoId;
            var bindingModel = userService.GetProfileSettingsBindingModel(userInfoId);

            return this.View(bindingModel);
        }

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
                return Redirect($"/{Area.Identity}/{Paths.Account}/{Actions.ProfileSettings}");
            }
            return View(bindingModel);
        }

        private ImageUploadResult UploadImage(string path)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path)
            };

            return cloudinary.Upload(uploadParams);
        }

        private async Task<string> CreateTempFile(IFormFile file)
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