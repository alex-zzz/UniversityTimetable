using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTimetable.BLL.Mappings
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDtoMappingProfile());
                cfg.AddProfile(new DtoToDomainMappingProfile());
            });

            return mapperConfiguration;
        }
    }
}
