using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.MapperConfig
{
    public class MappingConfig
    {
        private readonly MapperConfiguration _config;
        public MappingConfig()
        {
            _config = new MapperConfiguration(cfg =>
            {
                MapProductConfig.CreateMap(cfg);
                MapImageConfig.CreateMap(cfg);
            });
          
        }
        
        public IMapper CreateMapper()
        {
            return _config.CreateMapper();
        }
    }
}
