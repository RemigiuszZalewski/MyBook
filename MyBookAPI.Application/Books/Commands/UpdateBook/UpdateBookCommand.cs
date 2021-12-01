using MediatR;
using System;

namespace MyBookAPI.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Pages { get; set; }
        public string Category { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string PublishingHouse { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Description { get; set; }
    }
}
