using DataAccess.Database;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Catalog.Products.Dtos;
using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _dbContext;
        public PublicProductService(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<PagedViewModel<ProductViewModel>> GetByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
