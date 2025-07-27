namespace BookApp.Entities;

public class Order
{
    public int OrderId { get; private set; }

    public DateTime DateOrderedUtc { get; private set; }  = DateTime.UtcNow;

    public Guid CustomerId { get; set; }
    
    public ICollection<OrderLineItem> LineItems { get; set; }   
}