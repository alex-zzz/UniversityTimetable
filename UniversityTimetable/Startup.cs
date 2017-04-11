using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityTimetable.Startup))]
namespace UniversityTimetable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
