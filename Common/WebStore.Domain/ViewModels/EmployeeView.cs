using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    /// <summary>Модель-представления сотрудника</summary>
    public class EmployeeView
    {
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя фвляется обязательным", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина имени должна быть в пределах от 2 до 200 символов")]
        [RegularExpression(@"(?:[А-ЯЁ][а-яё]+)|(?:[A-Z][a-z]+)", ErrorMessage = "Странное имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть в пределах от 2 до 200 символов")]
        [Required(ErrorMessage = "Фамилия является обязательной")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }
    }
}
