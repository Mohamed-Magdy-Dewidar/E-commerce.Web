using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repository  = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands  = await repository.GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }



        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repository = _unitOfWork.GetRepository<Product, int>();
            var Specification = new ProductWithBrandAndTypeSpecifications(queryParams);
            var products = await Repository.GetAllAsync(Specification);                           
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var TotalCount = await Repository.CountAsync(new ProductCountSpecification(queryParams));
            return new PaginatedResult<ProductDto>(pageSize: productsDto.Count() , pageIndex: queryParams.PageIndex , totalCount: TotalCount , data: productsDto);
        
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return typesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specification = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specification);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var productDto = _mapper.Map<Product, ProductDto>(product);
            return productDto;
        }
    

    }

}
