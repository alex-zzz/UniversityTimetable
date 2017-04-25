using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UniversityTimetable.Models;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Infrastructure;
using System.Collections.Generic;
using UniversityTimetable.Common.Validators;

namespace UniversityTimetable.Controllers
{
    public class AccountController : Controller
    {

        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;


        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private ITimeTableService TTService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ITimeTableService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password};
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
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
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([ModelBinder(typeof(RegisterViewModelCustomBinder))]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    FullName = model.FullName,
                    UserName = model.Email,
                    IsTermsAccepted = model.IsTermsAccepted,
                    Role = "student"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {

                    ClaimsIdentity claim = await UserService.Authenticate(userDto); 
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

                        userDto = UserService.FindByEMail(model.Email);
                        TTService.AddStudent(new StudentDTO { UserId = userDto.Id });

                        return RedirectToAction("Index", "Home");
                    }                   
                }
                
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        //private async Task SetInitialDataAsync()
        //{
        //    await UserService.SetInitialData(new UserDTO
        //    {
        //        Email = "somemail@mail.com",
        //        UserName = "admin",
        //        Password = "admin",
        //        Name = "Admin",
        //        Address = "ул. Спортивная, д.30, кв.75",
        //        Role = "admin",
        //    }, new List<string> { "admin", "manager", "student" });
        //}

    }
}