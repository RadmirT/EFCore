using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using BookApp.Services.Books;

namespace BookApp.Controllers;

using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;
using Microsoft.Extensions.Logging.Abstractions;

public class HomeController(ListBooksService listBooksService, ILogger<HomeController> logger)
    : Controller
{
    private readonly ListBooksService listBooksService = listBooksService ?? throw new ArgumentNullException(nameof(listBooksService));
    private readonly ILogger<HomeController> logger = logger ?? NullLogger<HomeController>.Instance;

    public async Task<IActionResult> Index(SortFilterPageOptions options)
    {
        this.logger.LogTrace("Handle {ControllerName} {ActionName} action", nameof(HomeController), nameof(this.Index));
        var books = await listBooksService.GetBooksListAsync(options);
        return this.View(new BookListItemPageData
        {
            SortFilterPageOptions = options,
            BooksList = books
        });
    }

    public async Task<JsonResult> GetFilterSearchContent(SortFilterPageOptions options)
    {
        this.logger.LogTrace("Handle {ControllerName} {ActionName} action", nameof(HomeController), nameof(this.GetFilterSearchContent));
        return this.Json(await listBooksService.GetFilterDropDownValues(options));
    }
    
    public IActionResult Privacy()
    {
        this.logger.LogTrace("Handle {ControllerName} {ActionName} action", nameof(HomeController), nameof(this.Privacy));
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        this.logger.LogTrace("Handle {ControllerName} {ActionName} action", nameof(HomeController), nameof(this.Privacy));
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}