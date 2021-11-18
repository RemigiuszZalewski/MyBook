using Book = MyBookAPI.Domain.Entities.Book;
using GoodreadsBook = Goodreads.Models.Response.Book;

namespace MyBookAPI.Infrastructure.ExternalAPIs.Mappers
{
    public interface IGoodreadsMapper
    {
        Book Map(GoodreadsBook book);
    }
}