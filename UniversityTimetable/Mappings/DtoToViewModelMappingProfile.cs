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
            CreateMap<StudentDTO, StudentViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Group.Id.ToString()))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name));
        }
    }
}