using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO.Products
{
    public class SectionDTO : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}