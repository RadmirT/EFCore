namespace BookApp.Services.Api.Books;
using System.ComponentModel.DataAnnotations;

public record GetPubDateResponse(int BookId, string Title, [DataType(DataType.Date)]DateOnly PublishedOn);