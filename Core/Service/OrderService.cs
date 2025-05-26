using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModuleDto;
using Shared.DataTransferObjects.OrderModuleDto;

namespace Service
{
    public class OrderService(IUnitOfWork _unitOfWork , IBasketRepository basketRepository , IMapper _mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto OrderDto, string Email)
        {
            // map Address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(OrderDto.Address);


            // get basket
            var Basket = await basketRepository.GetBasketAsync(OrderDto.BasketId) 
                ?? throw new BasketNotFoundException(OrderDto.BasketId);


            // List of Order Items
            List<OrderItem> orderItems = [];
            // Price  , Quan , ProductId , Name , url
            var ProdcutRepo = _unitOfWork.GetRepository<Product, int>();
            foreach(var item in Basket.Items)
            {
                var product = await ProdcutRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, product));
            }

            // get DeliveryMethod
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(OrderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(OrderDto.DeliveryMethodId);

            decimal SubTotal = orderItems.Sum(I => I.Price * I.Quantity);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, SubTotal);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await _unitOfWork.SaveChanges();


            // i will continue from here tommorow the mapping is not done here 
            return _mapper.Map<Order, OrderToReturnDto>(Order);





        }

        private static OrderItem CreateOrderItem(DomainLayer.Models.Basket.BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered() { ProductId = product.Id, ProductName = product.Name, ProductUrl = product.PictureUrl },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod , int>().GetAllAsync();
            return _mapper.Map<IEnumerable <DeliveryMethod> , IEnumerable <DeliveryMethodDto> >(DeliveryMethods);
        }
        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var Orders =await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id)
        {
            var Spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            if (Order is null)            
                throw new OrderNotFoundException(Id);
            

            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }

    }
}
