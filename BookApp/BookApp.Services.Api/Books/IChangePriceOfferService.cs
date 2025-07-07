namespace BookApp.Services.Api.Books;

using BookApp.Services.Api.Books.Contracts;

public interface IChangePriceOfferService
{
    Task<PriceOfferDto> GetOriginalAsync(int id, CancellationToken cancellationToken);
    Task AddUpdatePriceOffer(PriceOfferDto promotion, CancellationToken cancellationToken);
}