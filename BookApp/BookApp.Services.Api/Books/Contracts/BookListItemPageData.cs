namespace BookApp.Services.Api.Books.Contracts;
public class BookListItemPageData
{
    public required SortFilterPageOptions SortFilterPageOptions { get; init; }
    public required IEnumerable<BookListItem>  BooksList { get; init; }
}
