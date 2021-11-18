using MyBookAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Book = MyBookAPI.Domain.Entities.Book;
using GoodreadsBook = Goodreads.Models.Response.Book;

namespace MyBookAPI.Infrastructure.ExternalAPIs.Mappers
{
    class GoodreadsMapper : IGoodreadsMapper
    {
        public Book Map(GoodreadsBook book)
        {
            return new Book
            {
                Name = book.Title,
                Author =
                {
                    AuthorName =
                    {
                        FirstName = "",
                        LastName = ""
                    }
                },
                Pages = book.Pages,
                PublicationDate = book.PublicationDate,
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Stars = book.RatingsCount
                    }
                },
                PublishingHouse =
                {
                    Name = book.Publisher
                },
            };
        }
    }
}
