using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.Common;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new DbInitializer());
        }

        public ApplicationDbContext(string connectionString)
            //:base(connectionString) 
            : base(connectionString, throwIfV1Schema: false)
        { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<News> News { get; set; }

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext("DefaultConnection");
        //}
    }
}
