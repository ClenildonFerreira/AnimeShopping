using AnimeShopping.ProductAPI.Data.ValueObjects;
using AnimeShopping.ProductAPI.Model;
using AutoMapper;

namespace AnimeShopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();
            });
            return mappingConfig;
        }
    }
}
