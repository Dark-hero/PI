using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Auto
    {
        public Auto()
        {
            ArtikulParts = new HashSet<ArtikulParts>();
            Orders = new HashSet<Orders>();
        }

        public string VinCode { get; set; }
        public string RegisterSign { get; set; }
        public double Mileage { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double EngineСapacity { get; set; }

        public ICollection<ArtikulParts> ArtikulParts { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
