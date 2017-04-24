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
    public class EventRepository : IRepository<Event>
    {
        private ApplicationDbContext db;

        public EventRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(Event item)
        {
            db.Events.Add(item);
        }

        public void Delete(Guid id)
        {
            Event @event = db.Events.Find(id);
            if (@event != null)
            {
                db.Events.Remove(@event);
            }
        }

        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            return db.Events.AsNoTracking().Where(predicate).ToList();
        }

        public Event Get(Guid id)
        {
            return db.Events.Find(id);
        }

        public IEnumerable<Event> GetAll()
        {
            return db.Events.ToList();
        }

        public void Update(Event item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
