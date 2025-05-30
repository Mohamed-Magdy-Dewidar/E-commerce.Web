using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.BasketModuleDto
{
    public class CustomerBasketDto
    {

        public string Id { get; set; }
        public ICollection<BasketItemDto> Items { get; set; } = [];

        public string? ClientSecret { get; set; }
        public string? paymentIntentId { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal shippingPrice { get; set; }
    }
}
