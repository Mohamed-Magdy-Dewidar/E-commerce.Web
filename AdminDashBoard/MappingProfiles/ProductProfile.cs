using AdminDashBoard.Models;
using AutoMapper;
using DomainLayer.Models.ProductModule;

namespace AdminDashBoard.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, ProductDeleteViewModel>().ReverseMap();
        }
    }
}
