using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Identity
{
    public class LoginViewModel
    {     
        [Required]
        [Display(Name = "Имя пользователя")]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}
