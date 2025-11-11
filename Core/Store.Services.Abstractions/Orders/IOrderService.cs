using Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Orders {
    public interface IOrderService {

        Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail);

        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();

        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail);

        Task<IEnumerable<OrderResponse>?> GetOrdersForSpecificUserAsync(string userEmail);


    }
}
