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
        IMapper _mapper;

        public AdminController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        private ITimeTableService TTService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ITimeTableService>();
            }
        }

        public static List<StudentViewModel> sl = new List<StudentViewModel>();
        public static List<NewsViewModel> newsList = new List<NewsViewModel>();


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

            //var news1 = new NewsViewModel();
            //news1.Title = "Article 1";
            ////news1.Img = Base64ToImage("/9j/");
            //news1.Img = "data:image/jpg;base64,/9j/";
            //news1.Content = "The Stanfords engaged Frederick Law Olmsted, the famed landscape architect who created New York’s Central Park, to design the physical plan for the university.The collaboration was contentious, but finally resulted in an organization of quadrangles on an east - west axis.Today, as Stanford continues to expand, the university’s architects attempt to respect those original university plans.";

            //var news2 = new NewsViewModel();
            //news2.Title = "Article 2";
            ////news2.Img = Base64ToImage("/9j/");
            //news2.Img = "data:image/jpg;base64,/9j/";
            //news2.Content = "The Stanfords engaged Frederick Law Olmsted, the famed landscape architect who created New York’s Central Park, to design the physical plan for the university.The collaboration was contentious, but finally resulted in an organization of quadrangles on an east - west axis.Today, as Stanford continues to expand, the university’s architects attempt to respect those original university plans.";

            ////var newsList = new List<NewsViewModel>();
            //newsList.Add(news1);
            //newsList.Add(news2);
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
            //return View();



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

                    //var img = Image.FromStream(hpf.InputStream, true, true);

                    string path = "/Uploads/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    hpf.SaveAs(Server.MapPath("/Uploads/") + Path.GetFileName(hpf.FileName));

                    newsViewModel.Img = path + Path.GetFileName(hpf.FileName);

                    NewsDTO newsDto = _mapper.Map<NewsViewModel, NewsDTO>(newsViewModel);

                    _newsService.AddNews(newsDto);
                }
                return RedirectToAction("News", "Admin");
            }
            else
            return PartialView("_AddNewsFormPartial", new NewsViewModel());
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditNews(Guid id)
        {
            NewsDTO newsDto = _newsService.GetNewsDTOById(id);
            var newsViewModel = _mapper.Map<NewsDTO, NewsViewModel>(newsDto);
            //newsViewModel.ImgFile = Image.FromFile(newsViewModel.Img);

            //var news = newsList.Where(n => n.Id == id).FirstOrDefault();
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

                    //var img = Image.FromStream(hpf.InputStream, true, true);

                    string path = "/Uploads/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    hpf.SaveAs(path + Path.GetFileName(hpf.FileName));

                    newsViewModel.Img = path + Path.GetFileName(hpf.FileName);

                    NewsDTO newsDto = _mapper.Map<NewsViewModel, NewsDTO>(newsViewModel);

                    _newsService.UpdateNews(newsDto);
                    
                    //newsList.Add(newsViewModel);

                }
                //return View("News", newsList);
                return RedirectToAction("News", "Admin");
            }
            else
            return PartialView("_EditNewsFormPartial", new NewsViewModel());
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteNews(Guid Id)
        {
            _newsService.DeleteNews(Id);
            return RedirectToAction("News", "Admin");
        }

    }
}