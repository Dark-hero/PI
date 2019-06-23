using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Auto
    {
        public Auto()
        {
            AutoToPart = new HashSet<AutoToPart>();
            Orders = new HashSet<Orders>();
        }

        public string VinCode { get; set; }
        public string RegisterSign { get; set; }
        public double Mileage { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double EngineСapacity { get; set; }

        public ICollection<AutoToPart> AutoToPart { get; set; }
        public ICollection<Orders> Orders { get; set; }

        
    }
}
