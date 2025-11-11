using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Orders {
    public class Order : BaseEntity<Guid> {

        public Order() {
            
        }

        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal) {
        
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        
        }

        public string UserEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public OrderAddress ShippingAddress { get; set; }
        
        public DeliveryMethod DeliveryMethod { get; set; } 

        public int DeliveryMethodId { get; set; } // Foreign key property

        public ICollection<OrderItem> Items { get; set; } // Navigation property

        public decimal Subtotal { get; set; }

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price; // not mapped
        #region Different way
        //[NotMapped]
        //public decimal Total { get; set; }
        #endregion



    }
}
