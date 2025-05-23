﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.ProductModuleDto;


namespace Service.MappingProfiles
{
    class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            var PictureUrl = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
            return PictureUrl;
        }
    }
}
