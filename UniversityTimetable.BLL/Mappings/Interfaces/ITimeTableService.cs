using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;

namespace UniversityTimetable.BLL.Interfaces
{
    public interface ITimeTableService
    {
        IEnumerable<StudentDTO> GetStudents();
        StudentDTO GetStudentDTOById(Guid Id);

        void AddStudent(StudentDTO studentDto);
        void UpdateStudent(StudentDTO studentDto);
        void DeleteStudent(Guid Id);


        IEnumerable<GroupDTO> GetGroups();
        GroupDTO GetGroupDTOById(Guid Id);

        void AddGroup(GroupDTO groupDto);
        void UpdateGroup(GroupDTO groupDto);
        void DeleteGroup(Guid Id);

        void Dispose();
    }
}
