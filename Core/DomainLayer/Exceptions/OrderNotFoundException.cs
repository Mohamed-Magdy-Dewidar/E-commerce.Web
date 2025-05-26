using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class OrderNotFoundException(Guid Id) : NotFoundException($"Order with Id {Id} was Not Found") 
    {

    }
}
