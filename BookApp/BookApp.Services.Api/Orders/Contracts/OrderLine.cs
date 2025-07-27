namespace BookApp.Services.Api.Orders.Contracts;

/// <summary>
/// Строка заказа.
/// </summary>
/// <param name="BookId">Идентификатор книги.</param>
/// <param name="Title">Название.</param>
/// <param name="AuthorsName">Авторы.</param>
/// <param name="BookPrice">Цена.</param>
/// <param name="ImageUrl">Ссылка на изображение обложки.</param>
/// <param name="Quantity">Количество.</param>
public record OrderLine(
    int BookId,
    string Title,
    string AuthorsName,
    decimal BookPrice,
    string? ImageUrl,
    ushort Quantity);
