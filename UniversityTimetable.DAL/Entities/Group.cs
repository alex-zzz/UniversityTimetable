using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Entities
{
    [Table("Groups")]
    public class Group : Entity
    {
        public Group()
        {
            Students = new List<Student>();
        }

        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}
