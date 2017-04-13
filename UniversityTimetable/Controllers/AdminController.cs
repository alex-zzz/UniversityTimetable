using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityTimetable.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize (Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Students()
        {
            return View();
        }
    }
}