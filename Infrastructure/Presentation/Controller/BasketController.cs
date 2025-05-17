using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDto;

namespace Presentation.Controller
{


    [ApiController]
    [Route("api/[controller]")]
    // baseurl/api/Basket
    
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string key)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var createdOrUpdatedBasket = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(createdOrUpdatedBasket);
        }


        [HttpDelete("{key}")] 
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            var deleted = await serviceManager.BasketService.DeleteBasketAsync(basketId);
            return Ok(deleted);
        }


    }
}
