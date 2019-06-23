using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Orders = new HashSet<Orders>();
            Records = new HashSet<Records>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClient { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина фамилии")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина отчества")]
        public string Patronimyc { get; set; }

        [Required(ErrorMessage = "Неверный формат адреса почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        public string Problem { get; set; }

        public DateTime Date { get; set; }
        public bool IsCancel { get; set; }
        public bool IsRecord { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Records> Records { get; set; }
    }
}
