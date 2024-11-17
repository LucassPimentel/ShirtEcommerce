using Microsoft.AspNetCore.Mvc;
using sh_rt.Interfaces;
using sh_rt.Models;
using sh_rt.ViewModels;

namespace sh_rt.Controllers
{
    public class ShirtController : Controller
    {
        private readonly IShirtRepository _shirtRepository;

        public ShirtController(IShirtRepository shirtRepository)
        {
            _shirtRepository = shirtRepository;
        }

        public IActionResult GetShirts(string category)
        {
            IEnumerable<Shirt> shirts;
            string currentCategory;

            if (category == null)
            {
                shirts = _shirtRepository.Shirts.OrderBy(c => c.Name);
                currentCategory = "Todos";
            }
            else
            {
                shirts = _shirtRepository.Shirts
                    .Where(s => s.Category.Name.Equals(category))
                    .OrderBy(c => c.Name);

                currentCategory = category;
            }

            var shirtListViewModel = new GetShirtListViewModel
            {
                Shirts = shirts,
                CurrentCategory = currentCategory
            };

            return View(shirtListViewModel);
        }

        public IActionResult Detail(int shirtId)
        {
            var shirt = _shirtRepository.GetShirtById(shirtId);
            return View(shirt);
        }

        public IActionResult Search(string searchedProduct)
        {
            IEnumerable<Shirt> shirts;
            string category = "Produtos Encontrados";

            if (string.IsNullOrEmpty(searchedProduct))
            {
                shirts = _shirtRepository.Shirts.OrderBy(a => a.Name);
            }
            else
            {
                shirts = _shirtRepository.Shirts.Where(s => s.Name.ToLower().Contains(searchedProduct.ToLower()));

                if (!shirts.Any())
                {
                    category = "Nenhum produto encontrado com esse nome :(";
                }
            }

            var getShirtsViewModel = new GetShirtListViewModel
            {
                Shirts = shirts,
                CurrentCategory = category
            };

            
            return View("~/Views/Shirt/GetShirts.cshtml", getShirtsViewModel);
        }
    }
}
