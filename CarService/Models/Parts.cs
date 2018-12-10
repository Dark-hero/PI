using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Parts
    {
        public Parts()
        {
            OrderingServices = new HashSet<OrderingServices>();
        }

        public int Artikul { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime DateOfDelivery { get; set; }

        public ArtikulParts ArtikulNavigation { get; set; }
        public ICollection<OrderingServices> OrderingServices { get; set; }
    }
}
