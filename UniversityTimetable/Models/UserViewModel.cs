﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
    }
}