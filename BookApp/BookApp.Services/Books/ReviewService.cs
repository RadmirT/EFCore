using BookApp.Entities;
using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Books;

using BookApp.Common;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

public class ReviewService(AppDbContext context) :  IReviewService
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public GetBlankReviewResponse GetBlankReview(int id)
    {
        var book = _context.Books.AsNoTracking().SingleOrDefault(book => book.BookId == id);
        if (book is null)
        {
            throw new ArgumentException("Book does not exist");
        }

        return new  GetBlankReviewResponse(book.Title, new Review(id));
    }

    public Result<Error> AddReviewToBook(Review review)
    {
        var book = _context.Books
            .Include(r => r.Reviews)
            .SingleOrDefault(k => k.BookId == review.BookId);
        if (book is null)
        {
            throw new ArgumentException("Book does not exist");
        }

        book.Reviews.Add(review);
        _context.SaveChanges();
        
        return Result<Error>.Success();
    }
}
