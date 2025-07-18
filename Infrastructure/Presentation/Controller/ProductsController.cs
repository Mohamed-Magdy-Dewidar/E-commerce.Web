using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDto;

namespace Presentation.Controller
{


 
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {



        [HttpGet]
        [RedisCash]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            // Validate the query parameters the Client did not send queries that where not register in the query params
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        // Get product by ID
        // baseurl/api/Products/id where id is int
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet(template: "types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        
        [HttpGet(template: "brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }



    
    
    
    }
}
