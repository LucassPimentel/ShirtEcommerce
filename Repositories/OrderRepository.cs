using sh_rt.Context;
using sh_rt.Interfaces;
using sh_rt.Models;

namespace sh_rt.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly Cart _cart;

        public OrderRepository(AppDbContext appDbContext, Cart cart)
        {
            _appDbContext = appDbContext;
            _cart = cart;
        }

        public void CreateOrder(Order order)
        {
            order.CreatedAt = DateTime.Now;
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var cartItems = _cart.CartItems;

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Quantity,
                    ShirtId = item.Shirt.Id,
                    OrderId = order.Id,
                    Price = item.Shirt.Price
                };
                _appDbContext.OrderDetails.Add(orderDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}
