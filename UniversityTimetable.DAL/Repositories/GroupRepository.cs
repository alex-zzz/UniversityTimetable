using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;
using UniversityTimetable.DAL.Interfaces;

namespace UniversityTimetable.DAL.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private ApplicationDbContext db;

        public GroupRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(Group item)
        {
            db.Groups.Add(item);
        }

        public void Delete(Guid id)
        {
            Group group = db.Groups.Find(id);
            if (group != null)
            {
                db.Groups.Remove(group);
            }
        }

        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            return db.Groups.Where(predicate).ToList();
        }

        public Group Get(Guid id)
        {
            return db.Groups.Find(id);
        }

        public IEnumerable<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        public void Update(Group item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
