using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Presentation.Attributes;
using Store.Services.Abstractions;
using Store.Shared;
using Store.Shared.Dtos.Products;
using Store.Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation {

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase {


        [HttpGet] // /api/Products
        [Cache(50)]
        public async Task<ActionResult<PaginationRespone<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters) {

            var result = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(result);

        }


        [HttpGet("{id}")] // api/Products/1
        public async Task<ActionResult<ProductResponse>> GetProductById(int? id) {

            var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);
            return Ok(result);

        }


        [HttpGet("brands")] // api/Products/brands
        public async Task<ActionResult<BrandTypeRespone>> GetAllBrands() {

            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(result);

        }


        [HttpGet("types")] // api/Products/types
        public async Task<ActionResult<BrandTypeRespone>> GetAllTypes() {

            var result = await _serviceManager.ProductService.GetAllTypesAsync();


            return Ok(result);

        }


    }
}
