namespace BookApp.Services.Api.Books.Contracts;

using BookApp.Entities;

public class GetPriceOfferResponse
{
    public PriceOfferDto PriceOffer { get; init; }
    public Book Book { get; init; }
}