using MyBookAPI.Domain.Entities;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Common.Interfaces
{
    public interface IGoodreads
    {
        Task<Book> GetBookByTitle(string title, string authorName);
    }
}
