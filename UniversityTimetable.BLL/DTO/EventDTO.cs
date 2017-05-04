using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.DTO
{
    public class EventDTO
    {
        public int RoomNumber { get; set; }
        public string TeacherName { get; set; }

        public string Text { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Guid TimeTableId { get; set; }
        public TimeTableDTO TimeTable { get; set; }
    }
}
