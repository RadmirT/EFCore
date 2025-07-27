namespace BookApp.Services.Api.Books.Contracts;

using BookApp.Entities;

public record GetBlankReviewResponse(string BookTitle, Review Review);