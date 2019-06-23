using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class AccountPasswordUpdate
    {
        public int IdUser { get; set; }
        public string Password { get; set; }
        public string OldPass { get; set; }


    }
}
