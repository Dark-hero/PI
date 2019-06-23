using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class OrdersUpd
    {
        public int Id { get; set; }
        
        public float OrderCost { get; set; }
    }
}
