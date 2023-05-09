using System.Collections.Generic;
using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public interface IUserProfileRepository
    {

        public List<UserProfile> GetAll();

        public UserProfile GetById(int id);
        public UserProfile GetUserProfileByFirebaseId(string firebaseUserId);
        public UserProfile GetUserProfileByIdWithVideos(int id);

        public List<UserProfile> GetAllUserProfilesWithVideos();
        void Add(UserProfile userProfile);
        void Delete(int id);
        void Update(UserProfile userProfile);

    }
}