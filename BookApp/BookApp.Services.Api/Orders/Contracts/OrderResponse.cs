namespace BookApp.Services.Api.Orders.Contracts;

public class OrderResponse
{
    public int OrderId { get; set; }

    public DateTime DateOrderedUtc { get; set; }

    public string OrderNumber => $"SO{OrderId:D6}";

    public IEnumerable<OrderLine> LineItems { get; set; }
}