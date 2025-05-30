﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ProductModuleDto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public decimal price { get; set; }

        public string ProductBrand { get; set; } = null!;

        public string ProductType { get; set; } = null!;



    }
}
