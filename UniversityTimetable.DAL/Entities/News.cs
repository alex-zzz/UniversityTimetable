using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Entities
{
    [Table("News")]
    public class News : Entity
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
    }
}
