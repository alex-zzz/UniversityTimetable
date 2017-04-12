﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int FullNAme { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    //public class ServiceModule : NinjectModule
    //{
    //    private string connectionString;

    //    public ServiceModule(string connection)
    //    {
    //        connectionString = connection;
    //    }

    //    public override void Load()
    //    {
    //        //Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
    //        //Bind<IMapper>().ToConstant(CbsTest.BLL.AutoMapperConfiguration.Configure().CreateMapper()).WhenInjectedInto(typeof(LibraryService));
    //    }
    //}
}