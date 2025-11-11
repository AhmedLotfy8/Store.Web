using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions.BadRequest;
using Store.Domain.Exceptions.NotFound;
using Store.Services.Abstractions.Orders;
using Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Orders {
    public class OrderService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepository _basketRepository) : IOrderService {

        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail) {

            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);


            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);


            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);


            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items) {

                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                if (product.Price != item.Price)
                    item.Price = product.Price;


                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }


            var subTotal = orderItems.Sum(oi => oi.Price * oi.Quantity);


            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);


            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();


            return _mapper.Map<OrderResponse>(order);
        }

        public Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync() {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResponse>?> GetOrdersForSpecificUserAsync(string userEmail) {
            throw new NotImplementedException();
        }
    }
}
