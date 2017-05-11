using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.Interfaces
{
    interface IEntityDTO
    {
        Guid Id { get; set; }
        DateTime CreatedDate { set; get; }
        DateTime ModifiedDate { set; get; }
    }
}
