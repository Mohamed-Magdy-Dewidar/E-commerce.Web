using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    // baseurl/api/Products
    public class ProductsControllers(IServiceManager _serviceManager) : ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
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
