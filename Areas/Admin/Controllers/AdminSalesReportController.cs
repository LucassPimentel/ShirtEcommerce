using Microsoft.AspNetCore.Mvc;
using sh_rt.Areas.Admin.Interfaces;

namespace sh_rt.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSalesReportController : Controller
    {
        private readonly ISalesReportService _salesReportService;

        public AdminSalesReportController(ISalesReportService salesReportService)
        {
            _salesReportService = salesReportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSalesReport(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");


            var result = await _salesReportService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }
    }
}
