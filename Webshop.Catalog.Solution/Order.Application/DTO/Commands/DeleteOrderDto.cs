namespace Order.Application.DTO.Commands;

public record DeleteOrderDto
{
    public Guid OrderId { get; set; }
}