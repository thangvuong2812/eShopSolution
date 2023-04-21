using AutoMapper;
using DataAccess.Models;
using eShopSolution.ViewModels.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.MapperConfig
{
    public class MapImageConfig
    {
        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            //Map Create
            //cfg.CreateMap<List<ProductImage>, List<ProductImageViewModel>>();
            //cfg.CreateMap<ProductImage, ProductImageViewModel>();

        }
    }
}
