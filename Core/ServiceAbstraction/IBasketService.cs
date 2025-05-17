using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BasketModuleDto;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<bool> DeleteBasketAsync(string basketId);

        Task<CustomerBasketDto> GetBasketAsync(string Key);

        Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto basket);

    }
}
