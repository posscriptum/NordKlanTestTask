using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NordKlan.Models
{
    /// <summary>
    /// Class <c>BookingEvent</c> this is model of table in db.
    /// </summary>
    public class BookingEvent
    {
        public BookingEvent()
        {
        }

        public BookingEvent(DateTime startEvent, DateTime stopEvent)
        {
            StartEvent = startEvent;
            StopEvent = stopEvent;
        }

        public BookingEvent(string name, DateTime startEvent, DateTime stopEvent, string eventAuthor, List<string> participants)
        {
            Name = name;
            StartEvent = startEvent;
            StopEvent = stopEvent;
            EventAuthor = eventAuthor;
            Participants = participants;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartEvent { get; set; }
        public DateTime StopEvent { get; set; }
        public string EventAuthor { get; set; }
        
        public List<string> Participants { get; set; }
    }
}
