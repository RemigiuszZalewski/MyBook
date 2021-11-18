using MyBookAPI.Domain.Entities;
using System.Collections.Generic;

namespace MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail
{
    public class AuthorDetailVm
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
