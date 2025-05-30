﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderModuleDto
{
    public class DeliveryMethodDto
    {

        public int Id { get; set; }
        public string ShortName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string DeliveryTime { get; set; } = default!;


        public decimal Cost { get; set; }

    }
}
