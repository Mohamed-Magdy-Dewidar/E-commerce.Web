using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityModuleDto;
using Shared.DataTransferObjects.OrderModuleDto;

namespace Service.MappingProfiles
{
    class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<DeliveryMethod , DeliveryMethodDto>().ReverseMap();


            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(destinationMember => destinationMember.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(destinationMember => destinationMember.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(destinationMember => destinationMember.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(destinationMember => destinationMember.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlReslover>())
                ;





            CreateMap<Order, OrderToReturnDto>()
                     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                     .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                     .ForMember(dest => dest.OrderAddress, opt => opt.MapFrom(src => src.OrderAddress))
                     .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                     .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                     .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                     .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                     .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
                     .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal())); // Assuming GetTotal() calculates total






        }
    }
}
