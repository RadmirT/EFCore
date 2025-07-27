namespace BookApp.Services.Order;

using System;
using System.Collections.Generic;
using System.Linq;
using BookApp.Common;
using BookApp.Entities;
using BookApp.Features.PlaceOrder;
using BookApp.Persistence;
using BookApp.Services.Api.Orders;
using BookApp.Services.Checkout;
using BookApp.Services.Runners;

public class PlaceOrderService 
{
    private readonly IBasket _basket;
    private readonly IRunner<PlaceOrderRequest, Order> _runner;

    public PlaceOrderService(AppDbContext context, IBasket basket)
    {
        this._basket = basket ?? throw new ArgumentNullException(nameof(basket));
        this._runner = new WriteDbRunner<PlaceOrderRequest, Order>(
            context,
            new PlaceOrderAction(new PlaceOrderDbAccess(context)));
    }

    public Result<int, IEnumerable<Error>> PlaceOrder(bool acceptTAndCs)
    {
        var placeOrderResult = _runner.Run(
            new PlaceOrderRequest(acceptTAndCs,
                this._basket.UserId,
                this._basket.LineItems.Select(line => new OrderLine(line.BookId, line.Quantity)).ToArray()));

        return placeOrderResult.Match(
            order =>
            {
                this._basket.Clear();
                this._basket.Save();
                return Result<int, IEnumerable<Error>>.Success(order.OrderId);
            },
            errors => Result<int, IEnumerable<Error>>.Fail(errors.ToArray()));
    }
}
