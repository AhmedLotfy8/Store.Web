using Store.Domain.Entities.Orders;
using Store.Shared.Dtos.Baskets;
using Store.Shared.Dtos.Orders;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Payments {
    public interface IPaymentService {

        Task<BasketDto?> CreatePaymentIntentAsync(string basketId);

        Task<OrderResponse> UpdatePaymentIntentForSucceedOrFailed(string paymentIntentId, bool flag);

    }
}
