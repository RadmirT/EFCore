namespace BookApp.Services.Books;

using System;
using System.Linq;
using BookApp.Entities;using BookApp.Persistence;
using BookApp.Services.Api.Books;

public class ChangePubDateService(AppDbContext dbContext): IChangePubDateService
{
    private readonly AppDbContext _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public GetPubDateResponse GetOriginal(int id)
    {
        return _context.Books
            .Where(b => b.BookId == id)
            .Select(p => new GetPubDateResponse(p.BookId, p.Title, p.PublishedOn))
            .Single();
    }

    public Book UpdateBook(ChangePubDateRequest request)
    {
        var book = _context.Books.SingleOrDefault(
            x => x.BookId == request.BookId);
        if (book == null)
        {
            throw new ArgumentException("Book not found");
        }

        book.PublishedOn = request.PublishedOn;
        _context.SaveChanges();
        return book;
    }
}