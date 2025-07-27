namespace BookApp.Services.Api.Books;


public record ChangePubDateRequest(int BookId, DateOnly PublishedOn);