using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(string BrandId) => 
            View(new BrandCompleteViewModel
            {
                Brands = GetBrands(), 
                CurrentBrandId = int.TryParse(BrandId, out var id) ? id : (int?)null
            });

        private IEnumerable<BrandViewModel> GetBrands() => _ProductData
           .GetBrands()
           .Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order
            })
           .OrderBy(brand => brand.Order)
           .ToList();

    }
}
