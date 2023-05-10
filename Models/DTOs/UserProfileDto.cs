using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WontDistractYouTube.Models.DTOs
{
    public class UserProfileDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }       
        public string Email { get; set; }       
        public string DisplayName { get; set; }

        public string FirebaseUserId { get; set; }
        public List<VideoDto> Videos { get; set; }      


        #region Nested Classes

        public class TopicDto
        {
            public int Id { get; set; }

            public string Title { get; set; }
        }

        public class VideoDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string Info { get; set; }

            public TopicDto Topic { get; set; }

            public List<TagDto> Tags { get; set; }

        }
        public class TagDto
        {
            public int Id { get; set; }

            public string Name { get; set; }


        }

        #endregion
    }

}
