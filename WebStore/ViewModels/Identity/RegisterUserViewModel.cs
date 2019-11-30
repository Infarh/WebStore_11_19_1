using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;

namespace WebStore.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(256)]
        [Remote(nameof(AccountController.IsNameFree), "Account", ErrorMessage = "Пользователь с таким именем уже существует.")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите ввод пароля")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
