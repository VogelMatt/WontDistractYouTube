using System.Collections.Generic;
using WontDistractYouTube.Models;
using WontDistractYouTube.Models.DTOs;

namespace WontDistractYouTube.Repositories
{
    public interface IUserProfileRepository
    {

        //public List<UserProfile> GetAll();

        //public UserProfile GetById(int id);
        //public UserProfile GetUserProfileByIdWithVideos(int id);
        public UserProfileDto GetUserProfileByFirebaseId(string firebaseUserId);

        //public UserProfileDto GetUserProfileWithVideosTagsTopics(string firebaseUserId);
        void Add(UserProfile userProfile);
        void Delete(int id);
        void Update(UserProfile userProfile);

    }
}