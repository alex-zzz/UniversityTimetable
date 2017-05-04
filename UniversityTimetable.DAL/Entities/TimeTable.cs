using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Entities
{
    public class TimeTable : Entity
    {
        public TimeTable()
        {
            Events = new List<Event>();
        }

        public virtual ICollection<Event> Events { get; set; }

        public virtual Group Group { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
