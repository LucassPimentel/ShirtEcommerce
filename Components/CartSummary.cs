using Microsoft.AspNetCore.Mvc;
using sh_rt.Models;
using sh_rt.ViewModels;

namespace sh_rt.Components
{
    public class CartSummary : ViewComponent
    {

        private readonly Cart _cart;

        public CartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;

            var cartViewModel = new CartViewModel
            {
                Cart = _cart,
                TotalCartValue = _cart.GetTotalCartValue()
            };

            return View(cartViewModel);
        }
    }
}
