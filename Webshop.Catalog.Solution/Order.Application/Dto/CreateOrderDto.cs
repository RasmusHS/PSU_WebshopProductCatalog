namespace Order.Application.Dto;

public record CreateOrderDto
{
    public string CustomerName { get; set; } 
    public int Quantity { get; set; } 
    public double Price { get; set; } 
    public string Status { get; set; } 
}