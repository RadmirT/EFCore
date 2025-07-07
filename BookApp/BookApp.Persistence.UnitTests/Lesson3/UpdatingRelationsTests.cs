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
}