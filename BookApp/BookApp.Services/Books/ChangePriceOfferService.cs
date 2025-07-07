namespace BookApp.Services.Books;

using BookApp.Entities;
using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;
using BookApp.Services.Books.Extensions;
using Microsoft.EntityFrameworkCore;

public class ChangePriceOfferService(AppDbContext context) : IChangePriceOfferService
{
    private readonly AppDbContext context  =  context ?? throw new NullReferenceException(nameof(context));

    public async Task<PriceOfferDto> GetOriginalAsync(int id, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .AsNoTracking()
            .Include(r => r.Promotion)
            .SingleAsync(k => k.BookId == id, cancellationToken: cancellationToken);

        return (book.Promotion?.ToPriceOfferDto())
               ?? new PriceOfferDto(id, book.Price);
    }

    public async Task AddUpdatePriceOffer(PriceOfferDto promotion, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .Include(r => r.Promotion)
            .SingleAsync(k => k.BookId == promotion.BookId, cancellationToken: cancellationToken);
       
        if (book.Promotion == null)
        {
            book.Promotion = new PriceOffer
            {
                NewPrice = promotion.NewPrice,
                PromotionalText = promotion.PromotionalText
            };
        }
        else
        {
            book.Promotion.NewPrice = promotion.NewPrice;
            book.Promotion.PromotionalText = promotion.PromotionalText;
        }
        await context.SaveChangesAsync(cancellationToken);
    }
}
