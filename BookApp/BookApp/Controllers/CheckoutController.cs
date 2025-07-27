using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers;

using BookApp.Persistence;
using BookApp.Services.Api.Orders.Contracts;
using BookApp.Services.Checkout;
using BookApp.Services.Order;

[Route("Checkout")]
public class CheckoutController(CheckoutService checkoutService, AppDbContext context): Controller
{
    private readonly CheckoutService _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IActionResult Index()
    {
        var basket = new CookieBasket(new BasketCookie(HttpContext.Request.Cookies));
        return View(this._checkoutService.GetCheckout(basket));
    }
    
    /// <summary>
    /// Добавляет новый товар в корзину
    /// </summary>
    /// <param name="bookId">Строка заказа.</param>
    /// <returns></returns>
    [Route("Buy/{bookId}")]
    public IActionResult Buy(int bookId, ushort quantity = 1)
    {
        var cookie = new BasketCookie(HttpContext.Request.Cookies, HttpContext.Response.Cookies);
        var basket = new CookieBasket(cookie);
        basket.Add(bookId, quantity);
        basket.Save();
        return RedirectToAction("Index");
    }

    [Route("PlaceOrder")]
    public IActionResult PlaceOrder(bool acceptTAndCs)
    {
        var cookie = new BasketCookie(HttpContext.Request.Cookies, HttpContext.Response.Cookies);
        var basket = new CookieBasket(cookie);
        var service = new PlaceOrderService( _context,basket);
        var result = service.PlaceOrder(acceptTAndCs);
        return result.Match<IActionResult>(
                orderId => RedirectToAction("ConfirmOrder", "Orders", new {orderId}),
                errors =>
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);

                    }
                    return View( this._checkoutService.GetCheckout(basket));
                });
    }

}