namespace BookApp.Services.Checkout;

using System.Collections.Generic;
using System.Linq;
using BookApp.Persistence;
using BookApp.Services.Api.Orders.Contracts;
using Microsoft.EntityFrameworkCore;

public class CheckoutService(AppDbContext dbContext)
{
    public IEnumerable<OrderLine> GetCheckout(CookieBasket basket)
    {
        var basketLines = basket.LineItems.ToDictionary(bl => bl.BookId, bl => bl.Quantity);
        var books = basketLines.Keys.ToArray();
        return dbContext.Books.Where(b => books.Contains(b.BookId))
            .Select(book => new
            {
                book.BookId,
                book.Title,
                Authors = string.Join(',', book.AuthorsLink.OrderBy(l => l.Order).Select(a => a.Author.Name)),
                book.Price,
                book.ImageUrl
            }).ToList()
            .Select(book =>
                new OrderLine(
                    book.BookId,
                    book.Title,
                    book.Authors,
                    book.Price,
                    book.ImageUrl,
                    basketLines[book.BookId])
            ).ToList();
    }
}