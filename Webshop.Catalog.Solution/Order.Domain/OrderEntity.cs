using System.ComponentModel.DataAnnotations;

namespace Order.Domain;

public class OrderEntity
{
    // For Entity Framework only
    internal OrderEntity()
    {
        
    }

    public OrderEntity(string customerName, int quantity, double price, string status)
    {
        OrderNumber = Guid.NewGuid().ToString();
        OrderDate = DateTime.Now;
        CustomerName = customerName;
        Quantity = quantity;
        Price = price;
        TotalAmount = Quantity * Price;
        Status = status;
    }
    
    public int OrderId { get; private set; } // PK
    public string OrderNumber { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string CustomerName { get; private set; }
    public int Quantity { get; private set; }
    public double Price { get; private set; }
    public double TotalAmount { get; private set; }
    public string Status { get; private set; }
    [Timestamp]
    public byte[] RowVersion { get; private set; }

    public void Edit(string customerName, int quantity, double price, string status, byte[] rowVersion)
    {
        CustomerName = customerName;
        Quantity = quantity;
        Price = price;
        TotalAmount = Quantity * Price;
        Status = status;
        RowVersion = rowVersion;
    }
}