using MediatR;
using System;

namespace MyBookAPI.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal? Price => (Price ?? 0) > 0 ? Price : null;
        public int Pages { get; set; }
        public string Category { get; set; }
        public DateTime? PublicationDate => PublicationDate != null ? PublicationDate : null;
        public string PublishingHouse { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
    }
}
