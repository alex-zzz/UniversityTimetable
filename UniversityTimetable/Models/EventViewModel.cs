using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityTimetable.Models
{
    public class EventViewModel
    {
        public Guid Id { get; set; }

        public int RoomNumber { get; set; }
        public string TeacherName { get; set; }

        public string Text { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Guid TimeTableId { get; set; }

        //[JsonIgnore]
        //public TimeTableViewModel TimeTable { get; set; }
    }
}