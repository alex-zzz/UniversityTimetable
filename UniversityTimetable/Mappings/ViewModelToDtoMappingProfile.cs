using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.Models;

namespace UniversityTimetable.Mappings
{
    public class ViewModelToDtoMappingProfile : Profile
    {
        public ViewModelToDtoMappingProfile()
        {
            CreateMap<NewsViewModel, NewsDTO>().ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Img));

            CreateMap<GroupViewModel, GroupDTO>().PreserveReferences();

            CreateMap<TimeTableViewModel, TimeTableDTO>().ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId)).PreserveReferences();

            CreateMap<EventViewModel, EventDTO>().PreserveReferences();

        }
    }
}