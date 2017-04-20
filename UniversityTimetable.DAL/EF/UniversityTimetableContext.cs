using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.DAL.EF
{
    public class UniversityTimetableContext : ApplicationDbContext
    {
        public UniversityTimetableContext(string connectionString)
            : base(connectionString)
        { }
    }
}
