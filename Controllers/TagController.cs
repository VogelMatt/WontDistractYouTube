using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WontDistractYouTube.Repositories;

namespace WontDistractYouTube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        { 
            return Ok(_tagRepository.GetAllTags());
        }
    }
}
