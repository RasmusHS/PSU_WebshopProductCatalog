namespace Order.Domain;

public class OrderEntity
{
    // For Entity Framework only
    internal OrderEntity()
    {
        
    }

    public OrderEntity(string customerName, int quantity, double price, string status)
    {
        OrderId = Guid.NewGuid();
        OrderNumber = Guid.NewGuid().ToString();
        OrderDate = DateTime.Now.ToUniversalTime();
        CustomerName = customerName;
        Quantity = quantity;
        Price = price;
        TotalAmount = Quantity * Price;
        Status = status;
    }
    
    public Guid OrderId { get; private set; } // PK
    public string OrderNumber { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string CustomerName { get; private set; }
    public int Quantity { get; private set; }
    public double Price { get; private set; }
    public double TotalAmount { get; private set; }
    public string Status { get; private set; }

    public void Edit(string customerName, int quantity, double price, string status)
    {
        CustomerName = customerName;
        Quantity = quantity;
        Price = price;
        TotalAmount = Quantity * Price;
        Status = status;
    }
}