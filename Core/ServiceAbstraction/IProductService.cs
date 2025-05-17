using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects.ProductModuleDto;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        
        
    }
}
