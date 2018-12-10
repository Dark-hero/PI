using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class OrderingServices
    {
        public int Id { get; set; }
        public int JobCode { get; set; }
        public int IdMaster { get; set; }
        public int Artikul { get; set; }

        public Parts ArtikulNavigation { get; set; }
        public Masters IdMasterNavigation { get; set; }
        public TypeOfWorks JobCodeNavigation { get; set; }
    }
}
