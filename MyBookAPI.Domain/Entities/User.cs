using MyBookAPI.Domain.Common;
using System;

namespace MyBookAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public PersonName UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public Review Review { get; set; }
    }
}
