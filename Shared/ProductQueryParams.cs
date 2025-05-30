using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {

        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }


        // changed for intergration with frontend
        //public ProductSortingOptions SortingOptions { get; set; }
        public ProductSortingOptions Sort { get; set; }



        private int pageIndex = 1;
        public int PageIndex
        {
            get => pageIndex;
            set => pageIndex = value < 1 ? 1 : value;
        }



        private int pageSize = DefaultPageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }


    }
}
