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

//public List<Topic> GetAllTopics()
//{
//    using (var conn = Connection)
//    {
//        conn.Open();
//        using (var cmd = conn.CreateCommand())
//        {
//            cmd.CommandText = @"
//                            SELECT 
//                                   t.Id as TagId, t.Name as TagName, 
//                                   vt.Id as VideoTagId, vt.VideoId as VideoTagVideoId, vt.TagId as VideoTagTagId, 
//                                   v.Id as VideoId, v.Url, v.Title, v.Info, v.TopicId as VideoTopicId, v.UserProfileId, 
//                                   tp.Id as TopicTopicId, tp.Title as TopicTitle
//                            FROM Topic t
//                                   LEFT JOIN VideoTag vt ON t.Id = vt.TagId
//                                   LEFT JOIN Video v ON vt.VideoId = v.Id
//                                   LEFT JOIN Topic tp ON tp.Id = v.TopicId";

//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                var topics = new List<Topic>();
//                while (reader.Read())
//                {
//                    var topicId = reader.GetInt32(reader.GetOrdinal("TopicTopicId"));
//                    var existingTopic = topics.FirstOrDefault(t => t.Id == topicId);
//                    if (existingTopic == null)
//                    {
//                        existingTopic = new Topic()
//                        {
//                            Id = topicId,
//                            Title = reader.GetString(reader.GetOrdinal("TopicTitle")),
//                        };
//                        topics.Add(existingTopic);
//                    }
//                }
//                return topics;
//            }
//        }
//    }
//}
