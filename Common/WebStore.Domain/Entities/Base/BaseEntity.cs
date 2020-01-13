using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>Сущность</summary>
    public abstract class BaseEntity : IBaseEntity
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ограничение уникальности значений в столбце
        public int Id { get; set; }
    }
}
