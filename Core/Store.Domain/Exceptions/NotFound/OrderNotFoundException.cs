using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions.NotFound {
    public class OrderNotFoundException(Guid intent) : NotFoundException($"Order wiht id: {intent} was not found!") {

    }
}
