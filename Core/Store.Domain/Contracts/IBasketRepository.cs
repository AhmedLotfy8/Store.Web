using Store.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts {
    public interface IBasketRepository {


        Task<CustomerBasket?> GetBasketAsync(string id);

        Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration);

        Task<bool> DeleteBasketAsync(string id);



    }
}
