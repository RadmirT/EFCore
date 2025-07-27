namespace BookApp.Persistence.UnitTests.Lesson3;

using BookApp.Entities;
using BookApp.Persistence.UnitTests.TestHelper;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

public class UpdatingRelationsDataTests
{
    private AppDbContext context;

    [SetUp]
    public void Setup()
    {
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder => { builder.LogTo(TestContext.WriteLine); });

        context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseFourBooks();
    }

    [TearDown]
    public void Teardown()
    {
        context.Dispose();
    }
    [Test]
    public void ChangePromotion()
    {
        var book = context.Books
            .Include(p => p.Promotion)
            .First(p => p.Promotion == null);

        book.Promotion = new PriceOffer
        {
            NewPrice = book.Price / 2,
            PromotionalText = "Half price today!"
        };
        context.SaveChanges();
    }
    
    [Test]
    public void AddReview()
    {
        var book = context.Books
            .Include(p => p.Reviews)
            .First();
        var reviewCount = book.Reviews.Count;
        book.Reviews.Add(new Review
        {
            VoterName = "Unit Test",
            NumStars = 5,
            Comment = "Great book!"
        });
        
        context.SaveChanges();
        book = context.Books
            .Include(p => p.Reviews)
            .Single(p => p.BookId == book.BookId);
        Assert.That(book.Reviews.Count, Is.EqualTo(reviewCount + 1));
    }
    
    [Test]
    public void ReplaceReview()
    {
        var twoReviewBookId = context.Books.Where(p => p.Reviews.Count >1).Select(book => book.BookId).First();
        
        var book = context.Books
            .Include(p => p.Reviews)
            .Single(p => p.BookId == twoReviewBookId);

        book.Reviews = new List<Review>
        {
            new Review
            {
                VoterName = "Unit Test",
                NumStars = 5,
            }
        };
        context.SaveChanges();
        
        book = context.Books
            .Include(book => book.Reviews)
            .Single(p => p.BookId == twoReviewBookId);
        
        Assert.That(book.Reviews.Count, Is.EqualTo(1));
    }
}