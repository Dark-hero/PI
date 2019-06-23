using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrdersToParts = new HashSet<OrdersToParts>();
            OrdersToWorks = new HashSet<OrdersToWorks>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string VinCode { get; set; }
        public int? IdClient { get; set; }
        public int? IdUser { get; set; }
        public DateTime StartDate { get; set; }
        public double OrderCost { get; set; }
        public int IdMaster { get; set; }
        public DateTime EndDate { get; set; }

        public Clients IdClientNavigation { get; set; }
        public Masters IdMasterNavigation { get; set; }
        public Account IdUserNavigation { get; set; }
        public Auto VinCodeNavigation { get; set; }
        public ICollection<OrdersToParts> OrdersToParts { get; set; }
        public ICollection<OrdersToWorks> OrdersToWorks { get; set; }

    }
}
