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
using System.Security.Principal;
using UniversityTimetable.BLL.Services;

namespace UniversityTimetable.Utils
{
    public static class IdentityExtensions
    {
        public static string GetUserFullName(this IIdentity identity)
        {
            UserDTO currentUser = UserService.FindById(identity.GetUserId());
            return currentUser == null ? "Anonim" : currentUser.Name == null ? currentUser.Email : currentUser.Name;
        }
    }
}
