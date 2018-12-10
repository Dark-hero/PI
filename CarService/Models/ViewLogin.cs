using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class ViewLogin
    {
        [Required(ErrorMessage = "Неверный формат адреса почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль не должен быть менее 6 символов")]
        public string Password { get; set; }
    }
}
