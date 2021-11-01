using MyBookAPI.Domain.Common;
using System.Collections.Generic;

namespace MyBookAPI.Domain.Entities
{
    public class Address : ValueObject
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{Country}, {City}, {Street}, {ZipCode}";
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return ZipCode;
        }
    }
}
