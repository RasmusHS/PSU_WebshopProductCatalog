namespace Order.Application.Dto;

public record DeleteOrderDto(int orderId)
{
    public int OrderId { get; init; } = orderId;
}