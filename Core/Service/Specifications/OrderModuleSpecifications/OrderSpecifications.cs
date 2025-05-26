using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;

namespace Service.Specifications.OrderModuleSpecifications
{
    class OrderSpecifications : BaseSpecification<Order ,Guid >
    {

        public OrderSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);

        }

        //Get By Email
        public OrderSpecifications(string email): base(o => o.UserEmail == email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderBy(O => O.OrderDate);           
        }
    }
}
