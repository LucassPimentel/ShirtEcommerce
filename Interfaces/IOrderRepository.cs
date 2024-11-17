using sh_rt.Models;

namespace sh_rt.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
