namespace BookApp.Services.Api.Books;

using BookApp.Common;
using BookApp.Entities;
using BookApp.Services.Api.Books.Contracts;

public interface IReviewService
{
    GetBlankReviewResponse GetBlankReview(int id);
    Result<Error> AddReviewToBook(Review review);
}