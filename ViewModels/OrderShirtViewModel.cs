using sh_rt.Models;
using System.Reflection.Metadata.Ecma335;

namespace sh_rt.ViewModels
{
    public class OrderShirtViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
