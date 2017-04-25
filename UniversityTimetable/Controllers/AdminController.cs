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
using UniversityTimetable.BLL.Infrastructure;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.Models;

namespace UniversityTimetable.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ITimeTableService _timeTableService;
        private readonly IMapper _mapper;

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
            //student.Number = "B123456";
            student.Name = "Mahesh";
            //student.Surname = "Chand";

            StudentViewModel student2 = new StudentViewModel();
            //student2.Number = "B123457";
            student2.Name = "Mahesh2";
            //student2.Surname = "Chand2";

            sl.Add(student);
            sl.Add(student2);
        }

        // GET: Admin
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index()
        {
            return RedirectToAction("Students", "Admin");
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult Managers()
        {
            //IEnumerable<GroupDTO> groupDtos = _timeTableService.GetGroups();
            IEnumerable<ManagerViewModel> managerViewModels = null;
            return View(managerViewModels);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult Groups()
        {
            IEnumerable<GroupDTO> groupDtos = _timeTableService.GetGroups();
            var groupViewModels = _mapper.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);
            return View(groupViewModels);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult AddGroup()
        {
            return PartialView("_AddGroupFormPartial", new GroupViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroup(GroupViewModel groupViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupDTO groupDto = _mapper.Map<GroupViewModel, GroupDTO>(groupViewModel);
                    _timeTableService.AddGroup(groupDto);
                    return Json(new { success = true });
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return PartialView("_AddGroupFormPartial", groupViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult EditGroup(Guid id)
        {
            GroupDTO groupDto = _timeTableService.GetGroupDTOById(id);
            var groupViewModel = _mapper.Map<GroupDTO, GroupViewModel>(groupDto);
            return PartialView("_EditGroupFormPartial", groupViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(GroupViewModel groupViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupDTO groupDto = _mapper.Map<GroupViewModel, GroupDTO>(groupViewModel);

                    _timeTableService.UpdateGroup(groupDto);
                    return Json(new { success = true });
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return PartialView("_EditGroupFormPartial", groupViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteGroup(Guid id)
        {
            _timeTableService.DeleteGroup(id);
            return RedirectToAction("Groups", "Admin");
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult Reports()
        {
            return View(sl);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult Timetables()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        //public ActionResult GetStudents()
        //{
        //    return Json(sl, JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "admin, manager")]
        public ActionResult Students()
        {
            IEnumerable<StudentDTO> studentDtos = _timeTableService.GetStudents();
            var studentViewModels = _mapper.Map<IEnumerable<StudentDTO>, List<StudentViewModel>>(studentDtos);
            return View(studentViewModels);
        }

        //[Authorize(Roles = "admin")]
        //public ActionResult GetGroups()
        //{
        //    IEnumerable<GroupDTO> groupDtos = _timeTableService.GetGroups();
        //    var groupViewModels = _mapper.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);
        //    return Json(groupViewModels, JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "admin, manager")]
        public ActionResult EditStudent(Guid id)
        {
            IEnumerable<GroupDTO> groupDtos = _timeTableService.GetGroups();
            var groupViewModels = _mapper.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);
            ViewBag.GroupList = new SelectList(groupViewModels, "Id", "Name");

            IEnumerable<StudentDTO> studentDtos = _timeTableService.GetStudents();
            var studentViewModels = _mapper.Map<IEnumerable<StudentDTO>, List<StudentViewModel>>(studentDtos);

            //var student = sl.FirstOrDefault(s => s.Id == id);
            return PartialView("_EditStudentFormPartial", studentViewModels.FirstOrDefault(s => s.Id == id));
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(StudentViewModel studentViewModel)
        {
            StudentDTO studentDto = _timeTableService.GetStudentDTOById(studentViewModel.Id);

            if (studentViewModel.GroupId != null)
            {
                GroupDTO groupDto = _timeTableService.GetGroupDTOById(new Guid(studentViewModel.GroupId));
                studentDto.Group = groupDto;
                studentDto.GroupId = new Guid(studentViewModel.GroupId);
            }
            else
            {
                GroupDTO defaultGroupDto = _timeTableService.GetGroupDTOById(new Guid("c47b2d19-bd53-41b8-98fb-fa8b0578b8ce"));
                studentDto.Group = defaultGroupDto;
                studentDto.GroupId = defaultGroupDto.Id;
            }

            _timeTableService.UpdateStudent(studentDto);

            //var student = sl.FirstOrDefault(s => s.Id == studentViewModel.Id);

            //if (studentViewModel != null && studentViewModel.GroupId != null)
            //{
            //    GroupDTO groupDto = _timeTableService.GetGroupDTOById(new Guid(studentViewModel.GroupId));
            //    var groupViewModel = _mapper.Map<GroupDTO, GroupViewModel>(groupDto);
            //    studentViewModel.GroupName = groupViewModel.Name;

            //    if (student != null)
            //    {
            //        student.GroupName = studentViewModel.GroupName;
            //        student.GroupId = studentViewModel.GroupId;
            //    }

            //}
            //else
            //{
            //    if (student != null) student.GroupName = "";
            //}

            //sl.Add(student);
            return RedirectToAction("Students", "Admin");
        }

        //[Authorize(Roles = "admin, manager")]
        //public ActionResult DeleteStudent(Guid Id)
        //{
        //    return new EmptyResult();
        //}

        [Authorize(Roles = "admin, manager")]
        public ActionResult News()
        {
            IEnumerable<NewsDTO> newsDtos = _newsService.GetNews();
            var newsViewModels = _mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos);
            return View(newsViewModels);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult AddNews()
        {
            return PartialView("_AddNewsFormPartial", new NewsViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public ActionResult AddNews(NewsViewModel newsViewModel)
        {
            Request.Files.Get(0);
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file];

                    if (hpf != null && hpf.ContentLength != 0)
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
                }

                NewsDTO newsDto = _mapper.Map<NewsViewModel, NewsDTO>(newsViewModel);
                _newsService.AddNews(newsDto);

                return RedirectToAction("News", "Admin");
            }
            else
                return PartialView("_AddNewsFormPartial", newsViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult EditNews(Guid id)
        {
            NewsDTO newsDto = _newsService.GetNewsDTOById(id);
            var newsViewModel = _mapper.Map<NewsDTO, NewsViewModel>(newsDto);
            return PartialView("_EditNewsFormPartial", newsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
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
                }

                NewsDTO newsDto = _mapper.Map<NewsViewModel, NewsDTO>(newsViewModel);
                _newsService.UpdateNews(newsDto);

                return RedirectToAction("News", "Admin");
            }
            else
                return PartialView("_EditNewsFormPartial", newsViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteNews(Guid Id)
        {
            _newsService.DeleteNews(Id);
            return RedirectToAction("News", "Admin");
        }

    }
}