using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services; 
namespace mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class imagesController : ControllerBase
    { 
        [HttpGet]
        public IActionResult ListIMages()
        { 
            return Ok(ImageService.Instance.GetAll());
        }
        [HttpPost]
        public IActionResult AddImage(IFormCollection data)
        {
            ImageService.Instance.Upload(data.Files[0]);
            return Ok();
        }
    }
}
