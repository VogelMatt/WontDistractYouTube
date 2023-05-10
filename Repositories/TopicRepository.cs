using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public class TopicRepository : BaseRepository
    {
        public TopicRepository(IConfiguration configuration) : base(configuration) { }
    {
        public List<Topic> GetAllTopics()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT t.Id as TagId, t.Name as TagName, 
                                           vt.Id as VideoTagId, vt.VideoId as VideoTagVideoId, vt.TagId as VideoTagTagId, 
                                           v.Id as VideoId, v.Url, v.Title, v.Info, v.TopicId as VideoTopicId, v.UserProfileId, 
                                           tp.Id as TopicTopicId, tp.Title as TopicTitle
                                    FROM Tag t
                                           LEFT JOIN VideoTag vt ON t.Id = vt.TagId
                                           LEFT JOIN Video v ON vt.VideoId = v.Id
                                           LEFT JOIN Topic tp ON tp.Id = v.TopicId";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var tags = new List<Tag>();
                        while (reader.Read())
                        {
                            var tagId = reader.GetInt32(reader.GetOrdinal("TagId"));
                            var existingTag = tags.FirstOrDefault(t => t.Id == tagId);
                            if (existingTag == null)
                            {
                                existingTag = new Tag()
                                {
                                    Id = tagId,
                                    Name = reader.GetString(reader.GetOrdinal("TagName")),
                                };
                                tags.Add(existingTag);
                            }
                        }
                        return tags;
                    }
                }
            }
        }
    }
}
