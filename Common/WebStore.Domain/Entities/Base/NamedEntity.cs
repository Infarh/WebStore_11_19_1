using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>Именованная сущность</summary>
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}