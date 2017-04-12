using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Identity
{
    public static class IdentityExtensions
    {
        public static string GetUserFullName(this IIdentity identity)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(identity.GetUserId());
            return currentUser == null ? "Anonim" : currentUser.FullName == null ? currentUser.Email : currentUser.FullName;
        }
    }
}
