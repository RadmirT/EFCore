namespace BookApp.Services.Books;
public class BookListItemViewData
{
    public required SortFilterPageOptions SortFilterPageOptions { get; init; }
    public required IEnumerable<BookListItem>  BooksList { get; init; }
}
