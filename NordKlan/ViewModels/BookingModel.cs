using NordKlan.Models;
using NordKlan.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NordKlan.ViewModels
{
    /// <summary>
    /// Class <c>BookingModel</c> this is model for retrive info from AddNewEvent view.
    /// </summary>
    public class BookingModel
    {
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Not specified start date")]
        public DateTime StartDateEvent { get; set; }
        [Required(ErrorMessage = "Not specified start time")]
        public TimeSpan StartTimeEvent { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Not specified stop date")]
        public DateTime StopDateEvent { get; set; }
        [Required(ErrorMessage = "Not specified stop time")]
        public TimeSpan StopTimeEvent { get; set; }
        [Required(ErrorMessage = "Not specified Event Name")]
        public string Name { get; set; }
        public string Author { get; set; }
        public List<string> Participants { get; set; }

        public DateTime StartDateTime
        {
            get => new DateTime(StartDateEvent.Year,
                             StartDateEvent.Month,
                             StartDateEvent.Day,
                             StartTimeEvent.Hours,
                             StartTimeEvent.Minutes,
                             StartTimeEvent.Seconds);
        }
        public DateTime StopDateTime
        {
            get => new DateTime(StopDateEvent.Year,
                              StopDateEvent.Month,
                              StopDateEvent.Day,
                              StopTimeEvent.Hours,
                              StopTimeEvent.Minutes,
                              StopTimeEvent.Seconds);
        }
        [DurationEventValidation(ErrorMessage = "Range less 30 min or more 24 hours")]
        public TimeSpan Duration { get => StopDateTime - StartDateTime; }
    }
}
