using Microsoft.AspNetCore.Mvc;

namespace sh_rt.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
