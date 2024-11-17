using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sh_rt.DTOs;
using sh_rt.Interfaces;
using sh_rt.Models;

namespace sh_rt.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Cart _cart;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, Cart cart, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cart = cart;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(OrderDTO orderDto)
        {
            int totalItems = 0;
            decimal totalPrice = 0.0m;

            var order = _mapper.Map<OrderDTO, Order>(orderDto);

            var items = _cart.GetCartItems();
            _cart.CartItems = items;

            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio :(");
            }

            // TODO: Refatorar - Incluir como uma prop só leitura no Cart
            foreach (var item in items)
            {
                totalItems += item.Quantity;
                totalPrice += (item.Shirt.Price * item.Quantity);
            }
            order.TotalOrder = totalPrice;
            order.TotalOrderItems = totalItems;


            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);

                ViewBag.CompletedCheckoutMessage = "Pedido finalizado! Agradecemos a preferência :)";
                ViewBag.TotalOrder = _cart.GetTotalCartValue();

                _cart.ClearCartItems();

                return View("~/Views/Order/CompletedCheckout.cshtml", order);
            }

            return View(order);
        }
    }
}
