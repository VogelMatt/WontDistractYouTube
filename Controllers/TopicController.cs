using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WontDistractYouTube.Repositories;

namespace WontDistractYouTube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_topicRepository.GetAllTopics());
        }
    }
}
