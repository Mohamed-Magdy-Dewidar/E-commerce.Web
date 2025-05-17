using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.BasketModuleDto
{
    public class BasketItemDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "ProductName must be 2-100 characters")]
        public string ProductName { get; set; } = default!;

        [Required(ErrorMessage = "PictureUrl is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string PictureUrl { get; set; }

        [Range(0.01, double.MaxValue,
            ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }


        [Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
