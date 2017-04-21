using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace UniversityTimetable.Mappings
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDtoMappingProfile());
            });

            return mapperConfiguration;
        }
    }
}