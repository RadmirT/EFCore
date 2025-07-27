namespace BookApp.Controllers;

using System.Globalization;
using BookApp.Entities;
using BookApp.Extensions;
using BookApp.Models;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    [HttpGet]
    public ActionResult ChangePubDate(int id, [FromServices] IChangePubDateService service)
    {
        Request.ThrowErrorIfNotLocal();
        var dto = service.GetOriginal(id);
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ChangePubDate(GetPubDateResponse dto, [FromServices] IChangePubDateService service)
    {
        Request.ThrowErrorIfNotLocal();
        service.UpdateBook(new ChangePubDateRequest(dto.BookId, dto.PublishedOn));
        return View("BookUpdated", "Successfully changed publication date");
    }

    [HttpGet]
    public async Task<ActionResult> ChangePromotion(int id, [FromServices] IChangePriceOfferService service)
    {
        Request.ThrowErrorIfNotLocal();
        var response = await service.GetOriginalAsync(id);
        ViewData["BookTitle"] = response.Book.Title;
        ViewData["OrgPrice"] = response.Book.Price < 0
            ? "Not currently for sale"
            : response.Book.Price.ToString("c", new CultureInfo("en-US"));
        return View(response.PriceOffer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ChangePromotion([FromForm] PriceOfferDto dto,
        [FromServices] IChangePriceOfferService service, CancellationToken token)
    {
        this.Request.ThrowErrorIfNotLocal();

        return (await service.AddOrUpdatePriceOffer(dto, token))
            .Match(
                () => View("BookUpdated", "Successfully added/changed a promotion"),
                error =>
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    return View(dto);
                });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemovePromotion(PriceOfferDto dto, [FromServices] IChangePriceOfferService service,
        CancellationToken token)
    {
        this.Request.ThrowErrorIfNotLocal();

        return (await service.RemovePriceOffer(dto.BookId, token))
            .Match(
                () => View("BookUpdated", "Successfully removed a promotion"),
                error =>
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    return View(dto);
                });
    }

    [HttpGet]
    public IActionResult AddBookReview(int id, [FromServices] IReviewService service)
    {
        Request.ThrowErrorIfNotLocal();

        var response = service.GetBlankReview(id);
        ViewData["BookTitle"] = response.BookTitle;
        return View(response.Review);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddBookReview(Review dto, [FromServices] IReviewService service)
    {
        Request.ThrowErrorIfNotLocal();

        return service.AddReviewToBook(dto)
            .Match(() => View("BookUpdated", "Successfully added a review"),
                error =>
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    return View(dto);
                });
    }
}