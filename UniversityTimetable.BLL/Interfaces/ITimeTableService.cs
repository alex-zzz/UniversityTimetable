using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;

namespace UniversityTimetable.BLL.Interfaces
{
    public interface ITimeTableService : IDisposable
    {
        IEnumerable<StudentDTO> GetStudents();
        StudentDTO GetStudentDTOById(Guid Id);

        void AddStudent(StudentDTO studentDto);
        void UpdateStudent(StudentDTO studentDto);
        void DeleteStudent(Guid Id);


        IEnumerable<GroupDTO> GetGroups();
        GroupDTO GetGroupDTOById(Guid Id);

        Guid AddGroup(GroupDTO groupDto);
        void UpdateGroup(GroupDTO groupDto);
        void DeleteGroup(Guid Id);

        IEnumerable<TimeTableDTO> GetTimeTables();
        TimeTableDTO GetTimeTableDTOById(Guid Id);

        Guid AddTimeTable(TimeTableDTO timeTableDto);
        void UpdateTimeTable(TimeTableDTO timeTableDto);
        void DeleteTimeTable(Guid Id);
    }
}
