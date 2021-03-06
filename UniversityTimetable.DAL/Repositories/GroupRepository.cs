﻿using System;
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
            return db.Groups.AsNoTracking().Where(predicate).ToList();
        }

        public Group Get(Guid id)
        {
            return db.Groups.Find(id);
        }

        public Group GetByName(string name)
        {
            return (from g in db.Groups where g.Name == name select g).FirstOrDefault();
        }

        public IEnumerable<Group> GetAll()
        {
            return db.Groups.Include("Students").Where(g => g.Id != new Guid("c47b2d19-bd53-41b8-98fb-fa8b0578b8ce")).OrderByDescending(t => t.CreatedDate).ToList();
        }

        public void Update(Group item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Groups.AddOrUpdate(item);

        }
    }
}
