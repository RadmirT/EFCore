namespace BookApp.Services.Books.Filtering;

using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class NoFilter() : BaseFilter(BooksFilterByOptions.NoFilter)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books, string filterValue)
        => books;

    public override Task<IEnumerable<DropdownItem>> GetDropdownItems(AppDbContext dbContext, CancellationToken cancellationToken = default)
    => Task.FromResult(Enumerable.Empty<DropdownItem>());
}