namespace BookApp.Services.Books.Extensions;

using BookApp.Entities;
using BookApp.Services.Api.Books.Contracts;

public static class PriceOfferExtensions
{
    public static PriceOfferDto ToPriceOfferDto(this PriceOffer priceOffer) =>
        new PriceOfferDto(priceOffer.BookId, priceOffer.NewPrice, priceOffer.PromotionalText);

}