using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class ArtikulParts
    {
        public string VinCode { get; set; }
        public int Artikul { get; set; }

        public Auto VinCodeNavigation { get; set; }
        public Parts Parts { get; set; }
    }
}
