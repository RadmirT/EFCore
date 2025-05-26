namespace BookApp.Services.Books.Filtering;

using BookApp.Persistence;

internal abstract class BaseFilter(BooksFilterByOptions options)
{
        public BooksFilterByOptions Options => options;
        public abstract IQueryable<BookListItem> Apply(IQueryable<BookListItem> books, string filterValue);
        public abstract Task<IEnumerable<DropdownItem>> GetDropdownItems(AppDbContext dbContext,  CancellationToken cancellationToken = default);
}