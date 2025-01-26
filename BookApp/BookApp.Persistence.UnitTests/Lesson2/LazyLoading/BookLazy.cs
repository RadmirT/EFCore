using BookApp.Entities;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;

public class BookLazy
{
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public int BookLazyId { get; private set; }
    
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
    public virtual PriceOffer? Promotion { get; set; }

    /// <summary>
    /// Отзывы на книги
    /// </summary>
    public virtual ICollection<Review> Reviews { get; private set; } = new List<Review>();

    /// <summary>
    /// Категории (теги)
    /// </summary>
    public virtual ICollection<TagLazy> Tags { get; private set; } = new List<TagLazy>();

    /// <summary>
    /// Авторы
    /// </summary>
    public virtual ICollection<BookAuthorLazy> AuthorsLink { get; private set; } = new List<BookAuthorLazy>();
}