using AutoMapper;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.DAL.Entities;

namespace UniversityTimetable.BLL.Mappings
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<GroupDTO, Group>();
        }
    }
}
