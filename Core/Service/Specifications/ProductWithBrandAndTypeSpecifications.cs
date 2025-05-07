using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {

        // get product by id with brand and type (Only one product)
        public ProductWithBrandAndTypeSpecifications(int id) : base(Product => Product.Id == id)
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);
        }


        // get all products with brand and type (Many products)
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) : 
            base
            ( P => (!queryParams.BrandId.HasValue || queryParams.BrandId == P.BrandId ) 
            && 
            ( !queryParams.TypeId.HasValue || queryParams.TypeId == P.TypeId )
            &&
            (string.IsNullOrEmpty(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
            )
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);


            switch(queryParams.SortingOptions)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(Product => Product.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(Product => Product.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(Product => Product.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(Product => Product.Name);
                    break;
                default:
                    break;
            }


            
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);


        }
        
    }
    
}
