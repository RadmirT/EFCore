namespace BookApp.Features.PlaceOrder;

using System.ComponentModel.DataAnnotations;
using BookApp.Common;
using BookApp.Entities;

public class PlaceOrderAction (IPlaceOrderDbAccess dbAccess): IBusinessAction<PlaceOrderRequest, Order>
{
    private readonly IPlaceOrderDbAccess _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));

    public Result<Order, IEnumerable<Error>> Action(PlaceOrderRequest input)
    {
        if (!input.AcceptTAndCs)
        {
            return Result<Order,IEnumerable<Error>>.Fail([new Error("You must accept the terms and conditions")]);
        }

        if (!input.LineItems.Any())
        {
            return Result<Order, IEnumerable<Error>>.Fail([new Error("You must add at least one item to the cart")]);
        }

        var bookDictionary = _dbAccess.GetBooksWithPriceOffers(input.LineItems.Select(x => x.BookId))
            .ToDictionary(b => b.BookId);

        var order = new Order()
        {
            CustomerId = input.UserId,
            LineItems = new List<OrderLineItem>(),
        };
        
        var errors = new List<Error>();
       byte orderLineNum = 1;
        foreach (var line in input.LineItems)
        {
            if (!bookDictionary.TryGetValue(line.BookId, out var book))
            {
                throw new InvalidOperationException($"Book with id {line.BookId} does not exist");
            }

            var lineItem = new OrderLineItem(orderLineNum++, book, line.Quantity);
            var validationErrors = new List<ValidationResult>();
            Validator.TryValidateObject(lineItem, new ValidationContext(lineItem), validationErrors, true);
           errors.AddRange(validationErrors.Select(x => new Error(x.ErrorMessage ?? "Order line item could not be validated")));
           order.LineItems.Add(lineItem);
        }
        this._dbAccess.AddOrder(order);

        return errors.Any() ? Result<Order, IEnumerable<Error>>.Fail(errors) : Result<Order, IEnumerable<Error>>.Success(order);
    }
}