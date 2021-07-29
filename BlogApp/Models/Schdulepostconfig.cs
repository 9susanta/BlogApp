using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Schdulepostconfig
    {
        public long Id { get; set; }
        public long? PostId { get; set; }
        public DateTime? PostedOn { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public int? TimeDelay { get; set; }
    }
}
