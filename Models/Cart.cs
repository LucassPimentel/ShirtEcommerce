using Microsoft.EntityFrameworkCore;
using sh_rt.Context;
using System.Net.NetworkInformation;

namespace sh_rt.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        private readonly AppDbContext _context;

        public Cart(AppDbContext context)
        {
            _context = context;
        }

        public static Cart GetCart(IServiceProvider services)
        {
            // define uma sessão
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo do contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera o id do carrinho
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            // atribui o id na sessão
            session.SetString("CartId", cartId);

            // retorna o carrinho com o contexto e o id
            return new Cart(context)
            {
                CartId = cartId,
            };
        }

        public void AddToCart(Shirt shirt)
        {
            var cartItem = HasCartItem(shirt);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = CartId,
                    Shirt = shirt,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public void RemoveCartItem(Shirt shirt)
        {
            var cartItem = HasCartItem(shirt);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems
                .Where(c => c.CartId == CartId)
                .Include(s => s.Shirt)
                .ToList());
        }

        public void ClearCartItems()
        {
            var cartItems = _context.CartItems.Where(c => c.CartId == CartId);

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal? GetTotalCartValue()
        {
            var total = _context.CartItems
                .Where(c => c.CartId == CartId)
                .Select(s => s.Shirt.Price * s.Quantity)
                .Sum();

            return total;
        }

        private CartItem? HasCartItem(Shirt shirt)
        {
            return _context.CartItems.SingleOrDefault(
                s => s.Shirt.Id == shirt.Id
                && s.CartId == CartId);
        }
    }
}
