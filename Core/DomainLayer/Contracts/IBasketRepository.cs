using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Basket;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {

        Task<bool> DeleteBasketAysnc(string basketId);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket  , TimeSpan? TimeToLive = null);


        Task<CustomerBasket?> GetBasketAsync(string Key);


    }
}
