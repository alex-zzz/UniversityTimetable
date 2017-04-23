using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class StudentViewModel
    {

        public Guid Id { get; set; }
 
        [Required]
        [Display(Name = "Number")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required]
        //[Display(Name = "Surname")]
        //public string Surname { get; set; }

        public string Group { get; set; }
    }
}