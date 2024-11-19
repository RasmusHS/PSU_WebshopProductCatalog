using Order.Application.DTO.Commands;

namespace Order.Application.Interfaces.Commands;

public interface ICreateOrderCommand
{
    void Create(CreateOrderDto dto);
}