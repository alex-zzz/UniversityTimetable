using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.DTO
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual List<StudentDTO> Students { get; set; }

        public virtual TimeTableDTO TimeTable { get; set; }
    }
}
