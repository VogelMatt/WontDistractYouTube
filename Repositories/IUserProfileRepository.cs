using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        void Delete(int id);
        UserProfile GetAllVideosByUserProfileId(int id);
        void Update(UserProfile userProfile);

        public UserProfile GetUserProfleByFirebaseId(string id);
    }
}