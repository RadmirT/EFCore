using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Books;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;
using BookApp.Services.Books.Extensions;
using BookApp.Services.Extensions;

/// <summary>
/// Сервис для получения списка книг
/// </summary>
/// <param name="context">Контекст доступа к БД.</param>
public class ListBooksService(AppDbContext context) : IListBooksService
{
    private readonly AppDbContext context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<BookListItem>> GetBooksListAsync(SortFilterPageOptions options, CancellationToken cancellationToken = default)
    {
        var bookQuery = context.Books
            .AsNoTracking()
            .MapToBookList()
            .ApplySort(options.OrderByOptions)
            .FilterBy(options.FilterBy, options.FilterValue);
        
        options.SetupPagingData(bookQuery);
       
        return await bookQuery.Page(options.PageNum, options.PageSize).ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<IEnumerable<DropdownItem>> GetFilterDropDownValues(SortFilterPageOptions options, CancellationToken cancellationToken = default)
        => this.context.GetDropdownItems(options.FilterBy, cancellationToken);
    
}