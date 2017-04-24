using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityTimetable.Models
{
    public class StudentViewModel
    {

        public Guid _id;

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

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Group")]
        public string GroupId { get; set; }
        public string GroupName { get; set; }
    }
}