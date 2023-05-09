using System.ComponentModel.DataAnnotations;

namespace WontDistractYouTube.Models.DTOs
{
    public class UserProfileDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }       
        public string Email { get; set; }       
        public string DisplayName { get; set; }



        #region Nested Classes

        public class TopicDto
        {
            public int Id { get; set; }

            public string Title { get; set; }
        }
        public class TagDto
        {
            public int Id { get; set; }

            public string Name { get; set; }


        }

        #endregion
    }

}
