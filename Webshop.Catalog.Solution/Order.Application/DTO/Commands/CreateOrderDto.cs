namespace Order.Application.DTO.Commands;

public record CreateOrderDto
{
    public string CustomerName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
}