using BookApp.Persistence.UnitTests.TestHelper;
using NUnit.Framework.Interfaces;
using TestSupport.EfHelpers;

namespace BookApp.Persistence.UnitTests.Lesson2;

public class ClientSideEvaluationTests
{
    private AppDbContext context;
    [SetUp]
    public void Setup()
    {
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder =>
        {
            builder.LogTo(TestContext.WriteLine);
        });
        
        context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseDummyBooks();
    }
    [TearDown]
    public void Teardown()
    {
        context.Dispose();
    }

    [Test]
    public void LoadBook()
    {
        var book = context.
            Books
            .Where(book => book.AuthorsLink.Count > 1)
            .Select(book =>
        new
        {
            book.BookId,
            book.Title,
            Autors = string.Join(", ", book.AuthorsLink.OrderBy(link => link.Order).Select(link => link.Author.Name)),
        }).First();
        TestContext.Out.WriteLine($"book Id: {book.BookId}, Title: {book.Title}, Authors: {book.Autors}");
    }

}