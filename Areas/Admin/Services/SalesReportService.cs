using Microsoft.EntityFrameworkCore;
using sh_rt.Areas.Admin.Interfaces;
using sh_rt.Context;
using sh_rt.Models;

namespace sh_rt.Areas.Admin.Services
{
    public class SalesReportService : ISalesReportService
    {
        private readonly AppDbContext _context;

        public SalesReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from order in _context.Orders select order;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.CreatedAt.Value.Date >= minDate.Value.Date);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.CreatedAt.Value.Date <= maxDate.Value.Date);
            }

            return await result
                .Include(i => i.OrderItems)
                .ThenInclude(s => s.Shirt)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
