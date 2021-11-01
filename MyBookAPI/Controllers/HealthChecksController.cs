using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/hc")]
    [ApiController]
    public class HealthChecksController : ControllerBase
    {
        /// <summary>
        /// Method that checks if the API is healthy or not.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> GetAsync() => "Healthy";
    }
}
