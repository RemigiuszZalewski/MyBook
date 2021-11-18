using System.Collections.Generic;

namespace MyBookAPI.Application.Books.Models
{
    public class BooksVm
    {
        public ICollection<BookDto> Books { get; set; }
    }
}
