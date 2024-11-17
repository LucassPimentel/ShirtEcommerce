using Microsoft.AspNetCore.Mvc;
using sh_rt.Interfaces;

namespace sh_rt.Components
{
    public class MenuCategories : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public MenuCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Categories.OrderBy(c => c.Name);

            return View(categories);
        }
    }
}
