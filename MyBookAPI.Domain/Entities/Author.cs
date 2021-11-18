using MyBookAPI.Domain.Common;
using System;
using System.Collections.Generic;

namespace MyBookAPI.Domain.Entities
{
    public class Author : AuditableEntity
    {
        public int Id { get; set; }
        public PersonName AuthorName { get; set; }
        public Description Description { get; set; }
        public string Country { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
