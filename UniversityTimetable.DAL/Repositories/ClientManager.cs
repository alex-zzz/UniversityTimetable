using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using UniversityTimetable.DAL.Identity;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Interfaces;

namespace UniversityTimetable.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationDbContext Database { get; set; }
        public ClientManager(ApplicationDbContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
