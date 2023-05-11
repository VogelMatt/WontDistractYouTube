using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Claims;
using WontDistractYouTube.Models;
using WontDistractYouTube.Models.DTOs;
using WontDistractYouTube.Repositories;

namespace WontDistractYouTube.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository, IUserProfileRepository userProfileRepository)
        {
            _videoRepository = videoRepository;
            _userProfileRepository = userProfileRepository;
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
        [Authorize]
        [HttpPost]
        public IActionResult Post( Video video )
        {
            var userProfile = GetCurrentUserProfile();

            video.UserProfileId = userProfile.Id;

            _videoRepository.Add(video );
            return CreatedAtAction("Get", new { id = video.Id }, video);
        }

        // https://localhost:5001/api/video/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Video video)
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

        


        private UserProfileDto GetCurrentUserProfile()
        {
           
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetUserProfileByFirebaseId(firebaseUserId);
            

        }
    }
}
    