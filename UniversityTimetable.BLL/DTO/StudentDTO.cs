using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        public UserDTO User { get; set; }

    }
}
