namespace BookApp.Services.Api.Books.Contracts;

public record PriceOfferDto(int BookId, decimal NewPrice, string? PromotionalText = null, bool IsNew = false);
