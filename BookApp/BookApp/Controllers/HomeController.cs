using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using BookApp.Services.Books;

namespace BookApp.Controllers;

public class HomeController : Controller
{
    private readonly ListBooksService listBooksService;
    private readonly ILogger<HomeController> logger;

    public HomeController( ListBooksService listBooksService, ILogger<HomeController> logger)
    {
        this.listBooksService = listBooksService ?? throw new ArgumentNullException(nameof(listBooksService));
        this.logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var books = await listBooksService.GetBooksListAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}