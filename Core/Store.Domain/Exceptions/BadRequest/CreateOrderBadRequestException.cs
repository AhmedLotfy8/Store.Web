using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions.BadRequest {
    public class CreateOrderBadRequestException() : BadRequestException("Invalid operation when creating order!") {
    
    
    }
}
