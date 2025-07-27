namespace BookApp.Services.Api.Orders.Contracts;

/// <summary>
/// Строка в корзине.
/// </summary>
public class BasketLineRequest
{
    /// <summary>Идентификатор книги.</summary>
    public int BookId { get; init; }

    /// <summary>Количество.</summary>
    public ushort Quantity { get; set; }
}