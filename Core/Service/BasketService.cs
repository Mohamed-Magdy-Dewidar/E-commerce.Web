using Shared.DataTransferObjects.BasketModuleDto;
using DomainLayer.Contracts;
using AutoMapper;
using DomainLayer.Models.Basket;
using DomainLayer.Exceptions;

namespace ServiceAbstraction
{
    
    public class BasketService(IBasketRepository basketRepository , IMapper _mapper) : IBasketService
    {



        public async Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(mappedBasket);          
            if (createdOrUpdatedBasket is not null)
                return  _mapper.Map<CustomerBasket, CustomerBasketDto>(createdOrUpdatedBasket);
            else
                throw new Exception("Could not Create or Update Basket");

        }


        public async Task<CustomerBasketDto> GetBasketAsync(string Key)
        {
            var basket = await basketRepository.GetBasketAsync(Key);
            if (basket  is not null)         
               return _mapper.Map<CustomerBasket , CustomerBasketDto>(basket);
            else
                throw new BasketNotFoundException(Key);

        }



        public async Task<bool> DeleteBasketAsync(string basketId) => await basketRepository.DeleteBasketAysnc(basketId);
    }
}
