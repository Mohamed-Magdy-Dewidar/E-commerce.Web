using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Basket;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Specifications.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDto;
using Stripe;
using Stripe.Forwarding;
using Product =  DomainLayer.Models.ProductModule.Product;
namespace Service
{
    public class PaymentService(IUnitOfWork unitOfWork , IMapper mapper 
        , IBasketRepository basketRepository , 
        IConfiguration configuration
        ) : IPaymentService
    {
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            var basket = await basketRepository.GetBasketAsync(basketId) 
                ?? throw new BasketNotFoundException(basketId);
            
            var ProductRepo = unitOfWork.GetRepository<Product , int>();
            foreach(var item in basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                ArgumentNullException.ThrowIfNull(Product.Price, nameof(Product.Price));
                item.Price = Product.Price; 
            }
            ArgumentNullException.ThrowIfNull(basket.deliveryMethodId, nameof(basket.deliveryMethodId));
            
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Id: basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.deliveryMethodId.Value);

            basket.shippingPrice = deliveryMethod.Cost;
            var BasketTotalAmount =  (long)(basket.Items.Sum(item => item.Price * item.Quantity) + deliveryMethod.Cost) * 100;

            var PaymentService  = new PaymentIntentService();
            if (basket.paymentIntentId == null) //Create
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = BasketTotalAmount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" },
                    Metadata = new Dictionary<string, string>
                    {
                        { "basketId", basket.Id }
                    }
                };
                var paymentIntent = await PaymentService.CreateAsync(options);
                basket.paymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else //Update
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = BasketTotalAmount,
                    Metadata = new Dictionary<string, string>
                    {
                        { "basketId", basket.Id }
                    }
                };
                await PaymentService.UpdateAsync(basket.paymentIntentId, options);
            }


            var updatedBasket = await basketRepository.CreateOrUpdateBasketAsync(basket);

            return mapper.Map<CustomerBasket,CustomerBasketDto>(source:updatedBasket);
        }



        public async Task UpdateOrderPaymentStatusAsync(string jsonRequest, string stripeHeader)
        {
            var endpointSecret = configuration.GetRequiredSection("Stripe")["EndToEndSecretKey"];
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,
                      stripeHeader, endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFailedAsync(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentReceivedAsync(paymentIntent.Id);
                    break;
                // ... handle other event types
                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }


        }


        private async Task UpdatePaymentReceivedAsync(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));

            order.OrderStatus = OrderStatus.PaymentReceived;

            unitOfWork.GetRepository<Order, Guid>().Update(order);

            await unitOfWork.SaveChanges();
        }
        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                   .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));

            order.OrderStatus = OrderStatus.PaymentFailed;

            unitOfWork.GetRepository<Order, Guid>().Update(order);

            await unitOfWork.SaveChanges();
        }

    }
}
