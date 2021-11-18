using MyBookAPI.Domain.Common;
using System.Collections.Generic;

namespace MyBookAPI.Domain.Entities
{
    public class PublishingHouse : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<Book> Books { get; set; }
        public Description Description { get; set; }
    }
}
