using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Infrastructure;

namespace UniversityTimetable.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<OperationDetails> ChangePasswordAsync(string Id, string OldPassword, string NewPassword);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        UserDTO FindByEMail(string email);
        IEnumerable<UserDTO> GetAllUsers();
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<UserDTO> GetManagers();
    }
}
