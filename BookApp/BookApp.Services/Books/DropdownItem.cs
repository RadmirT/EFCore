namespace BookApp.Services.Books;

public record DropdownItem(string Value, string Title)
{
    public DropdownItem (string title)
        :this(title, title)
    {
    }
}
