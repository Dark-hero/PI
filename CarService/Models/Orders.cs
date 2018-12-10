using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Orders
    {
        public string Id { get; set; }
        public string VinCode { get; set; }
        public int IdClient { get; set; }
        public int IdUser { get; set; }
        public DateTime AdmissionDate { get; set; }
        public double OrderCost { get; set; }

        public Clients IdClientNavigation { get; set; }
        public Account IdUserNavigation { get; set; }
        public Auto VinCodeNavigation { get; set; }
    }
}
