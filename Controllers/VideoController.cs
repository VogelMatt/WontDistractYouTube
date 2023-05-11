using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        //// https://localhost:5001/api/video/
        //[HttpGet("GetAll")]
        //public IActionResult Get()
        //{
        //    return Ok(_videoRepository.GetAll());
        //}

        [HttpGet]
        public IActionResult GetAllVideos()
        {
            var videos = _videoRepository.GetAllVideos();
            return Ok(videos);
        }

        [HttpGet("Topic/{id}")]
        public IActionResult GetAllVideosByTopicId(int id)
        {
            var videos = _videoRepository.GetAllVideosByTopicId(id);
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
        public IActionResult Post([FromBody] Video video, [FromQuery] int selectedTopicId, [FromQuery] List<int> selectedTagIds)
        {
            _videoRepository.Add(video,selectedTopicId, selectedTagIds);
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
    