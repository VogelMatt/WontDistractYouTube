using System.Collections.Generic;
using WontDistractYouTube.Models;
using WontDistractYouTube.Models.DTOs;

namespace WontDistractYouTube.Repositories
{
    public interface IVideoRepository
    {
        void Add(Video video);
        void Delete(int id);
        List<Video> GetAll();
        Video GetByVideoId(int id);
        void Update(Video video);

        public List<VideoDto> GetAllVideosWithTagsAndTopics();
    }
}