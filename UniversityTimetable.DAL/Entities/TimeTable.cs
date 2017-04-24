using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Entities
{
    public class TimeTable : Entity
    {
        public Group Group { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
