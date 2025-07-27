using BookApp.Entities;
using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Books;

public class ReviewService(AppDbContext context)
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public Review GetBlankReview(int id)
    {
        if( _context.Books.Any(p => p.BookId == id) == false)
                throw new ArgumentException("Book does not exist");
        return new Review(id);
    }

    public Book AddReviewToBook(Review review)
    {
        var book = _context.Books
            .Include(r => r.Reviews)
            .Single(k => k.BookId == review.BookId);
        
        book.Reviews.Add(review);
        _context.SaveChanges();
        return book;
    }
}
