using System.Collections.Generic;

namespace MyBookAPI.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool ToBeSold { get; set; }
        public int Pages { get; set; }
        public string Category { get; set; }
        public int ReleaseYear { get; set; }
        public int? PublishingHouseId { get; set; }
        public PublishingHouse PublishingHouse { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
