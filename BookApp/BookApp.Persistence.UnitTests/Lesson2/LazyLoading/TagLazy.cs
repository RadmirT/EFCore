using System.ComponentModel.DataAnnotations;
using BookApp.Entities;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;
public class TagLazy
{
    [MaxLength(40)]
    public required string TagLazyId { get; set; }
    public virtual ICollection<BookLazy> Books { get; private set; } = new List<BookLazy>();

}
