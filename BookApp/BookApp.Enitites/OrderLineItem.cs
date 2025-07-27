namespace BookApp.Entities;

using System.ComponentModel.DataAnnotations;

public class OrderLineItem: IValidatableObject
{
    public int OrderLineItemId { get; private set; }
    [Range(1, 5, ErrorMessage = "This order is over the limit of 5 books.")]
    public byte LineNum { get; private set; }
    public ushort NumBooks { get; private set; }
    public decimal BookPrice { get; private set; }
    public int OrderId { get; private set; }
    public Book ChosenBook { get; private set; }

    public OrderLineItem(byte lineNum, Book book, ushort numBooks)
    {
        LineNum = lineNum;
        ChosenBook = book;
        NumBooks = numBooks;
        BookPrice = book.Price;
    }
    
    private OrderLineItem()
    {
    }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ChosenBook.Price < 0)
        {
                yield return new ValidationResult($"Sorry, the book '{ChosenBook.Title}' is not for sale.");
        }

        if (NumBooks > 100)
        {
            yield return new ValidationResult("If you want to order a 100 or more books please call us.");
        }
       
    }
}