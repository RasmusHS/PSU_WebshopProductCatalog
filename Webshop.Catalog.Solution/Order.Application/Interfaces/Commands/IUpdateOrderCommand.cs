using Order.Application.Dto;

namespace Order.Application.Interfaces.Commands;

public interface IUpdateOrderCommand
{
    void Update(UpdateOrderDto dto);
}