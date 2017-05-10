using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class TimeTableViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Group")]
        public string GroupId { get; set; }
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "Finish date")]
        public DateTime End { get; set; }

        public List<EventViewModel> Events { get; set; }
    }
}