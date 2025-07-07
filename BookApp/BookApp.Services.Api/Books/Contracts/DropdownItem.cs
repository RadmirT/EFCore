namespace BookApp.Services.Api.Books.Contracts;

public record DropdownItem(string Value, string Title)
{
    public DropdownItem (string title)
        :this(title, title)
    {
    }
}
