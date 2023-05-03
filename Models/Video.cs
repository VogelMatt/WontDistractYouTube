using System.Collections.Generic;

namespace WontDistractYouTube.Models
{
    public class Video
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public int TopicId { get; set; }

        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public List<Tag> Tag { get; set; }
    }
}
