using Microsoft.AspNetCore.Mvc;
using sh_rt.Interfaces;
using sh_rt.Models;
using sh_rt.ViewModels;
using System.Diagnostics;

namespace sh_rt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShirtRepository _shirtRepository;

        public HomeController(IShirtRepository shirtRepository)
        {
            _shirtRepository = shirtRepository;
        }

        public IActionResult Index()
        {
            var favoritesShirts = _shirtRepository.PreferredShirts;

            var homeViewModel = new HomeViewModel
            {
                FavoritesShirts = favoritesShirts
            };

            return View(homeViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}