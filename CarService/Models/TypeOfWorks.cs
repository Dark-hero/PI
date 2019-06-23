using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class TypeOfWorks
    {
        public TypeOfWorks()
        {
            OrdersToWorks = new HashSet<OrdersToWorks>();
        }

        public int JobCode { get; set; }
        public string TypeOfWork { get; set; }
        public double Cost { get; set; }

        public ICollection<OrdersToWorks> OrdersToWorks { get; set; }
    }
}
