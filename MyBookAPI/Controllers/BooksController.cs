using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Books.Commands.CreateBook;
using MyBookAPI.Application.Books.Commands.DeleteBook;
using MyBookAPI.Application.Books.Commands.UpdateBook;
using MyBookAPI.Application.Books.Models;
using MyBookAPI.Application.Books.Queries.GetBookDetail;
using MyBookAPI.Application.Books.Queries.GetBooksByAuthor;
using MyBookAPI.Application.Books.Queries.GetBooksByCategory;
using MyBookAPI.Application.Books.Queries.GetBooksByCountry;
using System;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : BaseController
    {
        [HttpGet("details")]
        public async Task<ActionResult<BookDetailVm>> GetDetails([FromQuery] string bookName)
        {
            var vm = await Mediator.Send(new GetBookDetailQuery { BookName = bookName });
            return vm;
        }

        [HttpGet("author")]
        public async Task<ActionResult<BooksVm>> GetBooksByAuthor([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var vm = await Mediator.Send(new GetBooksByAuthorQuery { FirstName = firstName, LastName = lastName });
            return vm;
        }

        [HttpGet("category")]
        public async Task<ActionResult<BooksVm>> GetBooksByCategory([FromQuery] string category)
        {
            var vm = await Mediator.Send(new GetBooksByCategoryQuery { Category = category });
            return vm;
        }

        [HttpGet("country")]
        public async Task<ActionResult<BooksVm>> GetBooksByCountry([FromQuery] string country)
        {
            var vm = await Mediator.Send(new GetBooksByCountryQuery { Country = country });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookCommand command)
        {
            var result = Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBook(UpdateBookCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(DeleteBookCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
