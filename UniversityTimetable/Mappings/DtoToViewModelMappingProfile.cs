using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.Models;

namespace UniversityTimetable.Mappings
{
    public class DtoToViewModelMappingProfile : Profile
    {
        public DtoToViewModelMappingProfile()
        {
            CreateMap<NewsDTO, NewsViewModel>().ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.ImagePath));

            CreateMap<GroupDTO, GroupViewModel>().PreserveReferences();

            CreateMap<TimeTableDTO, TimeTableViewModel>().ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name)).PreserveReferences();

            CreateMap<EventDTO, EventViewModel>().PreserveReferences();

            CreateMap<StudentDTO, StudentViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Group.Id.ToString()))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name));

            CreateMap<UserDTO, ManagerViewModel>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserDTO, UserViewModel>();
        }
    }
}