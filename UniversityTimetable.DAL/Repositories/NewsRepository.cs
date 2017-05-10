using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Interfaces;
using System.Data.Entity;

namespace UniversityTimetable.DAL.Repositories
{
    public class NewsRepository : IRepository<News>
    {
        private ApplicationDbContext db;

        public NewsRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(News item)
        {
            db.News.Add(item);
        }

        public void Delete(Guid id)
        {
            News news = db.News.Find(id);
            if (news != null)
            {
                db.News.Remove(news);
            }
        }

        public IEnumerable<News> Find(Func<News, bool> predicate)
        {
            return db.News.Where(predicate).ToList();
        }

        public News Get(Guid id)
        {
            return db.News.Find(id);
        }

        public News GetByName(string name)
        {
            return (from n in db.News where n.Title == name select n).FirstOrDefault();
        }

        public IEnumerable<News> GetAll()
        {
            return db.News.OrderByDescending(t => t.CreatedDate).ToList();
        }

        public void Update(News item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
