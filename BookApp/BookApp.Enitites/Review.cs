namespace BookApp.Entities;
public class Review
{
    /// <summary>
    /// Идентификатор отзыва
    /// </summary>
    public int ReviewId { get; private set; }
    
    /// <summary>
    /// Имя автора отзыва
    /// </summary>
    public required string VoterName { get; set; }
    
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
    public int BookId { get; private set; }
}
