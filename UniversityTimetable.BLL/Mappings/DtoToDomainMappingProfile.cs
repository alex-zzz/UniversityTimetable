﻿using AutoMapper;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.BLL.Mappings
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<StudentDTO, Student>().PreserveReferences();
            CreateMap<GroupDTO, Group>().PreserveReferences();

            CreateMap<TimeTableDTO, TimeTable>().PreserveReferences();
            CreateMap<EventDTO, Event>().PreserveReferences();

            CreateMap<NewsDTO, News>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}
