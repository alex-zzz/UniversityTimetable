using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.DAL.Common
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем роли
            var role1 = new IdentityRole { Id = "admin",    Name = "admin" };
            var role2 = new IdentityRole { Id = "manager",  Name = "manager" };
            var role3 = new IdentityRole { Id = "student",  Name = "student" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new ApplicationUser
            {
                Id = "c3045e0c-48b7-444e-b38b-bd30ff87f51c",
                Email = "admin@stanford.edu",
                UserName = "admin@stanford.edu",
                FullName = "Admin"
            };
            admin.ClientProfile = new ClientProfile { Name = "Admin", ApplicationUser = admin };
            string password = "Qwert!123";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роли
                userManager.AddToRoles(admin.Id, role1.Name, role2.Name, role3.Name);
            }

            var manager1 = new ApplicationUser
            {
                Id = "50c9a856-ee28-4678-a236-ae434185e053",
                Email = "manager1@stanford.edu",
                UserName = "manager1@stanford.edu",
                FullName = "Rumblday Rumbldon"
            };
            manager1.ClientProfile = new ClientProfile { Name = "John Smith", ApplicationUser = manager1 };
            password = "Qwert!123";
            result = userManager.Create(manager1, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(manager1.Id, role2.Name);
            }

            var user1 = new ApplicationUser
            {
                Id = "6c476311-8a1c-4db2-9447-cf969975100c",
                Email = "student1@stanford.edu",
                UserName = "student1@stanford.edu",
                FullName = "John Smith"
            };
            user1.ClientProfile = new ClientProfile { Name = "John Smith", ApplicationUser = user1 };
            password = "Qwert!123";
            result = userManager.Create(user1, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(user1.Id, role3.Name);
            }

            var user2 = new ApplicationUser
            {
                Id = "79ae07fc-d86b-41b6-93fc-6c2c2dd997c0",
                Email = "student2@stanford.edu",
                UserName = "student2@stanford.edu",
                FullName = "Elthon John"
            };
            user2.ClientProfile = new ClientProfile { Name = "Elthon John", ApplicationUser = user2 };
            password = "Qwert!123";
            result = userManager.Create(user2, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(user2.Id, role3.Name);
            }

            //Default Group
            context.Groups.Add(new Group { Id = new Guid("c47b2d19-bd53-41b8-98fb-fa8b0578b8ce"), Name = "Default" });
            context.Groups.Add(new Group { Id = new Guid("44c93470-f488-4433-865c-2c707bfc9661"), Name = "C# Prof (04.05.17 - 20.06.2017)" });
            context.Groups.Add(new Group { Id = new Guid("7771485d-1156-4657-a085-f8f33504603f"), Name = "Java Script Advanced (18.05.17 - 04.07.2017)" });

            context.Students.Add(new Student { UserId = user1.Id, GroupId = new Guid("44c93470-f488-4433-865c-2c707bfc9661") });
            context.Students.Add(new Student { UserId = user2.Id, GroupId = new Guid("44c93470-f488-4433-865c-2c707bfc9661") });

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
