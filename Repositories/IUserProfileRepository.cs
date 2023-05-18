using System.Collections.Generic;
using WontDistractYouTube.Models;
using WontDistractYouTube.Models.DTOs;

namespace WontDistractYouTube.Repositories
{
    public interface IUserProfileRepository
    {              
        public UserProfileDto GetUserProfileByFirebaseId(string firebaseUserId);
                
        void Add(UserProfile userProfile);
        void Delete(int id);
        void Update(UserProfile userProfile);
        
    }
}