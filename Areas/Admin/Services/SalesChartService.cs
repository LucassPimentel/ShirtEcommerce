using sh_rt.Areas.Admin.Interfaces;
using sh_rt.Context;
using sh_rt.Models;

namespace sh_rt.Areas.Admin.Services
{
    public class SalesChartService : ISalesChartService
    {
        private readonly AppDbContext _context;

        public SalesChartService(AppDbContext context)
        {
            _context = context;
        }

        public List<SalesChart> GetSales(int days = 365)
        {
            var data = DateTime.Now.AddDays(-days);

            var products = (from od in _context.OrderDetails
                            join s in _context.Shirts on od.ShirtId equals s.Id
                            where od.Order.CreatedAt.Value.Date >= data.Date
                            group od by new { od.ShirtId, s.Name }
                            into g
                            select new SalesChart()
                            {
                                ProductName = g.Key.Name,
                                ProductQuantity = g.Sum(x => x.Quantity),
                                ProductTotalValue = g.Sum(x => x.Price * x.Quantity)
                            });

            return products.ToList();
        }
    }
}
