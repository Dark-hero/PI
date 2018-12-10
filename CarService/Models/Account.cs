using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models
{
    public partial class Account
    {
        public Account()
        {
            Comments = new HashSet<Comments>();
            Orders = new HashSet<Orders>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Укажите имя пользователя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Неверный формат адреса почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int? IdCard { get; set; }
        public DateTime DateOfRegistration { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль не должен быть менее 6 символов")]
        public string Password { get; set; }
        public bool Verified { get; set; }
        public Guid ActivationCode { get; set; }
        public string ResetPasswordCode { get; set; }

        public BonusCard IdCardNavigation { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}