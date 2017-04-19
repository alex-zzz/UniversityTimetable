using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.EF;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Interfaces;
using System.Data.Entity;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.DAL.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private ApplicationDbContext db;

        public StudentRepository(ApplicationDbContext context)
        {
            db = context; 
        }

        public void Create(Student item)
        {
            db.Students.Add(item);
        }

        public void Delete(Guid id)
        {
            Student student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
            }
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return db.Students.Where(predicate).ToList();
        }

        public Student Get(Guid id)
        {
            return db.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students.ToList();
        }

        public void Update(Student item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
