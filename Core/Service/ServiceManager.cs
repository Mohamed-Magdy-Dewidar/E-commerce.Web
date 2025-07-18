﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , IBasketRepository basketRepository , UserManager<ApplicationUser> _userManager , IConfiguration _configuration , IWebHostEnvironment env) 
    {

        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _LazyProductService.Value;


        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager , _configuration , mapper));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;


        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _LazyBasketService.Value;


        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(valueFactory: () => new OrderService(unitOfWork , basketRepository , mapper));
        public IOrderService OrderService => _LazyOrderService.Value; 
        
        private readonly Lazy<IPaymentService> _LazyPaymentService = new Lazy<IPaymentService>(valueFactory: () => new PaymentService(unitOfWork , mapper ,  basketRepository , _configuration));
        public IPaymentService PaymentService => _LazyPaymentService.Value;

        
        


        private readonly Lazy<IAttachmentService> _LazyAttachmentService = new Lazy<IAttachmentService>(valueFactory: () => new FileSystemAttachmentService(env , unitOfWork));
        public IAttachmentService AttachmentService => _LazyAttachmentService.Value;



       
        
    }


}
