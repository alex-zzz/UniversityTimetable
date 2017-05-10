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
        readonly IMapper _mapper;
        IUnitOfWork Database { get; set; }

        public TimeTableService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            Database = uow;
        }

        //Groups

        public IEnumerable<GroupDTO> GetGroups()
        {
            return _mapper.Map<IEnumerable<Group>, List<GroupDTO>>(Database.Groups.GetAll());
        }

        public GroupDTO GetGroupDTOById(Guid id)
        {
            Group group = Database.Groups.Get(id);
            return _mapper.Map<Group, GroupDTO>(group);
        }

        public Guid AddGroup(GroupDTO groupDto)
        {
            if (Database.Groups.Find(g => g.Name == groupDto.Name).Any())
                throw new ValidationException("Такая группа уже существует в БД!", "");

            var group = _mapper.Map<GroupDTO, Group>(groupDto);

            Database.Groups.Create(group);
            Database.Save();

            return group.Id;
        }

        public void UpdateGroup(GroupDTO groupDto)
        {
            if (Database.Groups.Find(g => g.Name == groupDto.Name).Any())
                throw new ValidationException("Такая группа уже существует в БД!", "");

            Group group = _mapper.Map<GroupDTO, Group>(groupDto);

            Database.Groups.Update(group);
            Database.Save();
        }

        public void DeleteGroup(Guid id)
        {
            if (Database.Groups.Get(id).Students.Any())
                throw new ValidationException("Нельзя удалить группу в которой присутствуют студенты", "");

            Database.Groups.Delete(id);
            Database.Save();
        }

        //Timetables

        public IEnumerable<TimeTableDTO> GetTimeTables()
        {
            return _mapper.Map<IEnumerable<TimeTable>, List<TimeTableDTO>>(Database.TimeTables.GetAll());
        }

        public TimeTableDTO GetTimeTableDTOById(Guid id)
        {
            TimeTable timeTable = Database.TimeTables.Find(t => t.Id == id).FirstOrDefault();
            return _mapper.Map<TimeTable, TimeTableDTO>(timeTable);
        }

        public Guid AddTimeTable(TimeTableDTO timeTableDTO)
        {
            TimeTable timeTable = _mapper.Map<TimeTableDTO, TimeTable>(timeTableDTO);
            timeTable.Group = Database.Groups.Get(timeTableDTO.GroupId);

            Database.TimeTables.Create(timeTable);
            Database.Save();

            return timeTable.Id;
        }

        public void UpdateTimeTable(TimeTableDTO timeTableDTO)
        {
            //TimeTable timeTable = _mapper.Map<TimeTableDTO, TimeTable>(timeTableDTO);

            //var existingTimeTable = Database.TimeTables.Find(t => t.Id == timeTableDTO.Id).FirstOrDefault();
            //var existingEvents = existingTimeTable.Events.ToList();
            //var updatedEvents = timeTable.Events.ToList();

            //var addedEvents = updatedEvents.Where(e => !existingEvents.Select(ee => ee.Id).Contains(e.Id)).ToList();
            //var deletedEvents = existingEvents.Where(e => !updatedEvents.Select(ee => ee.Id).Contains(e.Id)).ToList();
            //var modifiedEvents = updatedEvents.Where(e => !addedEvents.Select(ee => ee.Id).Contains(e.Id)).ToList();

            //foreach (var addedEvent in addedEvents)
            //{
            //    Database.Events.Create(addedEvent);
            //}


            //Database.TimeTables.Update(timeTable);
            Database.Save();
        }

        public void DeleteTimeTable(Guid id)
        {
            if (Database.TimeTables.Get(id).Events.Any())
                throw new ValidationException("Нельзя удалить расписание в котором определены события", "");

            Database.TimeTables.Delete(id);
            Database.Save();
        }

        //Studendts

        public IEnumerable<StudentDTO> GetStudents()
        {
            return _mapper.Map<IEnumerable<Student>, List<StudentDTO>>(Database.Students.GetAll());
        }

        public StudentDTO GetStudentDTOById(Guid id)
        {
            Student student = Database.Students.Get(id);
            return _mapper.Map<Student, StudentDTO>(student);
        }

        public void AddStudent(StudentDTO studentDto)
        {
            if (studentDto != null && studentDto.Group != null)
            {
                //if (Database.Students.GetAll().Where(a => a.User.Id == studentDto.User.Id && a.Group.Id == studentDto.Group.Id).Count() > 0)
                if (Database.Students.Find(a => a.User.Id == studentDto.User.Id && a.Group.Id == studentDto.Group.Id).Any())
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

        public void UpdateStudent(StudentDTO studentDto)
        {
            Student student = _mapper.Map<StudentDTO, Student>(studentDto);

            Database.Students.Update(student);
            Database.Save();
        }

        public void DeleteStudent(Guid id)
        {
            Database.Students.Delete(id);
            Database.Save();
        }

        //Events

        public EventDTO GetEventDTOById(Guid id)
        {
            Event @event = Database.Events.Find(e => e.Id == id).FirstOrDefault();

            EventDTO eventDto = new EventDTO
            {
                Id = @event.Id,
                Start = @event.Start,
                End = @event.End,
                Text = @event.Text,
                RoomNumber = @event.RoomNumber,
                TeacherName = @event.TeacherName,
                TimeTableId = @event.TimeTableId
            };

            //EventDTO eventDto = _mapper.Map<Event, EventDTO>(@event);

            return eventDto;
        }

        public Guid AddEvent(EventDTO eventDto)
        {
            Event @event = _mapper.Map<EventDTO, Event>(eventDto);

            Database.Events.Create(@event);
            Database.Save();

            return @event.Id;
        }

        public void UpdateEvent(EventDTO eventDto)
        {
            Event @event = _mapper.Map<EventDTO, Event>(eventDto);

            Database.Events.Update(@event);
            Database.Save();
        }

        public void DeleteEvent(Guid id)
        {
            Database.Events.Delete(id);
            Database.Save();
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
