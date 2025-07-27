namespace BookApp.Features.PlaceOrder;

public record PlaceOrderRequest (bool AcceptTAndCs, Guid UserId, IReadOnlyCollection<OrderLine> LineItems);