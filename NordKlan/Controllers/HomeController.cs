using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NordKlan.Models;
using NordKlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NordKlan.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _db;
        private int _numberWeek { get; set; }
        public HomeController(Context db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;

            List<List<DateTime>> tableOfDateTimes = new List<List<DateTime>>();
            DateTime startOfWeek = DateTime.Today.AddDays(
               (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
               (int)DateTime.Today.DayOfWeek);
            if(HttpContext.Session.TryGetValue("numberWeek", out byte[] numberWeek))
            {
                _numberWeek = BitConverter.ToInt32(numberWeek, 0);
            }
            else
            {
                HttpContext.Session.Set("numberWeek", BitConverter.GetBytes(0));
            }
            startOfWeek = startOfWeek.AddDays(_numberWeek * 7);
            var dates = Enumerable
                .Range(0, 8)
                .Select(i => startOfWeek
                .AddDays(i))
                .ToList();

            for (int indexOfWeekday = 0; indexOfWeekday <= 7; indexOfWeekday++)
            {
                DateTime dateStart = dates[indexOfWeekday];
                var ColumnOfTable = Enumerable
                            .Range(0, 24)
                            .Select(i => dateStart.AddHours(i))
                            .ToList();
                tableOfDateTimes.Add(ColumnOfTable);
            }

            ViewBag.Table = tableOfDateTimes;

            var events = _db.BookingEvents.Where(p => (p.StartEvent.Date >= dates[0].Date || p.StopEvent.Date >= dates[0].Date) && (p.StartEvent.Date <= dates[6].Date || p.StopEvent.Date <= dates[6].Date));
            ViewBag.Events = events.ToList();

            return View();
        }

        public IActionResult AddNewEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewEvent(BookingModel bookingModel)
        {
            if (ModelState.IsValid)
            {
                //need check intersect with other event
                var bookingEvent = new BookingEvent(bookingModel.StartDateTime, bookingModel.StopDateTime);
                
                if (_db.BookingEvents.Where(p =>    (p.StartEvent <= bookingEvent.StartEvent && p.StopEvent >= bookingEvent.StopEvent) ||
                                                    (p.StartEvent <= bookingEvent.StartEvent && p.StopEvent >= bookingEvent.StartEvent) ||
                                                    (p.StartEvent <= bookingEvent.StopEvent && p.StopEvent >= bookingEvent.StopEvent) ||
                                                    (p.StartEvent >= bookingEvent.StartEvent && p.StopEvent <= bookingEvent.StopEvent)).Any())
                {
                    ModelState.AddModelError("Intersection", "Event have intersection with existing event!");
                    return View();
                }
                _db.BookingEvents.Add(new BookingEvent(bookingModel.Name, bookingModel.StartDateTime, bookingModel.StopDateTime, User.Identity.Name, bookingModel.Participants));
                _db.SaveChanges();
                return Redirect("Index");
            }
            return View();
        }

        public IActionResult Detail(int id)
        {
            return View(_db.BookingEvents.Where(p => p.Id == id).FirstOrDefault());
        }

        public IActionResult PreviewWeek()
        {
            ChangeSessionByKeyNumberWeek(-1);
            return Redirect("Index"); ;
        }

        public IActionResult NextWeek()
        {
            ChangeSessionByKeyNumberWeek(1);
            return Redirect("Index");
        }

        private void ChangeSessionByKeyNumberWeek(int value)
        {
            if (HttpContext.Session.TryGetValue("numberWeek", out byte[] numberWeek))
            {
                _numberWeek = BitConverter.ToInt32(numberWeek, 0) + value;
                HttpContext.Session.Set("numberWeek", BitConverter.GetBytes(_numberWeek));
            }
        }
    }
}
