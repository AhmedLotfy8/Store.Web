using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.Dtos.Orders {
    public class OrderResponse {

        public Guid Id { get; set; }

        public string UserEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddressDto ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } 

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

    }
}
