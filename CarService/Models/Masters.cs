using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Masters
    {
        public Masters()
        {
            Orders = new HashSet<Orders>();
        }

        public int IdMaster { get; set; }
        public string Specialty { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PassportId { get; set; }
        public DateTime Birthday { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public string NumberFlat { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public bool IsWork { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
