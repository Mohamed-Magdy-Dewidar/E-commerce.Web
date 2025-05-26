using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModuleDto;

namespace Shared.DataTransferObjects.OrderModuleDto
{
    public class OrderToReturnDto
    {
        //And Return Order Details
        //(Id , UserName , OrderDate , Items (Product Name - Picture Url - Price - Quantity)
        //, Address , Delivery Method Name , Order Status Value , Sub Total, Total Price  )

        public Guid Id { get; set; }

        public string UserEmail { get; set; } = default!;

        public AddressDto OrderAddress { get; set; } = default!;

        public string DeliveryMethod { get; set; } = default!;

        public ICollection<OrderItemDto> Items { get; set; } = [];

        public DateTimeOffset OrderDate { get; set; }


        public string OrderStatus { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public   decimal Total { get; set; }



    }
}
