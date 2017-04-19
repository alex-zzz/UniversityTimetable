using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class StudentViewModel
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (this._id == Guid.Empty)
                    this._id = Guid.NewGuid();
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        [Required]
        [Display(Name = "Number")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }
}