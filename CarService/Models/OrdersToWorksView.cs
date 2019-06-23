using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class OrdersToWorksView
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int[] JobCode { get; set; }

        public TypeOfWorks TypeOfWorksNavigation { get; set; }
        public Orders OrdersNavigation { get; set; }
    }
}
