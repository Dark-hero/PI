using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class AutoToPart
    {
        public string VinCode { get; set; }
        public int Artikul { get; set; }
        public int Id { get; set; }


        public Parts ArtikulNavigation { get; set; }
        public Auto VinCodeNavigation { get; set; }

    }
}
