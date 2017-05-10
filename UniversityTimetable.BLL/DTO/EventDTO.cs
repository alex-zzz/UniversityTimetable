using System;

namespace UniversityTimetable.BLL.DTO
{
    public class EventDTO
    {
        public Guid Id { get; set; }

        public int RoomNumber { get; set; }
        public string TeacherName { get; set; }

        public string Text { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Guid TimeTableId { get; set; }
        public TimeTableDTO TimeTable { get; set; }
    }
}
