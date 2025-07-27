namespace BookApp.Services.Books;

using System;
using System.Threading;
using System.Threading.Tasks;
using BookApp.Common;
using BookApp.Entities;
using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;
using BookApp.Services.Books.Extensions;
using Microsoft.EntityFrameworkCore;

public class ChangePriceOfferService(AppDbContext context) : IChangePriceOfferService
{
    private readonly AppDbContext context  =  context ?? throw new NullReferenceException(nameof(context));

    public async Task<GetPriceOfferResponse> GetOriginalAsync(int id, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .AsNoTracking()
            .Include(r => r.Promotion)
            .SingleAsync(k => k.BookId == id, cancellationToken: cancellationToken);

        return new GetPriceOfferResponse()
        {
            Book = book,
            PriceOffer = book.Promotion?.ToPriceOfferDto() ?? new PriceOfferDto(id, book.Price, IsNew: true)
        };
    }

    public async Task<Result<Error>> AddOrUpdatePriceOffer(PriceOfferDto promotion, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(promotion.PromotionalText))
        {
            return Result<Error>.Fail(new Error("PromotionalText is required"));
        }
        
        var book = await context.Books
            .Include(r => r.Promotion)
            .SingleOrDefaultAsync(k => k.BookId == promotion.BookId, cancellationToken: cancellationToken);
        if (book == null)
        {
            return Result<Error>.Fail(new Error("Book not found"));
        }
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
        return Result<Error>.Success();
    }

    public async Task<Result<Error>> RemovePriceOffer(int bookId, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .Include(r => r.Promotion)
            .SingleOrDefaultAsync(k => k.BookId == bookId, cancellationToken: cancellationToken);
        if (book == null)
        {
            return Result<Error>.Fail(new Error("Book not found"));
        }
        book.Promotion = null;
        await context.SaveChangesAsync(cancellationToken);
        return Result<Error>.Success();
    }
}
