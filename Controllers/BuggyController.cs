using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    public class BuggyController :BaseApiController 
    {
        [HttpGet("auth")]

        public IActionResult GetAuth()
        {
            return Unauthorized();
        }


        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is server error");
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This is bad request");
        }


    }
}
