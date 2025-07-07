namespace BookApp.Entities;

/// <summary>
/// Содержит информацию о предложении скидки на книгу
/// </summary>
public class PriceOffer
{
    /// <summary>
    /// Идентификатор предложения скидки
    /// </summary>
    public int PriceOfferId { get; private set; }
    
    /// <summary>
    /// Новый цена книги
    /// </summary>
    public required decimal NewPrice { get; set; }
    
    /// <summary>
    /// Текст акции
    /// </summary>
    public required string? PromotionalText { get; set; }
    
    /// <summary>
    /// Идентификатор книги, к которой относится данное предложение
    /// </summary>
    public int BookId { get; private set; }
}
