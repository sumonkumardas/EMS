
using SinePulse.EMS.Messages.Model.Enums;
using System;

namespace SinePulse.EMS.Site.Models
{
    public class EventViewModel : BaseViewModel
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string url { get; set; }
        public string color { get; set; }
    }
}
