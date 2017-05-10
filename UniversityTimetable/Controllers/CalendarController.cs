using AutoMapper;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Json;
using System;
using System.Web.Mvc;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.Models;

namespace UniversityTimetable.Controllers
{
    [HandleError]
    public class CalendarController : Controller
    {
        private readonly ITimeTableService _timeTableService;
        private readonly IMapper _mapper;

        public CalendarController(ITimeTableService timeTableService, IMapper mapper)
        {
            _timeTableService = timeTableService;
            _mapper = mapper;
        }

        public ActionResult Backend(Guid id)
        {
            this.Session["TimeTableId"] = id;

            return new Dpc(_timeTableService, _mapper, id).CallBack(this);
        }

        public class Dpc : DayPilotCalendar
        {
            private readonly ITimeTableService _timeTableService;
            private readonly IMapper _mapper;
            private readonly Guid _id;

            public Dpc(ITimeTableService timeTableService, IMapper mapper, Guid id)
            {
                _timeTableService = timeTableService;
                _mapper = mapper;
                _id = id;
            }

            protected override void OnInit(InitArgs initArgs)
            {
                Update(CallBackUpdateType.Full);
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

            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                DataIdField = "Id";
                DataStartField = "Start";
                DataEndField = "End";
                DataTextField = "Text";

                TimeTableDTO timeTableDto = _timeTableService.GetTimeTableDTOById(_id);
                var timeTableViewModel = _mapper.Map<TimeTableDTO, TimeTableViewModel>(timeTableDto);

                Events = timeTableViewModel.Events;
            }


            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                string name = (string)e.Data["name"];
                if (String.IsNullOrEmpty(name))
                {
                    name = "(default)";
                }
                //new EventManager(Controller).EventCreate(e.Start, e.End, name);
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Calendar.EventMoveArgs e)
            {
                var eventDto = _timeTableService.GetEventDTOById(new Guid(e.Id));
                eventDto.Start = e.NewStart;
                eventDto.End = e.NewEnd;
                _timeTableService.UpdateEvent(eventDto);

                Update();
            }

            protected override void OnEventClick(EventClickArgs e)
            {

            }

            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Calendar.EventResizeArgs e)
            {
                var eventDto = _timeTableService.GetEventDTOById(new Guid(e.Id));
                eventDto.Start = e.NewStart;
                eventDto.End = e.NewEnd;
                _timeTableService.UpdateEvent(eventDto);

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
        }

        public ActionResult New(string id)
        {
            var @event = new EventViewModel();
            @event.Start = Convert.ToDateTime(Request.QueryString["start"]);
            @event.End = Convert.ToDateTime(Request.QueryString["end"]);

            return View(@event);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            string text = form["Text"];

            EventViewModel @event = new EventViewModel
            {
                TimeTableId = new Guid(this.Session["TimeTableId"].ToString()),
                Start = start,
                End = end,
                Text = text
            };

            EventDTO eventDto = _mapper.Map<EventViewModel, EventDTO>(@event);

            _timeTableService.AddEvent(eventDto);

            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }

        public ActionResult Edit(string id)
        {
            var eventDto = _timeTableService.GetEventDTOById(new Guid(id));
            var @event = _mapper.Map<EventDTO, EventViewModel>(eventDto);
            return View(@event);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            if (form["DeleteLesson"] == "on")
            {
                _timeTableService.DeleteEvent(new Guid(form["Id"]));
            }
            else
            {
                var eventDto = _timeTableService.GetEventDTOById(new Guid(form["Id"]));
                eventDto.Text = form["Text"];
                _timeTableService.UpdateEvent(eventDto);
            }
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }
    }
}