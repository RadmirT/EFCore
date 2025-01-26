using BookApp.Entities;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;

/// <summary>
/// Автор книги
/// </summary>
public class AuthorLazy
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int AuthorLazyId { get; private set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Книги автора
    /// </summary>
    public virtual ICollection<BookAuthorLazy> BooksLink { get; set; } = new List<BookAuthorLazy>();
}
