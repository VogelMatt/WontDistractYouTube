using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository(IConfiguration configuration) : base(configuration) { }

        public List<Topic> GetAllTopics()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT *
                                           
                                    FROM Topic t";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var topics = new List<Topic>();
                        while (reader.Read())
                        {
                            var topicId = reader.GetInt32(reader.GetOrdinal("Id"));
                            var existingTopic = topics.FirstOrDefault(t => t.Id == topicId);
                            if (existingTopic == null)
                            {
                                existingTopic = new Topic()
                                {
                                    Id = topicId,
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                };
                                topics.Add(existingTopic);
                            }
                        }
                        return topics;
                    }
                }
            }
        }
        
    }
}


