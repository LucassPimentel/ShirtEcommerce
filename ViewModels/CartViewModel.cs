using sh_rt.Models;

namespace sh_rt.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public decimal? TotalCartValue { get; set; }
    }
}
