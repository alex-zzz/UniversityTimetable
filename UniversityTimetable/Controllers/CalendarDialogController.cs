using DayPilot.Web.Mvc.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityTimetable.Common;

namespace UniversityTimetable.Controllers
{
    [HandleError]
    public class CalendarDialogController : Controller
    {

        public ActionResult New(string id)
        {
            return View(new EventManager.Event
            {
                Start = Convert.ToDateTime(Request.QueryString["start"]),
                End = Convert.ToDateTime(Request.QueryString["end"])
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            new EventManager(this).EventCreate(start, end, form["Text"], Guid.NewGuid().ToString());
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }


        public ActionResult Edit(string id)
        {
            var e = new EventManager(this).Get(id) ?? new EventManager.Event();
            return View(e);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            if (form["DeleteLesson"] == "on")
            {
                new EventManager(this).EventDelete(form["Id"]);
            }
            else
            {
                new EventManager(this).EventEdit(form["Id"], form["Text"]);
            }
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }


    }
}