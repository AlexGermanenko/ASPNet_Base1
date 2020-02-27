using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetBase.Models.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Введите Email")]
        [Remote(action: "CheckLoginEmail", controller: "User", ErrorMessage = "Пользователя с таким Email нет")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Display(Name = "Введите пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
