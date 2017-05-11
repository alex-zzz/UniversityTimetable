using AutoMapper;
using System;
using System.Collections.Generic;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(INewsService newsService, ITimeTableService timeTableService, IUserService userService, IMapper mapper)
        {
            _newsService = newsService;
            _timeTableService = timeTableService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult Index()
        {
            return RedirectToAction("Students", "Admin");
        }

        //Managers

        [Authorize(Roles = "admin, manager")]
        public ActionResult Managers()
        {
            IEnumerable<UserDTO> userDtos = _userService.GetManagers();
            IEnumerable<ManagerViewModel> managerViewModels = _mapper.Map<IEnumerable<UserDTO>, List<ManagerViewModel>>(userDtos);
            return View(managerViewModels);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult AddManager()
        {
            IEnumerable<UserDTO> users = _userService.GetUsers();
            var userViewModels = _mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(users);
            ViewBag.UserList = new SelectList(userViewModels, "Id", "FullName");

            return PartialView("_AddManagerFormPartial", new ManagerViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public ActionResult AddManager(string userId)
        {
            _userService.AddManager(userId);

            return RedirectToAction("Managers", "Admin");
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteManager(string userId)
        {
            _userService.RemoveManager(userId);

            return RedirectToAction("Managers", "Admin");
        }

        //Groups

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
                    var groupId = _timeTableService.AddGroup(groupDto);
                    return Json(new { success = true, id = groupId });
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
                    GroupDTO groupDto = _timeTableService.GetGroupDTOById(groupViewModel.Id);
                    groupDto.Name = groupViewModel.Name;
                    //groupDto = _mapper.Map<GroupViewModel, GroupDTO>(groupViewModel);

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
            try
            {
                _timeTableService.DeleteGroup(id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("_ErrorDeletePartial");
            }

        }

        //Reports

        [Authorize(Roles = "admin, manager")]
        public ActionResult Reports()
        {
            return View();
        }

        //TimeTables

        [Authorize(Roles = "admin, manager")]
        public ActionResult Timetables()
        {
            IEnumerable<TimeTableDTO> timeTablesDtos = _timeTableService.GetTimeTables();
            var timeTableViewModels = _mapper.Map<IEnumerable<TimeTableDTO>, List<TimeTableViewModel>>(timeTablesDtos);
            return View(timeTableViewModels);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult AddTimeTable()
        {
            IEnumerable<GroupDTO> groupDtos = _timeTableService.GetGroups();
            var groupViewModels = _mapper.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos).Where(g=>g.TimeTable == null);
            ViewBag.GroupList = new SelectList(groupViewModels, "Id", "Name");

            var newTimeTable = new TimeTableViewModel
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddMonths(1)
            };

            return PartialView("_AddTimeTableFormPartial", newTimeTable);
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public ActionResult AddTimeTable(TimeTableViewModel timeTableViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupDTO groupDto = _timeTableService.GetGroupDTOById(new Guid(timeTableViewModel.GroupId));
                    TimeTableDTO timeTableDto = _mapper.Map<TimeTableViewModel, TimeTableDTO>(timeTableViewModel);
                    var timeTableId = _timeTableService.AddTimeTable(timeTableDto);
                    return Json(new { success = true, id = timeTableId, GroupName = groupDto.Name });
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return PartialView("_AddTimeTableFormPartial", timeTableViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTimeTable(TimeTableViewModel timeTableViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TimeTableDTO timeTableDto = _mapper.Map<TimeTableViewModel, TimeTableDTO>(timeTableViewModel);

                    _timeTableService.UpdateTimeTable(timeTableDto);
                    return Json(new { success = true });
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return PartialView("_EditTimeTableFormPartial", timeTableViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteTimeTable(Guid id)
        {
            try
            {
                _timeTableService.DeleteTimeTable(id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("_ErrorDeletePartial");
            }
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult RedirectToTimeTableEventsEditor(Guid id)
        {
            var timeTableId = id;
            return Json(Url.Action("EditTimeTableEvents", new { id = timeTableId }), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult EditTimeTableEvents(Guid id)
        {
            TimeTableDTO timeTableDto = _timeTableService.GetTimeTableDTOById(id);
            TimeTableViewModel timeTableViewModel = _mapper.Map<TimeTableDTO, TimeTableViewModel>(timeTableDto);

            return View(timeTableViewModel);
        }

        //Students

        [Authorize(Roles = "admin, manager")]
        public ActionResult Students()
        {
            IEnumerable<StudentDTO> studentDtos = _timeTableService.GetStudents();
            var studentViewModels = _mapper.Map<IEnumerable<StudentDTO>, List<StudentViewModel>>(studentDtos);
            return View(studentViewModels);
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult EditStudent(Guid id)
        {
            IEnumerable<GroupDTO> groupDtos = _timeTableService.GetGroups();
            var groupViewModels = _mapper.Map<IEnumerable<GroupDTO>, List<GroupViewModel>>(groupDtos);
            ViewBag.GroupList = new SelectList(groupViewModels, "Id", "Name");

            IEnumerable<StudentDTO> studentDtos = _timeTableService.GetStudents();
            var studentViewModels = _mapper.Map<IEnumerable<StudentDTO>, List<StudentViewModel>>(studentDtos);

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
                //studentDto.Group = defaultGroupDto;
                studentDto.GroupId = defaultGroupDto.Id;
            }

            _timeTableService.UpdateStudent(studentDto);

            return RedirectToAction("Students", "Admin");
        }

        //News
       
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
        public ActionResult DeleteNews(Guid id)
        {
            _newsService.DeleteNews(id);
            return RedirectToAction("News", "Admin");
        }

    }
}