using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using Service.Services;

namespace mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class authorsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorDTO theAuthor)
        {
            try
            {
                AuthorService.Instance.CreateAuthor(theAuthor);
                return Ok();
            }
            catch(Exception err) 
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{AuthorId:Guid?}")]
        public IActionResult GetAuthors(Guid? AuthorId = null)
        {
            if (AuthorId is null)
            {
                return Ok(AuthorService.Instance.GetAuthors());
            }
            return Ok(AuthorService.Instance.GetAuthor(AuthorId.Value));
        }
       [HttpDelete("{AuthorId:Guid}")]
        public IActionResult DeleteAuthor(Guid AuthorId)
        {
            AuthorService.Instance.DeleteAuthor(AuthorId);
            return Ok();
        }
        [HttpPut("{AuthorId:Guid}")]
        public IActionResult UpdateAuthor([FromBody] UpdateAuthorDTO author)
        {
            AuthorService.Instance.UpdateAuthor(author);
            return Ok();
        }
    }
}
