using BookApp.Entities;
using BookApp.Persistence.UnitTests.TestHelper;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace BookApp.Persistence.UnitTests.Lesson2;

public class LoadDataTests
{

    [Test]
    public void LoadBooks()
    {
        
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder =>
        {
            builder.LogTo(TestContext.WriteLine);
        });
        
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseFourBooks();
        
        var books = context.Books.AsNoTracking().Where(o => o.Title.StartsWith("Quantum")).ToList();
        Assert.That(books, Has.Count.EqualTo(1));
        var book = books[0];
        Assert.That(book.Reviews, Is.Null.Or.Empty);
        Assert.That(book.AuthorsLink, Is.Null.Or.Empty);
        Assert.That(book.Tags, Is.Null.Or.Empty);
        Assert.That(book.Promotion, Is.Null.Or.Empty);
    }

    [Test]
    public void LoadReviews()
    {
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder => { builder.LogTo(TestContext.WriteLine); });

        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseFourBooks();
        var books = context.Books.AsNoTracking().Select(book =>
            new
            {
                BookId = book.BookId,
                Title = book.Title,
                PublishedOn = book.PublishedOn,
                AuthorsOrdered = string.Join(
                    ", ",
                    book.AuthorsLink
                        .OrderBy(link => link.Order)
                        .Select(link => link.Author.Name)),
                TagStrings = book.Tags.Select(tag => tag.TagId).ToArray(),
                Price = book.Price,
                ActualPrice = book.Promotion == null ? book.Price : book.Promotion.NewPrice,
                PromotionPromotionalText = book.Promotion == null ? null : book.Promotion.PromotionalText,
                ReviewsCount = book.Reviews.Count,
                ReviewsAverageVotes = book.Reviews.Select(review => (double?) review.NumStars).Average(),
            }).ToList();
    }
}