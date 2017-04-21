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
    public class HomeController : Controller
    {
        public static List<NewsViewModel> newsList = new List<NewsViewModel>();

        INewsService _newsService;
        IMapper _mapper;

        //private INewsService NewsService
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<INewsService>();
        //    }
        //}

        public HomeController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult News()
        {
            IEnumerable<NewsDTO> newsDtos = _newsService.GetNews();
            var newsViewModels = _mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos);

            return View(newsViewModels);
        }

        [ChildActionOnly]
        public ActionResult GetFooterNews(int quantity)
        {
            IEnumerable<NewsDTO> newsDtos = _newsService.GetNews();
            var newsViewModels = _mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos);

            return PartialView("_FooterNewsPartial", newsViewModels.Take(quantity));
        }

        public ActionResult GetDetailNews(Guid id)
        {
            NewsDTO newsDto = _newsService.GetNewsDTOById(id);
            var newsViewModel = _mapper.Map<NewsDTO, NewsViewModel>(newsDto);

            return View(newsViewModel);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}