using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace WontDistractYouTube.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public List<Video> Videos { get; set; }

        public string FirebaseUserId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        

    }
}
