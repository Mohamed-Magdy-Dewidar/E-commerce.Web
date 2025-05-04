using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int BrandId { get; set; } // this is the foreign key for the product brand

        public ProductType ProductType { get; set; }
        public int TypeId { get; set; } // this is the foreign key for the product type


        // product has one type and one brand while brand and type can have many products
    }
}
