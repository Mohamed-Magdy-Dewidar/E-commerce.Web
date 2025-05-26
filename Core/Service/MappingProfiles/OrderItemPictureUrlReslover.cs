using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.OrderModuleDto;
using Shared.DataTransferObjects.ProductModuleDto;

namespace Service.MappingProfiles
{
    class OrderItemPictureUrlReslover(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.ProductUrl))
                return string.Empty;

            var PictureUrl = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.ProductUrl}";
            return PictureUrl;
        }
    }
}
