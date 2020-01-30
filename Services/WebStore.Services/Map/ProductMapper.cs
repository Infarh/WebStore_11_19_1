using WebStore.Domain.DTO.Products;

namespace WebStore.Services.Map
{
    public static class ProductMapper
    {
        public static ProductDTO ToDTO(this Domain.Entities.Product product) => product is null ? null : new ProductDTO
        {
            Id =  product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Order = product.Order,
            Brand = product.Brand.ToDTO(),
            Section = product.Section.ToDTO()
        };

        public static Domain.Entities.Product FromDTO(this ProductDTO product) => product is null ? null : new Domain.Entities.Product
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Order = product.Order,
            BrandId = product.Brand?.Id,
            Brand = product.Brand.FromDTO(),
            Section = product.Section.FromDTO()
        };
    }
}
