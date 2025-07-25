﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }

        IBasketService BasketService { get; }

        IAuthenticationService AuthenticationService { get; }

        IOrderService OrderService { get; }

        IPaymentService PaymentService { get; }


        IAttachmentService AttachmentService { get; }




    }
}
