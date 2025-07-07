namespace BookApp.Services.Api.Books.Contracts;

/// <summary>
/// Содержит данные о книге отображаемых в списке книг.
/// </summary>
public class BookListItem
{
    /// <summary>
    /// Идентификатор книги.
    /// </summary>
    public int BookId { get; set; }
    
    /// <summary>
    /// Название книги.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Дата выпуска книги.
    /// </summary>
    public DateOnly PublishedOn { get; set; }
    
    /// <summary>
    /// Стоимость книги.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Актуальная цена книги с учетом скидок.
    /// </summary>
    public decimal ActualPrice { get; set; }

    /// <summary>
    /// Рекламный текст для цены со скидкой.
    /// </summary>
    public string? PromotionPromotionalText { get; set; }

    /// <summary>
    /// Список авторов книги в порядке их следования.
    /// </summary>
    public string AuthorsOrdered { get; set; }

    /// <summary>
    /// Количество отзывов о книги.
    /// </summary>
    public int ReviewsCount { get; set; }

    /// <summary>
    /// Средняя оценка отзывов о книге.
    /// </summary>
    public double? ReviewsAverageVotes { get; set; }

    /// <summary>
    /// Теги книги.
    /// </summary>
    public string[] TagStrings { get; set; }

}