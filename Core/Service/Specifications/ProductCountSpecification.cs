using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) :
            base
            (P => (!queryParams.BrandId.HasValue || queryParams.BrandId == P.BrandId)
            &&
            (!queryParams.TypeId.HasValue || queryParams.TypeId == P.TypeId)
            &&
            (string.IsNullOrEmpty(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
            )
        {
            
        }
    }
}
