namespace BookApp.Services.Books.Filtering;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal abstract class BaseFilter(BooksFilterByOptions options)
{
        public BooksFilterByOptions Options => options;
        public abstract IQueryable<BookListItem> Apply(IQueryable<BookListItem> books, string filterValue);
        public abstract Task<IEnumerable<DropdownItem>> GetDropdownItems(AppDbContext dbContext,  CancellationToken cancellationToken = default);
}