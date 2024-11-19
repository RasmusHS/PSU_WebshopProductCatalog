namespace Order.Application.Dto;

public record OrderQueryDto
{
    public int OrderId { get; set; } 
    public string OrderNumber { get; set; } 
    public DateTime OrderDate { get; set; } 
    public string CustomerName { get; set; }
    public int Quantity { get; set; } 
    public double Price { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; } 
}