using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmah;
using MrCMS.Services;
using MrCMS.Services.Resources;
using MrCMS.Web.Apps.Core.ModelBinders;
using MrCMS.Web.Apps.Core.Models.RegisterAndLogin;
using MrCMS.Web.Apps.Core.Pages;
using MrCMS.Web.Apps.Core.Services;
using MrCMS.Website.Binders;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Core.Controllers
{
    public class LoginController : MrCMSAppUIController<CoreApp>
    {
        private readonly ILoginService _loginService;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly IStringResourceProvider _stringResourceProvider;
        private readonly IUniquePageService _uniquePageService;
        private readonly IUserLookup _userLookup;

        public LoginController(IResetPasswordService resetPasswordService, IUniquePageService uniquePageService,
            ILoginService loginService, IStringResourceProvider stringResourceProvider, IUserLookup userLookup)
        {
            _resetPasswordService = resetPasswordService;
            _uniquePageService = uniquePageService;
            _loginService = loginService;
            _stringResourceProvider = stringResourceProvider;
            _userLookup = userLookup;
        }

        [HttpGet]
        public ViewResult Show(LoginPage page, LoginModel model)
        {
            ModelState.Clear();
            ViewData["login-model"] = TempData["login-model"] as LoginModel ?? model;
            return View(page);
        }

        [HttpPost]
        public async Task<RedirectResult> Post([IoCModelBinder(typeof (LoginModelModelBinder))] LoginModel loginModel)
        {
            if (loginModel != null && ModelState.IsValid)
            {
                var result = await _loginService.AuthenticateUser(loginModel);
                if (result.Success)
                    return Redirect(result.RedirectUrl);
                loginModel.Message = result.Message;
            }
            TempData["login-model"] = loginModel;

            return _uniquePageService.RedirectTo<LoginPage>();
        }

        [HttpGet]
        public ViewResult ForgottenPassword(ForgottenPasswordPage page)
        {
            ViewData["message"] = TempData["message"];
            return View(page);
        }

        [HttpPost]
        public ActionResult ForgottenPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["message"] = _stringResourceProvider.GetValue("Login Email Not Recognized",
                    "Email not recognized.");
                return _uniquePageService.RedirectTo<ForgottenPasswordPage>();
            }

            var user = _userLookup.GetUserByEmail(email);

            if (user != null)
            {
                _resetPasswordService.SetResetPassword(user);
                TempData["message"] =
                    _stringResourceProvider.GetValue("Login Password Reset",
                        "We have sent password reset details to you. Please check your spam folder if this is not received shortly.");
            }
            else
            {
                TempData["message"] = _stringResourceProvider.GetValue("Login Email Not Recognized",
                    "Email not recognized.");
            }

            return _uniquePageService.RedirectTo<ForgottenPasswordPage>();
        }


        [HttpGet]
        public ActionResult PasswordReset(ResetPasswordPage page, Guid id)
        {
            var user = _userLookup.GetUserByResetGuid(id);

            if (user == null)
                return Redirect("~");

            ViewData["ResetPasswordViewModel"] = new ResetPasswordViewModel(id, user);

            return View(page);
        }

        [HttpPost]
        public RedirectResult PasswordReset(ResetPasswordViewModel model)
        {
            try
            {
                _resetPasswordService.ResetPassword(model);
                return _uniquePageService.RedirectTo<LoginPage>();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return _uniquePageService.RedirectTo<LoginPage>();
            }
        }
    }
}