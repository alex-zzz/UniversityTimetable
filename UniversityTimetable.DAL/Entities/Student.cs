using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.DAL.Entities
{
    [Table("Students")]
    public class Student : Entity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

    }
}
