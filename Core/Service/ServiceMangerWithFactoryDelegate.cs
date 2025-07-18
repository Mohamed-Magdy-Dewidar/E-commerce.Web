using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;

namespace Service
{
    public class ServiceMangerWithFactoryDelegate(
        Func<IProductService> _productFactory,
        Func<IBasketService> _basketFactory,
        Func<IOrderService> _orderFactory,
        Func<IAuthenticationService> _authFactory,        
        Func<IAttachmentService> _attachmentFactory,        
        Func<IPaymentService> _paymentFactory) : IServiceManager
    {



        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService BasketService => _basketFactory.Invoke();
        public IOrderService OrderService => _orderFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authFactory.Invoke();

        public IPaymentService PaymentService => _paymentFactory.Invoke();

        public IAttachmentService AttachmentService => _attachmentFactory.Invoke();
    }
}
