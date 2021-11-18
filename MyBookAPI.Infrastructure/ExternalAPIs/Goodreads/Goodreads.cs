using Goodreads;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Entities;
using MyBookAPI.Infrastructure.ExternalAPIs.Mappers;
using System.Threading.Tasks;

namespace MyBookAPI.Infrastructure.ExternalAPIs.Goodreads
{
    public class Goodreads : IGoodreads
    {
        private readonly IGoodreadsMapper _mapper;
        public Goodreads(IGoodreadsMapper mapper)
        {
            _mapper = mapper;
        }

        const string apiKey = "23ADS-123DJ-SJ661-5SA5H";
        const string apiSecret = "23ADS-123Secret";
        public async Task<Book> GetBookByTitle(string title, string authorName)
        {
            var client = GoodreadsClient.Create(apiKey, apiSecret);
            var book = await client.Books.GetByTitle(title, authorName);
            var mappedResult = _mapper.Map(book);

            return mappedResult;
        }
    }
}
