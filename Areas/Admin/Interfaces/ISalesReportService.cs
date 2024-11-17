using sh_rt.Models;

namespace sh_rt.Areas.Admin.Interfaces
{
    public interface ISalesReportService
    {
        Task<List<Order>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
    }
}
