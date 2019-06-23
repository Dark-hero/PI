using System;
using System.Collections.Generic;

namespace CarService.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Account = new HashSet<Account>();
        }

        public int IdRole { get; set; }
        public string Role { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}
