using Microsoft.AspNetCore.Http.Extensions;
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
        /// <summary>
        /// Method that retreives information about a book passed in the request.
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        [HttpGet("details")]
        public async Task<ActionResult<BookDetailVm>> GetDetails([FromQuery] string bookName)
        {
            var vm = await Mediator.Send(new GetBookDetailQuery { BookName = bookName });
            return Ok(vm);
        }

        /// <summary>
        /// Method that returns a list of books that were written by specified author.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet("author")]
        public async Task<ActionResult<BooksVm>> GetBooksByAuthor([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var vm = await Mediator.Send(new GetBooksByAuthorQuery { FirstName = firstName, LastName = lastName });
            return Ok(vm);
        }

        /// <summary>
        /// Method that returns a list of books from specified category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("category")]
        public async Task<ActionResult<BooksVm>> GetBooksByCategory([FromQuery] string category)
        {
            var vm = await Mediator.Send(new GetBooksByCategoryQuery { Category = category });
            return Ok(vm);
        }

        /// <summary>
        /// Method that returns a list of books from specified country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpGet("country")]
        public async Task<ActionResult<BooksVm>> GetBooksByCountry([FromQuery] string country)
        {
            var vm = await Mediator.Send(new GetBooksByCountryQuery { Country = country });
            return Ok(vm);
        }

        /// <summary>
        /// Method that creates a new book.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookCommand command)
        {
            var result = await Mediator.Send(command);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + result), command);
        }

        /// <summary>
        /// Method that updates selected information about specified book.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateBook(UpdateBookCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Method that deletes a book.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(DeleteBookCommand command)
        {
            var result = await Mediator.Send(command);
            return NoContent();
        }
    }
}
