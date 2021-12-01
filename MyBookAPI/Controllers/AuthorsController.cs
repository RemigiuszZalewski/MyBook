using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Authors.Commands.DeleteAuthor;
using MyBookAPI.Application.Authors.Commands.UpdateAuthor;
using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;
using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<AuthorDetailVm>> GetDetails([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var vm = await Mediator.Send(new GetAuthorDetailQuery() { FirstName = firstName, LastName = lastName });
            return vm;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAuthor([FromQuery] string firstName, [FromQuery] string lastName,
                                                          [FromQuery] string description, [FromQuery] string country)
        {
            var result = await Mediator.Send(new CreateAuthorCommand { FirstName = firstName, LastName = lastName, Description = description, Country = country });
            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateAuthor([FromQuery] string firstName, [FromQuery] string lastName,
                                                          [FromQuery] string description, [FromQuery] string country)
        {
            var result = await Mediator.Send(new UpdateAuthorCommand { FirstName = firstName, LastName = lastName, Description = description, Country = country });
            return result;
        }

        [HttpDelete]
        public async Task<Unit> DeleteAuthor([FromQuery] string firstName, [FromQuery] string lastName)
        { 
            var result = await Mediator.Send(new DeleteAuthorCommand { FirstName = firstName, LastName = lastName });
            return result;
        }
    }
}
