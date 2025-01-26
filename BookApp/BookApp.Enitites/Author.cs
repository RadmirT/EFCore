namespace BookApp.Entities;

/// <summary>
/// Автор книги
/// </summary>
public class Author
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int AuthorId { get; private set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Книги автора
    /// </summary>
    public ICollection<BookAuthor> BooksLink { get; private set; } = new List<BookAuthor>();
}
