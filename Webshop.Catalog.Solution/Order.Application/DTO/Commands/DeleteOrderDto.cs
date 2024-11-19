namespace Order.Application.DTO.Commands;

public record DeleteOrderDto
{
    public int OrderId { get; set; }
}