using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Infrastructure;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Interfaces;

namespace UniversityTimetable.BLL.Services
{
    public class TimeTableService : ITimeTableService
    {
        IMapper _mapper;
        IUnitOfWork Database { get; set; }

        public TimeTableService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            Database = uow;
        }

        public void AddGroup(GroupDTO groupDto)
        {
            if (Database.Groups.GetAll().Where(a => a.Name == groupDto.Name).Count() > 0)
                throw new ValidationException("Такая группа уже существует в БД!", "");

            Group group = _mapper.Map<GroupDTO, Group>(groupDto);

            Database.Groups.Create(group);
            Database.Save();
        }

        public void AddStudent(StudentDTO studentDto)
        {
            if (studentDto != null && studentDto.Group != null)
            {
                if (Database.Students.GetAll().Where(a => a.User.Id == studentDto.User.Id && a.Group.Id == studentDto.Group.Id).Count() > 0)
                    throw new ValidationException("Такой студент уже существует в этой группе!", "");
            }

            Student student = _mapper.Map<StudentDTO, Student>(studentDto);
            if (student.Group == null)
            {
                //student.Group = Database.Groups.GetByName("Default");
                //student.GroupId = Database.Groups.GetByName("Default").Id;
                student.GroupId = Database.Groups.Get(new Guid("c47b2d19-bd53-41b8-98fb-fa8b0578b8ce")).Id;
            }

            Database.Students.Create(student);
            Database.Save();
        }

        public void DeleteGroup(Guid id)
        {
            Database.Groups.Delete(id);
            Database.Save();
        }

        public void DeleteStudent(Guid id)
        {
            Database.Students.Delete(id);
            Database.Save();
        }

        public StudentDTO GetStudentDTOById(Guid id)
        {
            Student student = Database.Students.Get(id);
            //Student student = Database.Students.Find(s => s.Id == id).FirstOrDefault();

            return _mapper.Map<Student, StudentDTO>(student);
        }

        public GroupDTO GetGroupDTOById(Guid id)
        {
            Group group = Database.Groups.Get(id);
            return _mapper.Map<Group, GroupDTO>(group);
        }

        public IEnumerable<StudentDTO> GetStudents()
        {
            return _mapper.Map<IEnumerable<Student>, List<StudentDTO>>(Database.Students.GetAll());
        }

        public IEnumerable<GroupDTO> GetGroups()
        {
            return _mapper.Map<IEnumerable<Group>, List<GroupDTO>>(Database.Groups.GetAll());
        }

        public void UpdateStudent(StudentDTO studentDto)
        {
            if (studentDto != null && studentDto.Group != null)
            {
                //if (Database.Students.Find(a => a.User.Id == studentDto.User.Id && a.Group.Id == studentDto.Group.Id).Any())
                if (Database.Students.GetAll().Where(a => a.User.Id == studentDto.User.Id && a.Group.Id == studentDto.Group.Id).Count() > 0)
                    throw new ValidationException("Такой студент уже существует в этой группе!", "");

            }

            Student student = _mapper.Map<StudentDTO, Student>(studentDto);

            Database.Students.Update(student);
            Database.Save();
        }

        public void UpdateGroup(GroupDTO groupDto)
        {
            if (Database.Groups.Find(g => g.Name == groupDto.Name).Any())
                throw new ValidationException("Такая группа уже существует в БД!", "");

            Group group = _mapper.Map<GroupDTO, Group>(groupDto);

            Database.Groups.Update(group);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
