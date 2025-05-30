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


    

    public class BasketsController(IServiceManager serviceManager) : ApiBaseController
    {



        [HttpGet]
        [RedisCash]
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


        //[HttpDelete("{id}")] 
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var deleted = await serviceManager.BasketService.DeleteBasketAsync(id);
            //return Ok(deleted);
            return NoContent(); // NoContent is more appropriate for delete operations
        }


    }
}
