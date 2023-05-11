using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WontDistractYouTube.Models.DTOs
{
    public class EditVideoDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }       
                
        public int TopicId { get; set; }

        public int TagId { get; set; }

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
