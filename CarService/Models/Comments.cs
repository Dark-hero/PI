using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Comments
    {
        public int IdComment { get; set; }
        public int IdUser { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }

        public Account IdUserNavigation { get; set; }
    }
}
