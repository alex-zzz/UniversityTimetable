using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.DTO
{
    public class TimeTableDTO
    {
        public Guid Id { get; set; }

        public virtual IEnumerable<EventDTO> Events { get; set; }

        public Guid GroupId { get; set; }
        public virtual GroupDTO Group { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
