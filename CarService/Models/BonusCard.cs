using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class BonusCard
    {
        public BonusCard()
        {
            Account = new HashSet<Account>();
        }

        public int IdCard { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double Discount { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}
