using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Transactions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Ajax.Utilities;
using Microsoft.Web.WebPages.OAuth;
using Mvc.JQuery.Datatables;
using Org.BouncyCastle.Bcpg;
using Web.Models.Account;
using Web.Service;
using WebMatrix.WebData;
using Web.Filters;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public partial class AccountController : Controller
    {

        private IUserCreationConfirmationService _creationConfirmationService;
        private IResetPasswordInformationService _resetPasswordInformationService;

        private UsersContext _usersContext;
        // GET: /Account/Login
        public AccountController(IUserCreationConfirmationService userCreationConfirmationService, UsersContext usersContext, IResetPasswordInformationService resetPasswordInformationService)
        {
            _creationConfirmationService = userCreationConfirmationService;
            _resetPasswordInformationService = resetPasswordInformationService;
            _usersContext = usersContext;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var u = requestContext.HttpContext.User;
            var data = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == u.Identity.Name);

            ViewBag.UserData = data;
        }


        #region login

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe) && WebSecurity.IsConfirmed(model.UserName))
            {
                return RedirectToLocal(returnUrl);
            }
            if (!WebSecurity.IsConfirmed(model.UserName))
            {
                ModelState.AddModelError("", "The user has not been confirmed.");
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
        #endregion
        #region Logoff
        public virtual ActionResult LogOff()
        {
            WebSecurity.Logout();
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            System.Web.HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Account");
        }
        #endregion
        #region KeepAlive
        [HttpPost]
        public virtual JsonResult KeepAlive()
        {
            return Json("OK");
        }

        #endregion
        #region Forgot Password Provides UI when user forgets the password and want to reset it
        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ForgotPassword(ForgotPasswordModel resetPasswordRequestModel)
        {

            var request = HttpContext.Request;

            if (ModelState.IsValid)
            {

                var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.Email == resetPasswordRequestModel.EmailAddress);
                if (userProfile == null)
                {
                    //If the user is null the show error page
                    ModelState.AddModelError("EmailAddress","This Email is not associated with any user account.");
                    return View();
                }
                if (WebSecurity.UserExists(userProfile.UserName))
                {
                    //if user has not been confirmed
                    if (!WebSecurity.IsConfirmed(userProfile.UserName))
                    {
                        var confirmLink = Url.Action("ResendConfirmation", new { email = userProfile.Email });
                        var confirmationUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, confirmLink);
                        return View("ShowMessage", new ShowMessageModel { Message = "This account has not been confirmed to resend please click this link", ActionLink = confirmationUrl, ActionMessage = "Resend Confirmation" });
                    }
                    var resetPasswordToken = WebSecurity.GeneratePasswordResetToken(userProfile.UserName);
                    var url = Url.Action("ResetPassword", new { userName = userProfile.UserName, resetPasswordToken = resetPasswordToken });
                    var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
                    _resetPasswordInformationService.SendPasswordResetEMail(baseUrl, userProfile.Email, userProfile.UserName, "Change Password - Vendor Portal");
                    return RedirectToAction("ShowMessage", new ShowMessageModel { Message = String.Format("Email has been sent to {0}.  You should receive a link to reset your password in the next 10 minutes.", userProfile.Email), ActionLink = Url.Action("Login", "Account"), ActionMessage = "Go back to login" });
                }
                //Something wrong no user

                return View();
            }
            return View();
        }

        #endregion
        #region ResetPassword Provides the UI for User to reset the password
        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string resetPasswordToken, string userName)
        {
            var request = HttpContext.Request;

            var loginUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, Url.Action("Login"));
            var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == userName);

            if (userProfile == null)
            {
                return View("ShowMessage", new ShowMessageModel { Message = string.Format("The user '{0}' is not present in the system.", userName), ActionLink = loginUrl, ActionMessage = "Go back to login" });
            }

            var model = new ResetPasswordModel();
            model.UserName = userName;
            model.PasswordResetToken = resetPasswordToken;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = WebSecurity.ResetPassword(resetPasswordModel.PasswordResetToken, resetPasswordModel.Password);
                if (user)
                {
                    return View("ShowMessage", new ShowMessageModel
                    {
                        ActionLink = Url.Action("Login", "Account"),
                        ActionMessage = "Click to login with new password",
                        Message = "Password has been successfully changed."

                    });
                }

                return View("ShowMessage", new ShowMessageModel
                {
                    ActionLink = Url.Action("Login", "Account"),
                    ActionMessage = "Click to login with new password",
                    Message = "Password reset has been done already."

                });
            }
            return View();
        }
        #endregion
        #region ConfirmAccount Provides UI to user to confirm his account
        [HttpGet]
        [AllowAnonymous]

        public virtual ActionResult ConfirmAccount(string userName, string confirmationToken)
        {
            var request = HttpContext.Request;

            var loginUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, Url.Action("Login"));
            var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == userName);

            if (userProfile == null)
            {
                return View("ShowMessage", new ShowMessageModel { Message = string.Format("The user '{0}' is not present in the system.", userName), ActionLink = loginUrl, ActionMessage = "Go back to login" });    
            }

            var model = new ConfirmPasswordForTheAccountModel();
            model.UserName = userName;
            model.ConfirmationToken = confirmationToken;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ConfirmAccount(ConfirmPasswordForTheAccountModel confirmPasswordForTheAccount)
        {
            if (ModelState.IsValid)
            {
                var IsConfirmAccount = WebSecurity.ConfirmAccount(confirmPasswordForTheAccount.UserName, confirmPasswordForTheAccount.ConfirmationToken);
                var IsConfirmPassword = WebSecurity.ChangePassword(confirmPasswordForTheAccount.UserName, "123456", confirmPasswordForTheAccount.Password);
                if (IsConfirmPassword)
                {
                    var userprofile =
                        _usersContext.UserProfiles.FirstOrDefault(
                            x => x.UserName == confirmPasswordForTheAccount.UserName);
                    if (userprofile != null)
                    {
                        userprofile.IsConfirmed = true;
                        _usersContext.SaveChanges();
                    }
                    if (WebSecurity.Login(confirmPasswordForTheAccount.UserName, confirmPasswordForTheAccount.Password))
                    {
                        //return RedirectToAction("List", "Transaction");
                        return RedirectToAction("Welcome", "Transaction");
                    }
                }

                return View("ShowMessage", new ShowMessageModel
                {
                    ActionLink = Url.Action("Login", "Account"),
                    ActionMessage = "Click to login",
                    Message = "Account Confirmation has been done already."

                });
            }
            return View(confirmPasswordForTheAccount);
        }
        #endregion
        #region Resend Confirmation mail provides UI is user wants the system to resend the confirmation mail

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ResendConfirmation(string email)
        {

            var request = HttpContext.Request;

            var loginUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, Url.Action("Login"));

            var userprofile = _usersContext.UserProfiles.FirstOrDefault(x => x.Email == email);
            if (userprofile == null)
            {
                return View("ShowMessage", new ShowMessageModel { Message = string.Format("{0} not present in the system", email), ActionLink = loginUrl, ActionMessage = "Go back to login" });
            }
            var url = Url.Action("ConfirmAccount", new { userName = userprofile.UserName, confirmationToken = userprofile.ConfirmationToken });
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
            _creationConfirmationService.SendConfirmationEmail(baseUrl, userprofile.Email, userprofile.UserName, "Resend Confirmation Link - Vendor Portal");
            return View("ShowMessage", new ShowMessageModel { Message = "Confirmation mail has been sent", ActionLink = loginUrl, ActionMessage = "Go back to login" });

        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ShowMessage(ShowMessageModel model)
        {
            return View(model);
        }

        #region NonAjax Register provides the admin to register a new user Obsolete
        
        #endregion
        #region Manage User Ajaxified UserManagement Actions returns JSON

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator,Checker")]
        public virtual ActionResult Manage()
        {
            var model = new RegisterModel();
            model.Roles = GetUserRoles(HttpContext.User);
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult Listusers(DataTablesParam dataTablesParam)
        {
            var totalRecords = _usersContext.UserProfiles.Count();
            var query = _usersContext.UserProfiles.AsQueryable();

            var allowedRoles = GetUserRoles(User);

            var data =
                _usersContext.UserProfiles
                    .WithRoles(allowedRoles)
                    .WithGlobalValue(dataTablesParam.sSearch)
                    .WithPagination(dataTablesParam.iDisplayStart, dataTablesParam.iDisplayLength)
                    .Select(x => new UserListViewModel()
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Role = x.Role,
                        EmailId = x.Email,
                        IsConfirmed = x.IsConfirmed
                    }).ToList();
            return Json(new
            {
                sEcho = dataTablesParam.sEcho,
                iTotalDisplayRecords = data.Count,
                iTotalRecords = totalRecords,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult Register(RegisterModel model)
        {
            IPrincipal currentUser = HttpContext.User;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                    if (WebSecurity.UserExists(model.UserName))
                    {
                        return Json(new { Success = false, ErrorMessage = "User name already exists. Please enter a different user name." });
                    }
                    if (_usersContext.UserProfiles.Any(x => x.Email == model.EmailAddress))
                    {
                        return Json(new { Success = false, ErrorMessage = "A user name for that Email address already exists. Please enter a different Email address." });
                    }

                    //No error
                    var confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, "123456",
                         new { UserName = model.UserName, Email = model.EmailAddress, Role = model.SelectedRole, IsConfirmed = false, FirstName = model.FirstName, LastName = model.LastName }, true);

                    //Get it from the dbcontext to save the confirmation
                    var userprofile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == model.UserName);
                    if (userprofile != null)
                    {
                        userprofile.ConfirmationToken = confirmationToken;
                        _usersContext.SaveChanges();
                    }
                    //This will come to this step only when there is a successful creation of the user
                    Roles.AddUserToRole(model.UserName, model.SelectedRole);
                    var urlHelper = new UrlHelper(HttpContext.Request.RequestContext);

                    var request = HttpContext.Request;
                    var url = Url.Action("ConfirmAccount", new { userName = model.UserName, confirmationToken = confirmationToken });
                    var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
                    _creationConfirmationService.SendConfirmationEmail(baseUrl, model.EmailAddress, model.UserName, "Confirmation for creation of user - Vendor Portal");
                    return Json(new { Success = true }); ;
                }
                catch (MembershipCreateUserException e)
                {
                    return Json(new { Success = false, ErrorMessage = e.StatusCode });

                }
            }
            return Json(new { Success = false, ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage) });
        }
        [HttpPost]
        public virtual JsonResult Edit(RegisterModel model)
        {

            var userprofile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == model.UserName);
            if (userprofile != null)
            {
                Roles.RemoveUserFromRole(model.UserName, userprofile.Role);
                userprofile.Role = model.SelectedRole;
                userprofile.FirstName = model.FirstName;
                userprofile.LastName = model.LastName;
                Roles.AddUserToRole(model.UserName, model.SelectedRole);
                _usersContext.SaveChanges();
            }
            return Json(new { Success = true });
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator,ClientAdministrator")]
        public virtual JsonResult DeleteUser(string userName)
        {
            bool ownUser = (userName == User.Identity.Name);
            if (Roles.GetRolesForUser(userName).Any())
            {
                Roles.RemoveUserFromRoles(userName, Roles.GetRolesForUser(userName));
            }
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(userName); // deletes record from webpages_Membership table
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(userName, true); // deletes record from UserProfile table
            return Json(new { Success = true, IsOwnUser = ownUser });
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdministrator,ClientAdministrator")]
        public virtual JsonResult ResetUserPassword(string userName)
        {
            var request = HttpContext.Request;
            var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == userName);
            if (userProfile == null)
            {
                return
                    Json(
                        new
                        {
                            Success = false,
                            ErrorMessage = string.Format("{0} is not present in the system", userName)
                        });
            }
            var resetPasswordToken = WebSecurity.GeneratePasswordResetToken(userProfile.UserName);
            var url = Url.Action("ResetPassword", new { userName = userProfile.UserName, resetPasswordToken = resetPasswordToken });
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
            _resetPasswordInformationService.SendPasswordResetEMail(baseUrl, userProfile.Email, userProfile.UserName, "Change Password - Vendor Portal");
            return Json(new { Success = true });
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdministrator,ClientAdministrator")]
        public virtual JsonResult GenerateConfirmationLink(string userName)
        {

            var request = HttpContext.Request;
            var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == userName);
            if (userProfile == null)
            {
                return
                    Json(
                        new
                        {
                            Success = false,
                            ErrorMessage = string.Format("{0} is not present in the system", userName)
                        });
            }
            var url = Url.Action("ConfirmAccount", new { userName = userProfile.UserName, confirmationToken = userProfile.ConfirmationToken });
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
            _creationConfirmationService.SendConfirmationEmail(baseUrl, userProfile.Email, userProfile.UserName, "Confirmation for creation of user - Vendor Portal");
            return Json(new { Success = true });
        }
        #endregion

        [HttpGet]
        public virtual ActionResult UserSettings()
        {
            var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var model = new UserSettingsModel();
            if (userProfile != null)
            {
                model.FirstName = userProfile.FirstName;
                model.LastName = userProfile.LastName;
                model.Email = userProfile.Email;
                ViewBag.IsSaveSuccessful = "false";
                return View(model);    
            }
            return View(model);    
        }

        [HttpPost]
        public virtual ActionResult UserSettings(UserSettingsModel model)
        {
            var userProfile = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
            try
            {
                if (userProfile != null)
                {
                    userProfile.FirstName = model.FirstName;
                    userProfile.LastName = model.LastName;
                    _usersContext.SaveChanges();
                    ViewBag.IsSaveSuccessful = "true";
                }
            return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UserSettings", ex);
                throw;
            }

            
        }

        public virtual JsonResult SaveUserPassword(string currentPassword, string newPassword)
        {
            var userName = User.Identity.Name;
            if (WebSecurity.ChangePassword(userName, currentPassword, newPassword))
            {
                return Json(new {result = "success"});
            }
            return Json(new {result = "failed"});
        }


        #region Helpers
        [Authorize(Roles = "SuperAdministrator,ClientAdministrator")]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && !returnUrl.ToUpper().Contains("HL7ENTRYDETAILS"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction("List", "Transaction");
                return RedirectToAction("Welcome", "Transaction");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }



        protected override void OnResultExecuting(ResultExecutingContext context)
        {
            System.Web.HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
            System.Web.HttpContext.Current.Response.AddHeader("Cache-Control", "private"); //to be safe cross browser
        }




        private IList<string> GetUserRoles(IPrincipal currentUser)
        {
            if (currentUser.IsInRole("SuperAdministrator"))
            {
                return new List<string>() { "SuperAdministrator", "Checker", "Maker" };
            }
            else if (currentUser.IsInRole("Checker"))
            {
                return new List<string>() { "Checker", "Maker" };
            }
            return null;
        }
         


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
