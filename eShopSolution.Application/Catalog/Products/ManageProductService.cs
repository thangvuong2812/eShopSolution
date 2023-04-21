using AutoMapper;
using DataAccess.Database;
using DataAccess.Models;
using System.Linq;
using eShopSolution.Application.MapperConfig;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using eShopSolution.Application.Comon;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.ProductTranslations;
using System.Diagnostics;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "AppData";

        public ManageProductService(EShopDbContext dbContext, IStorageService storageService)
        {
            _storageService = storageService;
            _dbContext = dbContext;
            _mapper = new MappingConfig().CreateMapper();

        }
        public async Task<long> Create(ProductCreateRequest productCreateRequest)
        {
            //var product = _mapper.Map<Product>(productCreateRequest);
            var product = new Product
            {
                Price = productCreateRequest.Price,
                OriginalPrice = productCreateRequest.OriginalPrice,
                Stock = productCreateRequest.Stock,

            };
            product.ViewCount = 0;
            product.DateCreated = DateTime.Now;
            product.ProductTranslations = new List<ProductTranslation>
            {
                new ProductTranslation
                {
                    Name = productCreateRequest.Name,
                    Description = productCreateRequest.Description,
                    Details = productCreateRequest.Details,
                    SeoDescription = productCreateRequest.SeoDescription,
                    SeoAlias = productCreateRequest.SeoAlias,
                    SeoTitle = productCreateRequest.SeoTitle,
                    LanguageId = productCreateRequest.LanguageId
                }
            };

            if (productCreateRequest.ThumbNailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage
                    {
                        Caption = "Caption img",
                        FileSize = productCreateRequest.ThumbNailImage.Length,
                        ImgPath = await this.SaveFile(productCreateRequest.ThumbNailImage),
                        IsDefault = true,
                        SortOrder = 1,
                        DateCreated = DateTime.Now,
                        CreatedByUserId = productCreateRequest.CreatedUserId,

                    }
                };
            }
            var productEntity = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            //BUG
            if (productEntity.State == EntityState.Added)
                return product.Id;
            return product.Id;

        }

        public async Task<int> Delete(long productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception($"Cannot find a product has Id: {productId}");

            }
            var images = _dbContext.ProductImages.Where(o => o.ProductId == productId);
            if (images != null && images.Count() > 0)
            {
                await images.ForEachAsync(async o => await _storageService.DeleteFileAsync(o.ImgPath));
            }
            _dbContext.Products.Remove(product);

            return await _dbContext.SaveChangesAsync();

        }


        //Phân trang theo các loại sản phẩm và tên sản phẩm
        public async Task<PagedViewModel<ProductViewModel>> GetAllPaging(PagingRequestProduct pagingRequestProduct)
        {
            var query = from p in _dbContext.Products
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == pagingRequestProduct.LanguageId
                        select new { p, pic, pt };

            if (!string.IsNullOrEmpty(pagingRequestProduct.Keyword))
                query = query.Where(o => o.pt.Name.Contains(pagingRequestProduct.Keyword));

            if (pagingRequestProduct.CategoryIds.Count > 0)
            {
                query = query.Where(o => pagingRequestProduct.CategoryIds.Contains(o.pic.CategoryId));
            }

            int totalRow = await query.CountAsync();

            //offsite paging
            var recordsPerPage = query.Skip((pagingRequestProduct.PageIndex - 1) * pagingRequestProduct.PageSize)
                .Take(pagingRequestProduct.PageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,


                }).ToListAsync();

            var pagedResult = new PagedViewModel<ProductViewModel>
            {
                Items = await recordsPerPage,
                TotalRecord = totalRow,
                PageIndex = pagingRequestProduct.PageIndex,
                PageSize = pagingRequestProduct.PageSize
            };

            return pagedResult;
        }

        public async Task<int> Update(ProductUpdateRequest productUpdateRequest)
        {
            var product = await _dbContext.Products.FindAsync(productUpdateRequest.ProductId);
            var pt = await _dbContext.ProductTranslations.FirstOrDefaultAsync(o => o.ProductId == productUpdateRequest.ProductId);
            if (product == null || pt == null)
                throw new Exception($"Cannot find product with id: {productUpdateRequest.ProductId}");
            pt.SeoAlias = productUpdateRequest.SeoAlias;
            pt.SeoDescription = productUpdateRequest.SeoDescription;
            pt.SeoTitle = productUpdateRequest.SeoTitle;

            pt.Name = productUpdateRequest.Name;
            pt.Description = productUpdateRequest.Description;
            pt.Details = productUpdateRequest.Details;

            if (productUpdateRequest.ThumbNailImage != null)
            {
                var thumbnailImg = await _dbContext.ProductImages.FirstOrDefaultAsync(o => o.IsDefault == true && o.ProductId == productUpdateRequest.ProductId);
                if (thumbnailImg != null)
                {

                    thumbnailImg.ImgPath = await this.SaveFile(productUpdateRequest.ThumbNailImage);
                    thumbnailImg.FileSize = productUpdateRequest.ThumbNailImage.Length;
                    _dbContext.ProductImages.Update(thumbnailImg);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(long productId, decimal newPrice)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find product with id: {productId}");
            product.Price = newPrice;
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateStock(long productId, int newQuantity)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find product with id: {productId}");
            product.Stock = newQuantity;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateViewCount(long productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{DateTime.Now.ToFileTimeUtc()}-{originalFileName}";

            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";

        }

        public async Task<long> AddImage(ProductImageCreateRequest productImageCreateRequest)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(o => o.Id == productImageCreateRequest.ProductId);
            if (product != null)
            {
                string fileName = await this.SaveFile(productImageCreateRequest.ImageFile);
                var newImg = _dbContext.ProductImages.Add(new ProductImage
                {
                    Caption = productImageCreateRequest.Caption,
                    ImgPath = fileName,
                    DateCreated = DateTime.Now,
                    IsDefault = productImageCreateRequest.IsDefault,
                    ProductId = product.Id,
                    SortOrder = productImageCreateRequest.SortOrder,
                    CreatedByUserId = productImageCreateRequest.CreatedUserId,
                    FileSize = productImageCreateRequest.ImageFile.Length
                });
                await _dbContext.SaveChangesAsync();
                if (newImg.State == EntityState.Added)
                    return newImg.Entity.SysId;
            }
            return -1;
        }

        public async Task<int> RemoveImages(List<long> imageIds)
        {
            var images = _dbContext.ProductImages.Where(o => imageIds.Contains(o.SysId));
            if (images != null)
            {
                await images.ForEachAsync(o => _storageService.DeleteFileAsync(o.ImgPath));
                _dbContext.ProductImages.RemoveRange(images);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateImage(ProductImageUpdateRequest productImageUpdateRequest)
        {
            var image = await _dbContext.ProductImages.FirstOrDefaultAsync(o => o.SysId == productImageUpdateRequest.ImageId);
            if (image != null)
            {
                image.Caption = productImageUpdateRequest.Caption;
                image.IsDefault = productImageUpdateRequest.IsDefault;
                image.SortOrder = productImageUpdateRequest.SortOrder;
                var updatedImg = _dbContext.ProductImages.Update(image);
                await _dbContext.SaveChangesAsync();
                if (updatedImg.State == EntityState.Modified)
                    return true;
            }
            return false;
        }

        public async Task<List<ProductImageViewModel>> GetImages(long productId)
        {
            var imagesList = await _dbContext.ProductImages.Where(o => o.ProductId == productId).ToListAsync();
            var imageVMList = _mapper.Map<List<ProductImageViewModel>>(imagesList);
            return imageVMList;
        }

        public async Task<ProductImageViewModel> GetImageById(long imageId)
        {
            var image = await _dbContext.ProductImages.FindAsync(imageId);
            var imgVM = _mapper.Map<ProductImageViewModel>(image);
            return imgVM;
        }

        public async Task<PagedViewModel<ProductViewModel>> GetByCategoryId(PagingPublicRequestProduct pagingPublicRequestProduct)
        {
            var query = from p in _dbContext.Products
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == pagingPublicRequestProduct.LanguageId
                        select new { p, pic, pt };



            if (pagingPublicRequestProduct.CategoryId.HasValue && pagingPublicRequestProduct.CategoryId > 0)
            {
                query = query.Where(o => pagingPublicRequestProduct.CategoryId.Equals(o.pic.CategoryId));
            }

            int totalRow = await query.CountAsync();

            //offsite paging
            var recordsPerPage = query.Skip((pagingPublicRequestProduct.PageIndex - 1) * pagingPublicRequestProduct.PageSize)
                .Take(pagingPublicRequestProduct.PageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,


                }).ToListAsync();

            var pagedResult = new PagedViewModel<ProductViewModel>
            {
                Items = await recordsPerPage,
                TotalRecord = totalRow,
                PageIndex = pagingPublicRequestProduct.PageIndex,
                PageSize = pagingPublicRequestProduct.PageSize
            };

            return pagedResult;
        }

        public async Task<List<ProductViewModel>> GetAll(string languageId)
        {
            var query = from p in _dbContext.Products
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        where pt.LanguageId == languageId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        select new { p, pic, pt };


            //offsite paging
            var recordsPerPage = await query
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,


                }).ToListAsync();



            return recordsPerPage;
        }

        public async Task<int> AddProductTranslation(ProductTranslationCreateRequest productTranslationCreateRequest)
        {
            var productTrans = _mapper.Map<ProductTranslation>(productTranslationCreateRequest);
            var productTransEntity = await _dbContext.ProductTranslations.AddAsync(productTrans);

            productTransEntity.State = EntityState.Added;
            return await _dbContext.SaveChangesAsync();


        }

        public async Task<int> UpdateProductTranslation(ProductTranslationUpdateRequest productTranslationUpdateRequest)
        {
            var productTrans = _mapper.Map<ProductTranslation>(productTranslationUpdateRequest);

            var productEntity = _dbContext.ProductTranslations.Update(productTrans);
            Debug.WriteLine(productEntity.State);
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<ProductViewModel> GetProductById(long productId, string languageId)
        {
            var product = await _dbContext.Products.Where(o => o.Id == productId).FirstOrDefaultAsync();
            var productTranslation = await _dbContext.ProductTranslations.FirstOrDefaultAsync(o => o.ProductId == productId && o.LanguageId == languageId);

            if (product == null)
                return null;
            var productVM = _mapper.Map<ProductViewModel>(product);
            if (productTranslation != null)
            {
                productVM.Description = productTranslation.Description;
                productVM.Name = productTranslation.Name;
                productVM.SeoAlias = productTranslation.SeoAlias;
                productVM.SeoDescription = productTranslation.SeoDescription;
                productVM.SeoTitle = productTranslation.SeoTitle;
                productVM.Details = productTranslation.Details;
                productVM.LanguageId = productTranslation.LanguageId;
            }
            return productVM;

        }

        //public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest categoryAssignRequest)
        //{
        //    var product = await _dbContext.Products.FindAsync(id);
        //    if (product == null)
        //        return new ApiErrorResult<bool>("Cannot found the product");

        //    foreach(var category in categoryAssignRequest.Categories)
        //    {

        //    }
        //}
    }
}
