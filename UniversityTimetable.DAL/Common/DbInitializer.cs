using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.DAL.Common
{
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "manager" };
            var role3 = new IdentityRole { Name = "student" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new ApplicationUser {
                Email = "admin@stanford.edu",
                UserName = "admin@stanford.edu",
                FullName = "Admin" };
            admin.ClientProfile = new ClientProfile { Name = "Admin", ApplicationUser = admin };
            string password = "Qwert!123";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);                
            }

            //Default Group
            context.Groups.Add(new Group { Name = "Default" });

            //news
            context.News.Add(new News
            {
                Title = "Я горжусь нашим с Джошуа взаимным уважением, - Кличко",
                ImagePath = "http://image.112.ua/original/2017/03/27/290036.jpg",
                Content = @"Украинский боксер Владимир Кличко рассказал о своем состоянии незадолго до боя против британца Энтони Джошуа. Об этом сообщил The Guardian.
                            Взаимное уважение.Это то, что мы показали, и я горжусь этим, - сказал Кличко."
            });

            //context.News.Add(new News
            //{
            //    Title = "",
            //    ImagePath = "",
            //    Content = ""
            //});

            base.Seed(context);
        }
    }
}
