using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using Ninject;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.BLL.Services;


namespace UniversityTimetable.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<ITimeTableService>().To<TimeTableService>();
            //kernel.Bind<IMapper>().ToConstant(Mappings.AutoMapperConfiguration.Configure().CreateMapper()).WhenInjectedInto(typeof(HomeController));
            //kernel.Bind<IMapper>().ToConstant(Mappings.AutoMapperConfiguration.Configure().CreateMapper()).WhenInjectedInto(typeof(AccountController));
            //kernel.Bind<IMapper>().ToConstant(Mappings.AutoMapperConfiguration.Configure().CreateMapper()).WhenInjectedInto(typeof(ManageController));
        }
    }
}