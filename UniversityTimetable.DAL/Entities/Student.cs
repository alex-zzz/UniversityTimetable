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
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Nullable<Guid> GroupId { get; set; }
        public virtual Group Group { get; set; }

    }
}
