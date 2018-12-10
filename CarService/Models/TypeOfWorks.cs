using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class TypeOfWorks
    {
        public TypeOfWorks()
        {
            OrderingServices = new HashSet<OrderingServices>();
        }

        public int JobCode { get; set; }
        public string TypeOfWork { get; set; }
        public double Deadline { get; set; }
        public double HourСost { get; set; }
        public int IdEquipment { get; set; }

        public Equipment IdEquipmentNavigation { get; set; }
        public ICollection<OrderingServices> OrderingServices { get; set; }
    }
}
