using Microsoft.AspNet.Identity;
using System.Security.Principal;
using UniversityTimetable.BLL.DTO;
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
