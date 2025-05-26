using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.ProductModuleDto;
using Shared;
using Shared.DataTransferObjects.OrderModuleDto;
using System.Net.Mail;

namespace Presentation.Controller
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var products = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(products);
        }


        [AllowAnonymous]
        [HttpGet("DelvieryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethod()
        {
            var DeliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }
        
   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(Email: GetEmailFromToken());
            return Ok(Orders);
        }
        
       
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
