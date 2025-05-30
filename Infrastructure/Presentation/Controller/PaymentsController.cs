using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDto;

namespace Presentation.Controller
{
    
    public class PaymentsController(IServiceManager serviceManager) : ApiBaseController
    {


        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdate(string basketId)
        {
            var Result = await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(Result);
        }


        
        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var jsonRequest = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();            
            await serviceManager.PaymentService.UpdateOrderPaymentStatusAsync(jsonRequest ,Request.Headers["Stripe-Signature"]);

            return new EmptyResult();        
        }

    }
}
