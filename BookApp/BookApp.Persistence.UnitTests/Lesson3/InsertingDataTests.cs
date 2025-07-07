namespace BookApp.Persistence.UnitTests.Lesson3;

using BookApp.Entities;
using BookApp.Persistence.UnitTests.TestHelper;
using TestSupport.EfHelpers;

public class InsertingDataTests
{
    private AppDbContext context;

    [SetUp]
    public void Setup()
    {
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder => { builder.LogTo(TestContext.WriteLine); });

        context = new AppDbContext(options);
        context.Database.EnsureCreated();
    }

    [TearDown]
    public void Teardown()
    {
        context.Dispose();
    }

    [Test]
    public void AddAuthor()
    {
        var author = new Author
        {
            Name = "Лев Толстой"
        };

        this.context.Authors.Add(author);
        this.context.SaveChanges();
    }

    [Test]
    public void AddBookWithRewiew()
    {
        var book = new Book
        {
            Title = "Война и мир",
            Description = """
                          Роман-эпопея Льва Николаевича Толстого, рассказывающий о сложном,
                          бурном периоде в истории России и всей Европы — эпохе завоевательных
                          походов императора Наполеона в Восточную Европу и Россию, с 1805 по 1812 год.
                          """,
            PublishedOn = new DateOnly(1868, 12, 16),
            Publisher = "Русский вестник",
            Price = 2_000_000,
            Reviews =
            {
                new Review
                {
                    VoterName = "В.Г. Белинский",
                    NumStars = 5,
                    Comment = """
                              Патриотизм не в пышных фразах, а в честном выполнении долга,
                               военного и человеческого, несмотря ни на что
                              """
                }
            }
        };
        this.context.Books.Add(book);
        this.context.SaveChanges();


    }

    [Test]
    public void AddBookWithExistingAuthor()
    {
        // Arange
        context.Authors.Add(new Author { Name = "Фёдор Достоевский" });
        this.context.SaveChanges();
        
        // Act
        var existingAuthor = context.Authors.FirstOrDefault(a => a.Name == "Фёдор Достоевский");
        if (existingAuthor == null)
        {
            throw new InvalidOperationException("Автор не найден");
        }

        var newBook = new Book
        {
            Title = "Преступление и наказание",
            PublishedOn = DateOnly.FromDateTime(DateTime.Now),
            Publisher = "Издательство Раскольников и Kо",
            Price = 317.60m,
        };

        newBook.AuthorsLink.Add(new BookAuthor { Book = newBook, Author = existingAuthor });
        this.context.Books.Add(newBook);

        context.SaveChangesAsync();
    }
}