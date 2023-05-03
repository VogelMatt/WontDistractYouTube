using System.Security.Policy;
using System.Web;

namespace WontDistractYouTube.Models
{
    public class VideoTag
    {
        public string Id { get; set; }

        public int TagId { get; set; }

        public string VideoId { get; set; }

        public Tag tag { get; set; }

        public Video Video { get; set; }   
    }
}
