using AutoMapper;
using sh_rt.DTOs;
using sh_rt.Models;

namespace sh_rt.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDTO, Order>();
        }

    }
}
