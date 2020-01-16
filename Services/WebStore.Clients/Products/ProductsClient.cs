using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration config) : base(config, "api/products") { }

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{_ServiceAddress}/sections");

        public IEnumerable<Brand> GetBrands() => Get<List<Brand>>($"{_ServiceAddress}/brands");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => Post(_ServiceAddress, Filter)
           .Content
           .ReadAsAsync<List<ProductDTO>>()
           .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_ServiceAddress}/{id}");
    }
}
