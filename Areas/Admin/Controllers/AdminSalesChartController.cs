using Microsoft.AspNetCore.Mvc;
using sh_rt.Areas.Admin.Interfaces;

namespace sh_rt.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSalesChartController : Controller
    {
        private readonly ISalesChartService _salesChartService;

        public AdminSalesChartController(ISalesChartService salesChartService)
        {
            _salesChartService = salesChartService;
        }

        public JsonResult Sales(int days)
        {
            var sales = _salesChartService.GetSales(days);
            return Json(sales);
        }

        [HttpGet]
        public IActionResult AnnualSales()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MonthlySales()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WeeklySales()
        {
            return View();
        }
    }
}
