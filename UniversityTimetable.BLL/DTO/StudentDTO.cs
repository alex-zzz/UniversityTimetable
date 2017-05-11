using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.DTO
{
    public class StudentDTO : EntityDTO
    {
        //public Guid Id { get; set; }

        public string UserId { get; set; }
        public UserDTO User { get; set; }

        public Guid GroupId { get; set; }
        public GroupDTO Group { get; set; }
    }
}
