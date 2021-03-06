using MyBookAPI.Domain.Common;
using System;
using System.Collections.Generic;

namespace MyBookAPI.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool ToBeSold { get; set; }
        public int Pages { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? PublishingHouseId { get; set; }
        public PublishingHouse PublishingHouse { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        public Description Description { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
