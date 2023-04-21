using AutoMapper;
using DataAccess.Models;
using eShopSolution.ViewModels.Catalog;
using eShopSolution.ViewModels.Catalog.Products;

namespace eShopSolution.Application.MapperConfig
{
    public class MapProductConfig
    {
        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            //Map Create
            cfg.CreateMap<ProductCreateRequest, Product>();
            cfg.CreateMap<Product, ProductCreateRequest>();


            //Map Update
            cfg.CreateMap<Product, ProductUpdateRequest>();
            cfg.CreateMap<ProductUpdateRequest, Product>();
            cfg.CreateMap <Product, ProductViewModel>();
        }
    }
}
