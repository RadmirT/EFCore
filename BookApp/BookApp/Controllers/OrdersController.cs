using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers;

using BookApp.Persistence;
using BookApp.Services.Order;
[Route("[controller]")]
public class OrdersController(DisplayOrdersService displayOrder) : Controller
{
    DisplayOrdersService _displayOrder = displayOrder ?? throw new ArgumentNullException(nameof(displayOrder));

    // GET
    public IActionResult Index()
    {
        return View(this._displayOrder.GetUsersOrders());
    }
    [HttpGet("ConfirmOrder/{orderId}")]
    public IActionResult ConfirmOrder(int orderId)
    {
        return View(_displayOrder.GetOrderDetail(orderId));
    }
}