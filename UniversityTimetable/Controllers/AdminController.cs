using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.Models;

namespace UniversityTimetable.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        INewsService _newsService;
        ITimeTableService _timeTableService;
        IMapper _mapper;

        public AdminController(INewsService newsService, ITimeTableService timeTableService, IMapper mapper)
        {
            _newsService = newsService;
            _timeTableService = timeTableService;
            _mapper = mapper;
        }

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
        public ActionResult Reports()
        {
            return View(sl);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Timetables()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        //public ActionResult GetStudents()
        //{
        //    return Json(sl, JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "admin")]
        public ActionResult Students()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddStudent()
        {
            return PartialView("_AddStudentFormPartial", new StudentViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddStudent(StudentViewModel sVM)
        {
            return PartialView("_AddStudentFormPartial", new StudentViewModel());
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditStudent(Guid id)
        {
            var student = sl.Where(s => s.Id == id).FirstOrDefault();
            return PartialView("_EditStudentFormPartial", student);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditStudent(StudentViewModel sVM)
        {
            return PartialView("_AddStudentFormPartial", new StudentViewModel());
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteStudent(Guid Id)
        {
            return new EmptyResult();
        }

        [Authorize(Roles = "admin")]
        public ActionResult News()
        {
            IEnumerable<NewsDTO> newsDtos = _newsService.GetNews();
            var newsViewModels = _mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos);
            return View(newsViewModels);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddNews()
        {
            return PartialView("_AddNewsFormPartial", new NewsViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddNews(NewsViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file];

                    if (hpf.ContentLength != 0)
                    {
                        string path = "/Uploads/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        hpf.SaveAs(Server.MapPath(path) + Path.GetFileName(hpf.FileName));
                        newsViewModel.Img = path + Path.GetFileName(hpf.FileName);
                    }
                    else
                    {
                        newsViewModel.Img = "https://placehold.it/350x200";
                    }

                    NewsDTO newsDto = _mapper.Map<NewsViewModel, NewsDTO>(newsViewModel);
                    _newsService.AddNews(newsDto);
                }
                return RedirectToAction("News", "Admin");
            }
            else
            return PartialView("_AddNewsFormPartial", newsViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditNews(Guid id)
        {
            NewsDTO newsDto = _newsService.GetNewsDTOById(id);
            var newsViewModel = _mapper.Map<NewsDTO, NewsViewModel>(newsDto);
            return PartialView("_EditNewsFormPartial", newsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditNews(NewsViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file];

                    if (hpf.ContentLength != 0)
                    {
                        string path = "/Uploads/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        hpf.SaveAs(Server.MapPath(path) + Path.GetFileName(hpf.FileName));
                        newsViewModel.Img = path + Path.GetFileName(hpf.FileName);
                    }

                    NewsDTO newsDto = _mapper.Map<NewsViewModel, NewsDTO>(newsViewModel);
                    _newsService.UpdateNews(newsDto);
                }
                return RedirectToAction("News", "Admin");
            }
            else
            return PartialView("_EditNewsFormPartial", newsViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteNews(Guid Id)
        {
            _newsService.DeleteNews(Id);
            return RedirectToAction("News", "Admin");
        }

    }
}