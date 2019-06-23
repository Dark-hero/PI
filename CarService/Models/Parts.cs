using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Parts
    {
        public Parts()
        {
            AutoToPart = new HashSet<AutoToPart>();
            OrdersToParts = new HashSet<OrdersToParts>();
        }

        public int Artikul { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfDelivery { get; set; }

        public ICollection<AutoToPart> AutoToPart { get; set; }
        public ICollection<OrdersToParts> OrdersToParts { get; set; }
    }
}
