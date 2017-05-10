using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;
using UniversityTimetable.DAL.Interfaces;

namespace UniversityTimetable.DAL.Repositories
{
    public class TimeTableRepository : IRepository<TimeTable>
    {
        private ApplicationDbContext db;

        public TimeTableRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(TimeTable item)
        {
            db.TimeTables.Add(item);
        }

        public void Delete(Guid id)
        {
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable != null)
            {
                db.TimeTables.Remove(timeTable);
            }
        }

        public IEnumerable<TimeTable> Find(Func<TimeTable, bool> predicate)
        {
            return db.TimeTables.AsNoTracking().Where(predicate).ToList();
        }

        public TimeTable Get(Guid id)
        {
            return db.TimeTables.Find(id);
        }

        public IEnumerable<TimeTable> GetAll()
        {
            return db.TimeTables.OrderByDescending(t=>t.CreatedDate).ToList();
        }

        public TimeTable GetByName(string name)
        {
            return null;
        }

        public void Update(TimeTable item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.TimeTables.AddOrUpdate(item);

        }
    }
}
