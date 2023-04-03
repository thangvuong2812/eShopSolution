using DataAccess.Database;
using DataAccess.Models;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Catalog.Products.Dtos;
using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _dbContext;
        public ManageProductService(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Create(ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                Price = productCreateRequest.Price
            };

            _dbContext.Products.Add(product);
            return await _dbContext.SaveChangesAsync();

        }

        public Task<int> Delete(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PagedViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductEditRequest productEditRequest)
        {
            throw new NotImplementedException();
        }
    }
}
