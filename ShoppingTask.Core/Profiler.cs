using AutoMapper;
using ShoppingTask.Core.DTOs;

namespace ShoppingTask.Core;

public class Profiler : Profile
{
    public Profiler()
    {
        CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
    }
}
