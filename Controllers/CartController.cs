using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sh_rt.Interfaces;
using sh_rt.Models;
using sh_rt.ViewModels;

namespace sh_rt.Controllers
{
    public class CartController : Controller
    {

        private readonly IShirtRepository _shirtRepository;
        private readonly Cart _cart;

        public CartController(IShirtRepository shirtRepository, Cart cart)
        {
            _shirtRepository = shirtRepository;
            _cart = cart;
        }

        public IActionResult Index()
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

        [Authorize]
        public IActionResult AddItem(int shirtId)
        {
            var selectedShirt = _shirtRepository.GetShirtById(shirtId);

            if (selectedShirt is not null)
            {
                _cart.AddToCart(selectedShirt);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoveItem(int shirtId)
        {
            var selectedShirt = _shirtRepository.GetShirtById(shirtId);

            if (selectedShirt is not null)
            {
                _cart.RemoveCartItem(selectedShirt);
            }

            return RedirectToAction("Index");
        }
    }
}
