using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.DAL.Entities
{
    public class News : Entity
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
    }
}
