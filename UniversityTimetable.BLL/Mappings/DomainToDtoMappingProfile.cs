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
            CreateMap<Student, StudentDTO>().PreserveReferences();
            CreateMap<Group, GroupDTO>().PreserveReferences();
            CreateMap<News, NewsDTO>();
            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
