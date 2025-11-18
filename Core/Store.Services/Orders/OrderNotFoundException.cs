using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Orders {
    internal class OrderNotFoundException(Guid id) : Exception($"Order with id {id} was not found") {
    }
}
