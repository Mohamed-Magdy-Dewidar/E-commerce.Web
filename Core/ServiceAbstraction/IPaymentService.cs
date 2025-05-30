using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BasketModuleDto;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);

        Task UpdateOrderPaymentStatusAsync(string json, string stripeSignature);

    }
}
