using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.ViewModels.BreadCrumbs;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BreadCrumbsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        private void GetParameters(out BreadCrumbsType Type, out int id, out BreadCrumbsType FromType)
        {
            Type = Request.Query.ContainsKey("SectionId")
                ? BreadCrumbsType.Section
                : Request.Query.ContainsKey("BrandId")
                    ? BreadCrumbsType.Brand
                    : BreadCrumbsType.None;

            if ((string)ViewContext.RouteData.Values["action"] == nameof(CatalogController.Details))
            {
                Type = BreadCrumbsType.Product;
            }

            id = 0;
            FromType = BreadCrumbsType.Section;

            switch (Type)
            {
                default: throw new ArgumentOutOfRangeException();

                case BreadCrumbsType.None: break;

                case BreadCrumbsType.Section:
                    id = int.Parse(Request.Query["SectionId"].ToString());
                    break;

                case BreadCrumbsType.Brand:
                    id = int.Parse(Request.Query["BrandId"].ToString());
                    break;

                case BreadCrumbsType.Product:
                    id = int.Parse(ViewContext.RouteData.Values["id"].ToString());
                    if (Request.Query.ContainsKey("FromBrand"))
                    {
                        FromType = BreadCrumbsType.Brand;
                    }
                    break;
            }
        }

        public IViewComponentResult Invoke()
        {
            GetParameters(out var Type, out var id, out var FromType);

            switch (Type)
            {
                default: return View(Array.Empty<BreadCrumbViewModel>());

                case BreadCrumbsType.Section:
                    return View(new []
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbsType = BreadCrumbsType.Section,
                            Id = id.ToString(),
                            Name = _ProductData.GetSectionById(id).Name
                        } 
                    });

                case BreadCrumbsType.Brand:
                    return View(new[]
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbsType = BreadCrumbsType.Brand,
                            Id = id.ToString(),
                            Name = _ProductData.GetBrandById(id).Name
                        }
                    });

                case BreadCrumbsType.Product:
                    var product = _ProductData.GetProductById(id);
                    return View(new[]
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbsType = FromType,
                            Id = FromType == BreadCrumbsType.Section
                                ? product.Section.Id.ToString()
                                : product.Brand.Id.ToString(),
                            Name = FromType == BreadCrumbsType.Section
                                ? product.Section.Name
                                : product.Brand.Name
                        }, 
                        new BreadCrumbViewModel
                        {
                            BreadCrumbsType = BreadCrumbsType.Product,
                            Id = product.Id.ToString(),
                            Name = product.Name
                        }, 
                    });
            }
        }
    }
}
