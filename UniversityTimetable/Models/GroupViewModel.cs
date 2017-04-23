using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Group name")]
        public string Name { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}