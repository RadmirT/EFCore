using System.ComponentModel.DataAnnotations;

namespace BookApp.Entities;
public class Tag
{
    [MaxLength(40)]
    public required string TagId { get; set; }
    public virtual ICollection<Book> Books { get; private set; } = new List<Book>();

}
