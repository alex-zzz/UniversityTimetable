﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UniversityTimetable.Models;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.BLL.DTO;
using System.Security.Claims;

namespace UniversityTimetable.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IUserService _userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager _signInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ManageController()
        {
        }

        public IAuthenticationManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<IAuthenticationManager>();
            }
        }

        public IUserService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = "",// await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = false,// await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = null, //await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }


        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succedeed)
            {
                UserDTO userDto = new UserDTO { Email = User.Identity.GetUserName(), Password = model.NewPassword };
                ClaimsIdentity claim = await UserManager.Authenticate(userDto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login/password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //if (user != null)
            //{
            //    return user.PasswordHash != null;
            //}
            return true;
        }

        private bool HasPhoneNumber()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //if (user != null)
            //{
            //    return user.PhoneNumber != null;
            //}
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}