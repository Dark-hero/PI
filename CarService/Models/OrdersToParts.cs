using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class OrdersToParts
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int Artikul { get; set; }

        public Parts PartsNavigation { get; set; }
        public Orders OrdersNavigation { get; set; }
    }
}
