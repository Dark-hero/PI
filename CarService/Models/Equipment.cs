using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            TypeOfWorks = new HashSet<TypeOfWorks>();
        }

        public int IdEquipment { get; set; }
        public string Name { get; set; }
        public DateTime? DatуOfManufacture { get; set; }
        public double Cost { get; set; }
        public double CoefficientOfLoading { get; set; }
        public DateTime Warranty { get; set; }

        public ICollection<TypeOfWorks> TypeOfWorks { get; set; }
    }
}
