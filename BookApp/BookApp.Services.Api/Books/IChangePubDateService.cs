namespace BookApp.Services.Api.Books;

using BookApp.Entities;

public interface IChangePubDateService
{
    GetPubDateResponse GetOriginal(int id);
    Book UpdateBook(ChangePubDateRequest request);
}