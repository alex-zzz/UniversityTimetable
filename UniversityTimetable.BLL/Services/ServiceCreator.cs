using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.DAL.Repositories;

namespace UniversityTimetable.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new EFUnitOfWork(connection));
        }

        public ITimeTableService CreateTTService(string connection)
        {
            return new TimeTableService(new EFUnitOfWork(connection), Mappings.AutoMapperConfiguration.Configure().CreateMapper());
        }

        public INewsService CreateNewsService(string connection)
        {
            return new NewsService(new EFUnitOfWork(connection), Mappings.AutoMapperConfiguration.Configure().CreateMapper());
        }
    }
}
