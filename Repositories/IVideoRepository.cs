using System.Collections.Generic;
using WontDistractYouTube.Models;
using WontDistractYouTube.Models.DTOs;

namespace WontDistractYouTube.Repositories
{
    public interface IVideoRepository
    {

        public void Add(Video video);
        
        void Delete(int id);
      
        EditVideoDto GetByVideoId(int id);
        void Update(Video video);

        public List<VideoDto> GetAllVideosByTopicId(int id);

        public List<VideoDto> GetAllVideos();

        public List<UserProfileDto.VideoDto> GetAllVideosByUserId(string firebaseUserId);
    }
}