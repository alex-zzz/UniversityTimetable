using AutoMapper;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.BLL.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<Group, GroupDTO>();
            CreateMap<News, NewsDTO>();
            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
