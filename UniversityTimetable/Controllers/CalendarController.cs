using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityTimetable.Common;

namespace UniversityTimetable.Controllers
{
    [HandleError]
    public class CalendarController : Controller
    {

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        public class Dpc : DayPilotCalendar
        {
            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                string name = (string)e.Data["name"];
                if (String.IsNullOrEmpty(name))
                {
                    name = "(default)";
                }
                new EventManager(Controller).EventCreate(e.Start, e.End, name);
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Calendar.EventMoveArgs e)
            {
                if (new EventManager(Controller).Get(e.Id) != null)
                {
                    new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                }

                Update();
            }

            protected override void OnEventClick(EventClickArgs e)
            {

            }

            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Calendar.EventResizeArgs e)
            {
                new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            private int i = 0;
            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                if (Id == "dpc_customization")
                {
                    // alternating color
                    int colorIndex = i % 4;
                    string[] backColors = { "#FFE599", "#9FC5E8", "#B6D7A8", "#EA9999" };
                    string[] borderColors = { "#F1C232", "#3D85C6", "#6AA84F", "#CC0000" };
                    e.BackgroundColor = backColors[colorIndex];
                    e.BorderColor = borderColors[colorIndex];
                    i++;
                }
            }

            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime)e.Data["start"];
                        Update(CallBackUpdateType.Full);
                        break;

                    case "refresh":
                        Update();
                        break;

                    case "previous":
                        StartDate = StartDate.AddDays(-7);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "next":
                        StartDate = StartDate.AddDays(7);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;

                }
            }

            protected override void OnInit(InitArgs initArgs)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                // this select is a really bad example, no where clause
                Events = new EventManager(Controller).Data.AsEnumerable();

                /*
                foreach (DataRow dr in (EnumerableRowCollection) Events)
                {
                    dr["text"] = "custom HTML";
                }
                 */

                DataStartField = "start";
                DataEndField = "end";
                DataTextField = "text";
                DataIdField = "id";
            }

        }
    }
}