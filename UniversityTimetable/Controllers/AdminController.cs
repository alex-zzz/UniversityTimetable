using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityTimetable.Models;

namespace UniversityTimetable.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public static List<StudentViewModel> sl = new List<StudentViewModel>();

        static AdminController()
        {
            StudentViewModel student = new StudentViewModel();
            student.Number = "B123456";
            student.Name = "Mahesh";
            student.Surname = "Chand";

            StudentViewModel student2 = new StudentViewModel();
            student2.Number = "B123457";
            student2.Name = "Mahesh2";
            student2.Surname = "Chand2";

            sl.Add(student);
            sl.Add(student2);
        }


        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return RedirectToAction("Students", "Admin");
        }


        [Authorize(Roles = "admin")]
        public ActionResult Students()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Reports()
        {
            //return View();



            return View(sl);
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetStudents()
        {

            return Json(sl, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteStudent(Guid Id)
        {


            return new EmptyResult();

        }

        [Authorize(Roles = "admin")]
        public ActionResult AddStudent()
        {
            return PartialView("_StudentFormPartial", new StudentViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddStudent(StudentViewModel sVM)
        {
            return PartialView("_StudentFormPartial", new StudentViewModel());
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditStudent(Guid id)
        {
            var student = sl.Where(s => s.Id == id).FirstOrDefault();
            return PartialView("_StudentFormPartial", student);
        }

        [Authorize(Roles = "admin")]
        public ActionResult News()
        {
            return View();
        }

    }
}