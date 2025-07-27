namespace BookApp.Services.Api.Books;

using BookApp.Common;
using BookApp.Services.Api.Books.Contracts;

public interface IChangePriceOfferService
{
    Task<GetPriceOfferResponse> GetOriginalAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Error>> AddOrUpdatePriceOffer(PriceOfferDto promotion, CancellationToken cancellationToken);
    Task<Result<Error>> RemovePriceOffer(int dtoBookId, CancellationToken token);
}