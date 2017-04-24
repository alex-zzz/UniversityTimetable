using AutoMapper;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Identity;

namespace UniversityTimetable.BLL.Mappings
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<GroupDTO, Group>();
            CreateMap<NewsDTO, News>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}
