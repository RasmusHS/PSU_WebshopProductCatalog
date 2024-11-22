namespace Order.Application.DTO.Commands;

public record UpdateOrderDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; }
}