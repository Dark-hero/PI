using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Records
    {
        public int IdRecod { get; set; }
        public int? IdClient { get; set; }
        public int? IdUser { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

        public Clients IdClientNavigation { get; set; }
        public Account IdUserNavigation { get; set; }
    }
}
