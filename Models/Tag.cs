using System.Collections.Generic;

namespace WontDistractYouTube.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Video> Videos { get; set; }
    }
}
