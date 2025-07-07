namespace BookApp.Services.Api.Books;

using BookApp.Services.Api.Books.Contracts;

public interface IListBooksService
{
    Task<List<BookListItem>> GetBooksListAsync(SortFilterPageOptions options, CancellationToken cancellationToken = default);
    Task<IEnumerable<DropdownItem>> GetFilterDropDownValues(SortFilterPageOptions options, CancellationToken cancellationToken = default);
}