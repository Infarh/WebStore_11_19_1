using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    /// <summary>Модель-представления заказа</summary>
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Имя является обязательным")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона для связи")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Неизвестно куда везти!")]
        public string Address { get; set; }
    }
}
