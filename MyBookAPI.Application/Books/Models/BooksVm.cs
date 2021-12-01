using MyBookAPI.Application.Books.Queries.GetBookDetail;
using System.Collections.Generic;

namespace MyBookAPI.Application.Books.Models
{
    public class BooksVm
    {
        public ICollection<BookDetailVm> Books { get; set; }
    }
}
