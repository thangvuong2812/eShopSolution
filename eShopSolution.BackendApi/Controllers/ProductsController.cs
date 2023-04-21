using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IManageProductService _manageProductService;
        public ProductsController(IManageProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync(string languageId)
        {
            var result = await _manageProductService.GetAll(languageId);

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(long productId, string languageId)
        {
            var result = await _manageProductService.GetProductById(productId, languageId);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            try
            {
                var productId = await _manageProductService.Create(request);
                var productVM = await _manageProductService.GetProductById(productId, request.LanguageId);
                if (productVM != null)
                    return CreatedAtAction(nameof(Create), new { id = productId }, productVM);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Cannot create the new product");
        }

        [HttpPatch]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            try
            {
                var result = await _manageProductService.Update(request);
                if (result > 0)
                    return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Cannot update the product");
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long productId)
        {
            try
            {
                var result = await _manageProductService.Delete(productId);
                if (result > 0)
                    return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Cannot delete the product with id" + productId);
        }
    }
}
