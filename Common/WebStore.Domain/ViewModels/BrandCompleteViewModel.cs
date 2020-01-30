using System.Collections.Generic;

namespace WebStore.Domain.ViewModels
{
    public class BrandCompleteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}
