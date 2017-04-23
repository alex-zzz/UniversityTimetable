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
            CreateMap<GroupDTO, GroupViewModel>();
        }
    }
}