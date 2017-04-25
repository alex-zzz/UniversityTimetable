using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Infrastructure;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.DAL.Identity;
using UniversityTimetable.DAL.Interfaces;
using UniversityTimetable.DAL.Repositories;
using System;

namespace UniversityTimetable.BLL.Services
{
    public class UserService : IUserService
    {
        static IUnitOfWork StaticDatabase { get; set; }
        IUnitOfWork Database { get; set; }
        IMapper _mapper;

        static UserService()
        {
            StaticDatabase = new EFUnitOfWork("DefaultConnection");
        }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
            _mapper = Mappings.AutoMapperConfiguration.Configure().CreateMapper();
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email, IsTermsAccepted = userDto.IsTermsAccepted, FullName = userDto.FullName };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.FullName };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successful!", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login/EMail already exists.", "Email");
            }
        }

        public async Task<OperationDetails> ChangePasswordAsync(string Id, string OldPassword, string NewPassword)
        {
            ApplicationUser au = Database.UserManager.FindById(Id);

            if (au == null)
            {
                return new OperationDetails(false, "Can't change the password!", "");
            }
            else
            {
                var result = await Database.UserManager.ChangePasswordAsync(Id, OldPassword, NewPassword);
                if (result.Succeeded)
                {
                    return new OperationDetails(true, "Password is changed!", "");
                }
                else
                {
                    return new OperationDetails(false, result.ToString(), "");
                }
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public static UserDTO FindById(string id)
        {
            ApplicationUser au = StaticDatabase.UserManager.FindById(id);
            if (au == null)
            { return null; }
            else
            {
                UserDTO user = new UserDTO { Name = au.FullName, FullName = au.FullName, Id = au.Id, Email = au.Email, Address = au.ClientProfile.Address };
                return user;
            }
        }

        public UserDTO FindByEMail(string email)
        {
            ApplicationUser au = Database.UserManager.FindByEmail(email);
            if (au == null)
            { return null; }
            else
            {
                UserDTO user = new UserDTO { Name = au.FullName, FullName = au.FullName, Id = au.Id, Email = au.Email, Address = au.ClientProfile.Address };
                return user;
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _mapper.Map<List<ApplicationUser>, List<UserDTO>>(Database.UserManager.Users.ToList());
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _mapper.Map<List<ApplicationUser>, List<UserDTO>>((from user in Database.UserManager.Users
                                                                      where user.Roles.Count == 0
                                                                      select user).ToList());
        }

        public IEnumerable<UserDTO> GetManagers()
        {
            return _mapper.Map<List<ApplicationUser>, List<UserDTO>>((from user in Database.UserManager.Users
                                                                      where user.Roles.Any(r => r.RoleId == "manager")
                                                                      select user).ToList());
        }
    }
}
