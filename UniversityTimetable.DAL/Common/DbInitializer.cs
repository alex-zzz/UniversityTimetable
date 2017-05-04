using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
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

            // создаем роли
            var role1 = new IdentityRole { Id = "admin", Name = "admin" };
            var role2 = new IdentityRole { Id = "manager", Name = "manager" };
            var role3 = new IdentityRole { Id = "student", Name = "student" };

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
                userManager.AddToRoles(admin.Id, role1.Name);
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
                userManager.AddToRoles(manager1.Id, role2.Name, role3.Name);
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
                FullName = "Elton John"
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

            var group1 = new Group
            {
                Id = new Guid("44c93470-f488-4433-865c-2c707bfc9661"),
                Name = "C# Prof (cs001)"
            };

            context.Groups.Add(group1);

            var group2 = new Group
            {
                Id = new Guid("7771485d-1156-4657-a085-f8f33504603f"),
                Name = "Java Script Advanced (js001)"
            };

            context.Groups.Add(group2);

            var group3 = new Group
            {
                Id = new Guid(),
                Name = "HTML5 Advanced (htm001)"
            };

            context.Groups.Add(group3);

            var timeTable1 = new TimeTable
            {
                Group = group1,
                Start = new DateTime(2017, 5, 5),
                End = new DateTime(2017, 6, 5)
            };

            context.TimeTables.Add(timeTable1);

            var timeTable2 = new TimeTable
            {
                Group = group2,
                Start = new DateTime(2017, 5, 12),
                End = new DateTime(2017, 6, 12)
            };

            context.TimeTables.Add(timeTable2);

            context.Events.Add(new Event
            {
                Start = new DateTime(2017, 5, 5, 9, 0, 0),
                End = new DateTime(2017, 5, 5, 10, 30, 0),
                RoomNumber = 18,
                TeacherName = "Anders Hejlsberg",
                TimeTableId = timeTable1.Id,
                Text = "Intro C#"
            });

            context.Events.Add(new Event
            {
                Start = new DateTime(2017, 5, 6, 9, 0, 0),
                End = new DateTime(2017, 5, 6, 10, 30, 0),
                RoomNumber = 18,
                TeacherName = "Anders Hejlsberg",
                TimeTableId = timeTable1.Id,
                Text = "Lesson 1 C#"
            });

            context.Events.Add(new Event
            {
                Start = new DateTime(2017, 5, 12, 9, 0, 0),
                End = new DateTime(2017, 5, 12, 10, 30, 0),
                RoomNumber = 12,
                TeacherName = "Brendan Eich",
                TimeTableId = timeTable2.Id,
                Text = "Intro JS"
            });

            context.Events.Add(new Event
            {
                Start = new DateTime(2017, 5, 13, 9, 0, 0),
                End = new DateTime(2017, 5, 13, 10, 30, 0),
                RoomNumber = 12,
                TeacherName = "Brendan Eich",
                TimeTableId = timeTable2.Id,
                Text = "Lesson 1 JS"
            });

            context.Students.Add(new Student { UserId = user1.Id, GroupId = new Guid("44c93470-f488-4433-865c-2c707bfc9661") });
            context.Students.Add(new Student { UserId = user2.Id, GroupId = new Guid("44c93470-f488-4433-865c-2c707bfc9661") });

            //news
            context.News.Add(new News
            {
                Title = "Stanford undergrads win Lemelson-MIT Student Prize for work",
                ImagePath = "https://s3-us-west-1.amazonaws.com/stanford.ucomm.newsms.media/wp-content/uploads/2017/04/11161234/Lemelson-795x530.jpg",
                Content = "A team of Stanford ChEM - H undergraduates has won the Lemelson - MIT Student Prize for their development of proteins that could combat multidrug - resistant bacteria, which the World Health Organization has described as one of the most serious public health threats the world faces today. " +
                "Maria Filsinger Interrante, now a student in the School of Medicine’s Medical Scientist Training Program, and seniors Christian Choe and Zach Rosenthal will share a $10,000 award that comes with the prize, which is presented to promising young inventors around the country." +
                "Multidrug resistance is exactly as it sounds: Since the first use of penicillin in the 1940s, some disease-causing bacteria have evolved the ability to withstand multiple antibiotic drugs. The danger, however, lies not just in their sophisticated defenses against current antibiotics, but in their ability to evade any newly developed antibiotics of the future. Choe, Filsinger Interrante and Rosenthal thought what was needed was an entirely new class of antibiotic – one that bacteria can’t evolve defenses against." +
                "Their idea was to engineer a bacteria-killing protein with a special feature: If bacteria evolved resistance to one of the protein’s attacks, it would make itself susceptible to others." +
                "The trio, collectively called Team Lyseia, began as part of a biotech entrepreneurship student club spearheaded by ChEM-H director Chaitan Khosla and life-science industry veterans Zachary Sweeney, an alumnus, and Cameron Gray. During weekly 90-minute meetings, club members learned how to develop scientific ideas into proposals for biotech startups. After their pitch, Khosla gave them $10,000 and access to ChEM-H’s Macromolecular Structure Knowledge Center to develop their idea and run proof-of-concept tests. The team is now working on larger-scale studies and is looking into forming a company around the project."
            });

            context.News.Add(new News
            {
                Title = "Stanford scientists test links between extreme weather and climate change",
                ImagePath = "https://s3-us-west-1.amazonaws.com/stanford.ucomm.newsms.media/wp-content/uploads/2017/04/17152000/framework_shutterstock_69920845-345x230.jpg",
                Content = "After an unusually intense heat wave, downpour or drought, Noah Diffenbaugh and his research group inevitably receive phone calls and emails asking whether human-caused climate change played a role." +
                "In the past, scientists typically avoided linking individual weather events to climate change, citing the challenges of teasing apart human influence from the natural variability of the weather. But that is changing." +
                "Over the past decade, there’s been an explosion of research, to the point that we are seeing results released within a few weeks of a major event,” said Diffenbaugh, who is also the Kimmelman Family Senior Fellow at the Stanford Woods Institute for the Environment" +
                "In a new study, published in this week’s issue of Proceedings of the National Academy of Sciences, Diffenbaugh and a group of current and former Stanford colleagues outline a four-step “framework” for testing whether global warming has contributed to record-setting weather events. The new paper is the latest in a burgeoning field of climate science called “extreme event attribution,” which combines statistical analyses of climate observations with increasingly powerful computer models to study the influence of climate change on individual extreme weather events." +
                "The Stanford research team, which includes a number of former students and postdocs who have moved on to positions at other universities, has been developing the extreme event framework in recent years, focusing on individual events such as the 2012-2017 California drought and the catastrophic flooding in northern India in June 2013. In the new study, a major goal was to test the ability of the framework to evaluate events in multiple regions of the world, and to extend beyond extreme temperature and precipitation, which have been the emphasis of most event attribution studies."
            });

            context.News.Add(new News
            {
                Title = "Stanford’s Generation Anthropocene podcast is back",
                ImagePath = "https://s3-us-west-1.amazonaws.com/stanford.ucomm.newsms.media/wp-content/uploads/2017/04/21102129/genanthro_Studio-345x230.jpeg",
                Content = "Started by two graduate students, the Generation Anthropocene podcast has grown into an award-winning production and beloved undergraduate course. Its fifth year brings new insight into how humans are changing the planet." +
                "Generation Anthropocene was developed from within Stanford Earth thanks to generous support from Dean Pamela Matson. In recent years, the project has also received funding from Worldview Stanford and the Vice Provost for Teaching and Learning (VPTL). This year, thanks to extra support from VPTL, Stanford Earth, the Vice Provost for Undergraduate Education, and the HAAS Center for Public Service, the Podcasting the Anthropocene course was offered for two quarters. The first focused on interviewing and story development; the second quarter included writing for the ear, voicing narration and other more advanced audio production skills." +
                "From the start, Generation Anthropocene was envisioned as a way to create a cross-generational dialogue between experts who’ve spent years studying the world and young people about to journey into it. Traer and Osborne know all too well that their students, who represent a variety of majors, are profoundly affected by the idea that humans are having geological-scale impacts on Earth." +
                "Over time, the issues that students want to explore has evolved. Traer said food and water scarcity were the initial hot topics, then biodiversity loss. This year, the students gravitated toward discussing environmental policy under the new administration. Members of the class are encouraged to be ambitious with their projects, stretching the bounds of what the podcast covers, reaching out to well-known experts at Stanford and beyond. By Osborne’s count, they’ve interviewed six or seven MacArthur Foundation grant awardees following hours in research and preparation. The goal is to empower the students to ask meaningful, relevant questions while also communicating their own curiosity and concerns." +
                "From late 2013 to spring 2015, Osborne, Traer and Chang took some time away from the podcast to hone their skills – and in the case of Osborne and Traer, to finish their PhDs. Since returning in 2015, the podcast has featured a mix of student pieces and episodes produced by Traer, Osborne and Chang. But with two quarters’ worth of student material for this season, they plan to come back in a big way this spring."
            });

            base.Seed(context);
        }
    }
}
