﻿using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.Interfaces;
using UniversityTimetable.DAL.Repositories;
using UniversityTimetable.BLL.Services;

namespace UniversityTimetable.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IMapper>().ToConstant(Mappings.AutoMapperConfiguration.Configure().CreateMapper()).WhenInjectedInto(typeof(TimeTableService));
        }
    }
}
