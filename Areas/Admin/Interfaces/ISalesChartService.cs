using sh_rt.Models;

namespace sh_rt.Areas.Admin.Interfaces
{
    public interface ISalesChartService
    {
        List<SalesChart> GetSales(int days = 365);
    }
}