using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.Basket;
using Shared.DataTransferObjects.BasketModuleDto;

namespace Service.MappingProfiles
{
    class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
