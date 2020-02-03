using System.Collections.Generic;

namespace WebStore.Domain.Entities
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public List<int> Ids { get; set; }

        public int Page { get; set; }

        public int? PageSize { get; set; }
    }
}