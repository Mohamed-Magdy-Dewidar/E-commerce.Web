using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {


        // For EF if edit EF used paramterless Ctor
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress orderAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string paymentIntentId)
        {
            UserEmail = userEmail;
            OrderAddress = orderAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = default!;

        public OrderAddress OrderAddress { get; set; } = default!;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        
        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal SubTotal { get; set; }


        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus OrderStatus { get; set; }


        //[NotMapped]
        //public decimal Total { get { return SubTotal + DeliveryMethod.Price;  } }
        
          
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } 



    }
}
