using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BasketNotFoundException(string key) : Exception($"Basket With Id {key} is not found")
    {

    }
}
