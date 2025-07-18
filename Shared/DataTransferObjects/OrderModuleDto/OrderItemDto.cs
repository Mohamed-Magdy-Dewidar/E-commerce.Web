﻿namespace Shared.DataTransferObjects.OrderModuleDto
{
    public class OrderItemDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}