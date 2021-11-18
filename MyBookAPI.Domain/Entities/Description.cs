using MyBookAPI.Domain.Common;
using System.Collections.Generic;

namespace MyBookAPI.Domain.Entities
{
    public class Description : ValueObject
    {
        public string Text { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}
