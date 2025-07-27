namespace BookApp.Entities;
public class Review
{
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public Review()
    {
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Review"/>  с привязкой к книге.
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <remarks>
    /// Используется для отключенных сценариев работы.
    /// </remarks>
    public Review(int bookId)
    {
        BookId = bookId;
    }
    
    /// <summary>
    /// Идентификатор отзыва
    /// </summary>
    public int ReviewId { get; private set; }
    
    /// <summary>
    /// Имя автора отзыва
    /// </summary>
    public string VoterName { get; set; }
    
    /// <summary>
    /// Оценка книги
    /// </summary>
    public int NumStars { get; set; }
    
    /// <summary>
    /// Комментарий к оценке
    /// </summary>
    public string? Comment { get; set; }
    
    /// <summary>
    /// Идентификатор книги, к которой относится отзыв
    /// </summary>
    public int BookId { get; set; }
}
