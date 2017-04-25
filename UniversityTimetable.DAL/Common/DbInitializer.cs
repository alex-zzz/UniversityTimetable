using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
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
            context.Groups.Add(new Group { Id = new Guid("c47b2d19-bd53-41b8-98fb-fa8b0578b8ce"), Name = "Default" });
            context.Groups.Add(new Group { Name = "C# Prof (04.05.17 - 20.06.2017)" });
            context.Groups.Add(new Group { Name = "Java Script Advanced (18.05.17 - 04.07.2017)" });

            //news
            context.News.Add(new News
            {
                Title = "Я горжусь нашим с Джошуа взаимным уважением, - Кличко",
                ImagePath = "http://image.112.ua/original/2017/03/27/290036.jpg",
                Content = @"Украинский боксер Владимир Кличко рассказал о своем состоянии незадолго до боя против британца Энтони Джошуа. Об этом сообщил The Guardian.
                            Взаимное уважение.Это то, что мы показали, и я горжусь этим, - сказал Кличко."
            });

            context.News.Add(new News
            {
                Title = "Stanford scientists test links between extreme weather and climate change",
                ImagePath = "https://s3-us-west-1.amazonaws.com/stanford.ucomm.newsms.media/wp-content/uploads/2017/04/17152000/framework_shutterstock_69920845-345x230.jpg",
                Content = "After an unusually intense heat wave, downpour or drought, Noah Diffenbaugh and his research group inevitably receive phone calls and emails asking whether human-caused climate change played a role." +
                "In the past, scientists typically avoided linking individual weather events to climate change, citing the challenges of teasing apart human influence from the natural variability of the weather. But that is changing." +
                "Over the past decade, there’s been an explosion of research, to the point that we are seeing results released within a few weeks of a major event,” said Diffenbaugh, who is also the Kimmelman Family Senior Fellow at the Stanford Woods Institute for the Environment"
            });

            context.News.Add(new News
            {
                Title = "Stanford’s Generation Anthropocene podcast is back",
                ImagePath = "https://s3-us-west-1.amazonaws.com/stanford.ucomm.newsms.media/wp-content/uploads/2017/04/21102129/genanthro_Studio-345x230.jpeg",
                Content = "Started by two graduate students, the Generation Anthropocene podcast has grown into an award-winning production and beloved undergraduate course. Its fifth year brings new insight into how humans are changing the planet."
            });

            base.Seed(context);
        }
    }
}
