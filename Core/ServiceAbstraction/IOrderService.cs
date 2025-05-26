using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.OrderModuleDto;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto order, string Email);


        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    

        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);


        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);

    
    }
}
