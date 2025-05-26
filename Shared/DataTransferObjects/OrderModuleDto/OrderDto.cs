using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModuleDto;

namespace Shared.DataTransferObjects.OrderModuleDto
{
    public class OrderDto
    {
        //Basket Id, Shipping Address , DeliveryMethodId , CustomerEmail
        public string BasketId { get; set; } = default!;
        
        public int DeliveryMethodId { get; set; } = default!;

        public AddressDto Address { get; set; } = default!;

    }
}
