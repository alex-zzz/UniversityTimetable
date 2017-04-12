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
        public static string GetUserName(this IIdentity identity) {
            return identity
        }
    }
}
