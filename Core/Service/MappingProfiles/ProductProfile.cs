using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.ProductModuleDto;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() : base()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                //.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"https://localhost:7060/{src.PictureUrl}") );
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>() );


            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();





        }
    }
}
