namespace BookApp.Entities;
public class PriceOffer
{
    public int PriceOfferId { get; private set; }
    public decimal NewPrice { get; set; }
    public required string? PromotionalText { get; set; }
    public int BookId { get; private set; }
}
