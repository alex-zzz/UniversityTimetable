using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class ManagerViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}