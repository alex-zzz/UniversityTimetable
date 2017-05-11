using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.DTO
{
    public class EntityDTO
    {

        public Guid Id { set; get; }

        public DateTime CreatedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }


}
