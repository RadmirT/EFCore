using BookApp.Entities;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;

/// <summary>
/// Содержит связь автора и книг
/// </summary>
public class BookAuthorLazy
{
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public int BookLazyId { get; private set; }
    
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public int AuthorLazyId { get; private  set; }

    /// <summary>
    /// Порядковый номер автора в списке авторов книги
    /// </summary>
    public byte Order { get; set; }

    /// <summary>
    /// Книга
    /// </summary>
    public virtual required BookLazy Book { get; set; }

    /// <summary>
    /// Автор
    /// </summary>
    public virtual required AuthorLazy Author { get; set; }

}
