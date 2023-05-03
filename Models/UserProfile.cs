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
        public Video Video { get; set; }

        public int FirebaseUserId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        

    }
}
