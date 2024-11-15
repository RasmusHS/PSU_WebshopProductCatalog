using Order.Application.Dto;

namespace Order.Application.Interfaces.Commands;

public interface ICreateOrderCommand
{
    void Create(CreateOrderDto dto);
}