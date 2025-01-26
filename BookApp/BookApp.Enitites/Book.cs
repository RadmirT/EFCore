namespace BookApp.Entities;

/// <summary>
/// Содержит основную информацию о книге
/// </summary>
public class Book
{
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public int BookId { get; private set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// Описания
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Дата публикации
    /// </summary>
    public required DateOnly PublishedOn { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    public required string Publisher { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Ссылка на изображение обложки
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Промо цена
    /// </summary>
    public PriceOffer? Promotion { get; set; }

    /// <summary>
    /// Отзывы на книги
    /// </summary>
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();

    /// <summary>
    /// Категории (теги)
    /// </summary>
    public ICollection<Tag> Tags { get; private set; } = new List<Tag>();

    /// <summary>
    /// Авторы
    /// </summary>
    public ICollection<BookAuthor> AuthorsLink { get; private set; } = new List<BookAuthor>();
}
