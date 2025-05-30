using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;

namespace Service.Specifications.OrderModuleSpecifications
{
    class OrderWithPaymentIntentIdSpecifications : BaseSpecification<Order , Guid>
    {

        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : 
            base(Order => Order.PaymentIntentId == paymentIntentId) 
        {
            
        }
    }
}
