using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Books;

/// <summary>
/// Сервис для получения списка книг
/// </summary>
/// <param name="context">контекст доступа к БД.</param>
public class ListBooksService(AppDbContext context)
{
    private readonly AppDbContext context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<BookListItem>> GetBooksListAsync()
    {
        return await context.Books.MapToBookList().ToListAsync();
    }
}