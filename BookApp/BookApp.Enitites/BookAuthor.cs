namespace BookApp.Entities;

    /// <summary>
    /// Содержит связь автора и книг
    /// </summary>
    public class BookAuthor
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public int BookId { get; private set; }
        
        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public int AuthorId { get; private  set; }

        /// <summary>
        /// Порядковый номер автора в списке авторов книги
        /// </summary>
        public byte Order { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        public required Book Book { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public required Author Author { get; set; }

    }
