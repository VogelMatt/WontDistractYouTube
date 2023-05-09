using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WontDistractYouTube.Models;
using WontDistractYouTube.Repositories;

namespace WontDistractYouTube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        // https://localhost:5001/api/video/
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_videoRepository.GetAll());
        }

        [HttpGet("VideosWithTagsAndTopics")]
        public IActionResult GetAllVideoTagsAndTopics()
        {
            var videos = _videoRepository.GetAllVideosWithTagsAndTopics();
            return Ok(videos);
        }

        // https://localhost:5001/api/video/{id}
        [HttpGet("{id}")]
        public IActionResult GetByVideoId(int id)
        {
            var video = _videoRepository.GetByVideoId(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        // https://localhost:5001/api/video/
        [HttpPost]
        public IActionResult Post(Video video)
        {
            _videoRepository.Add(video);
            return CreatedAtAction("Get", new { id = video.Id }, video);
        }

        // https://localhost:5001/api/video/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            _videoRepository.Update(video);
            return NoContent();
        }

        // https://localhost:5001/api/video/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _videoRepository.Delete(id);
            return NoContent();
        }
    }
}
    