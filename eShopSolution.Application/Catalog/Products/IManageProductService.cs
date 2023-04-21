using eShopSolution.ViewModels.Catalog;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.ProductTranslations;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<long> Create(ProductCreateRequest productCreateRequest);
        Task<int> Update(ProductUpdateRequest productUpdateRequest);
        Task<int> Delete(long productId);
        Task<bool> UpdatePrice(long productId, decimal newPrice);
        Task<bool> UpdateStock(long productId, int newQuantity);
        Task UpdateViewCount(long productId);
        //Task<int> AddImage (long productId,)
        Task<ProductViewModel> GetProductById(long productId, string languageId);
        Task<PagedViewModel<ProductViewModel>> GetAllPaging(PagingRequestProduct pagingRequestProduct);

        Task<long> AddImage(ProductImageCreateRequest productImageCreateRequest);
        Task<int> RemoveImages(List<long> imageIds);
        Task<bool> UpdateImage(ProductImageUpdateRequest productImageUpdateRequest);

        Task<List<ProductImageViewModel>> GetImages(long productId);

        Task<ProductImageViewModel> GetImageById(long imageId);

        Task<PagedViewModel<ProductViewModel>> GetByCategoryId(PagingPublicRequestProduct pagingPublicRequestProduct);

        Task<List<ProductViewModel>> GetAll(string languageId);

        Task<int> AddProductTranslation(ProductTranslationCreateRequest productTranslationCreateRequest);

        Task<int> UpdateProductTranslation(ProductTranslationUpdateRequest productTranslationUpdateRequest);

        //Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest categoryAssignRequest);
    }
}
