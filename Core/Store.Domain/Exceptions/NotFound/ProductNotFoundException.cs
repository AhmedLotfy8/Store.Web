using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions.NotFound {
    public class ProductNotFoundException(int id) :
        NotFoundException($"Product wiht id: {id} was not found!") {
 
    
    
        
    
    }
}
