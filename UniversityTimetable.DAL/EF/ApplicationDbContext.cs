using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
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
        public DbSet<Event> Events { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }


        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext("DefaultConnection");
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>().Property(d => d.CreatedDate).HasColumnType("datetime2");
            modelBuilder.Entity<Entity>().Property(d => d.ModifiedDate).HasColumnType("datetime2");

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public override int SaveChanges()
        {
            var CreatedEntities = ChangeTracker.Entries<Entity>().Where(E => E.State == EntityState.Added).ToList();

            CreatedEntities.ForEach(E =>
            {
                E.Entity.CreatedDate = DateTime.Now;
            });

            var ModifiedEntities = ChangeTracker.Entries<Entity>().Where(E => E.State == EntityState.Modified).ToList();

            ModifiedEntities.ForEach(E =>
            {
                E.Entity.ModifiedDate = DateTime.Now;
            });

            return base.SaveChanges();
        }
    }
}
