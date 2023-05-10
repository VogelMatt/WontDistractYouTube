using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using WontDistractYouTube.Models;
using WontDistractYouTube.Repositories;

namespace WontDistractYouTube.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IVideoRepository _videoRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository, IVideoRepository videoRepository)
        {
            _userProfileRepository = userProfileRepository;
            _videoRepository = videoRepository;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok();
        //}

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetByFirebaseUserId(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetUserProfileByFirebaseId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetUserProfileByFirebaseId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("WithVideos/{firebaseUserId}")]
        public IActionResult GetByFirebaseUserIdWithVideos(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetUserProfileByFirebaseId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            userProfile.Videos = _videoRepository.GetAllVideosByUserId(firebaseUserId);


            return Ok(userProfile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }       

        [HttpPost]
        public IActionResult Register(UserProfile userProfile)
        {
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(
                nameof(GetByFirebaseUserId), new { firebaseUserId = userProfile.FirebaseUserId }, userProfile);
        }
    }
}









//[HttpGet]
//public IActionResult Get()
//{
//    return Ok(_userProfileRepository.GetUserProfileWithVideosTagsTopics());
//}
//private UserProfile GetCurrentUserProfile()
//{
//    var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
//    return _userProfileRepository.GetUserProfileByFirebaseId(firebaseUserId);
//}

//[HttpGet("{id}/GetUserWithVideos")]
//public IActionResult GetUserByIdWithVideos(int id)
//{
//    var user = _userProfileRepository.GetUserProfileByIdWithVideos(id);
//    if (user == null)
//    {
//        return NotFound();
//    }
//    return Ok(user);
//}

//[HttpGet("GetWithVideos")]
//public IActionResult GetWithVideos()
//{
//    var users = _userProfileRepository.GetUserProfileWithVideosTagsTopics();
//    return Ok(users);
//}
//[HttpPost]
//public IActionResult Post(UserProfile user)
//{
//    _userProfileRepository.Add(user);
//    return CreatedAtAction("Get", new { id = user.Id }, user);
//}

//[Authorize]
//[HttpPut("{id}")]
//public IActionResult Put(int id, UserProfile user)
//{
//    if (id != user.Id)
//    {
//        return BadRequest();
//    }

//    _userProfileRepository.Update(user);
//    return NoContent();
//}

//[Authorize]
//[HttpDelete("{id}")]
//public IActionResult Delete(int id)
//{
//    _userProfileRepository.Delete(id);
//    return NoContent();
//}

//[HttpGet("{firebaseUserId}")]
//public IActionResult GetUserProfile(string firebaseUserId)
//{
//    return Ok(_userProfileRepository.(firebaseUserId));
//}

//[HttpGet("DoesUserExist/{firebaseUserId}")]
//public IActionResult DoesUserExist(string firebaseUserId)
//{
//    var userProfile = _userProfileRepository.GetUserProfleByFirebaseId(firebaseUserId);

//    if (userProfile == null)
//    {
//        return NotFound();
//    }

//    return Ok(userProfile);
//}

//[HttpGet("{id}")]
//public IActionResult Get(int id)
//{
//    var user = _userProfileRepository.GetAllVideosByUserProfileId(id);
//    if (user == null)
//    {
//        return NotFound();
//    }
//    return Ok(user);
//}

//[Authorize]
//[HttpGet("DoesUserExist/{id}")]
//public IActionResult GetByFirebaseId(string id)
//{
//    var user = _userProfileRepository.GetUserProfleByFirebaseId(id);
//    if (user == null)
//    {
//        return NotFound();
//    }
//    return Ok(user);
//}