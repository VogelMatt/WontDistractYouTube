using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using WontDistractYouTube.Models;
using WontDistractYouTube.Utils;
using Azure;
using System.Linq;

namespace WontDistractYouTube.Repositories
{
    public class VideoRepository : BaseRepository, IVideoRepository
    {

        public VideoRepository(IConfiguration configuration) : base(configuration) { }

        
        public List<Video> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT v.Id, v.Url, v.Title, v.Info, v.TopicId, v.UserProfileId,

                                       up.Id, up.Name, up.Email, up.DisplayName, up.FirebaseUserId
                      
                        
                                FROM Video v 
                                JOIN UserProfile up ON v.UserProfileId = up.Id
                                ORDER BY TopicId
            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        var videos = new List<Video>();
                        while (reader.Read())
                        {
                            videos.Add(new Video()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Info = reader.GetString(reader.GetOrdinal("Info")),
                                TopicId = reader.GetInt32(reader.GetOrdinal("TopicId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                },
                            });
                        }

                        return videos;
                    }
                }
            }
        }

        public List<Video> GetAllVideosWithTagsAndTopics()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT v.Id AS VideoId, v.Title, v.Info, v.Url, v.TopicId,
                       up.Id AS UserProfileId, up.Name, up.Email, up.DisplayName, up.FirebaseUserId,
                       t.Id AS TagId, t.Name AS TagName,
                       tp.Id AS TopicId, tp.Title
                       
                  FROM Video v
                       LEFT JOIN Topic tp ON v.TopicId = tp.Id
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN VideoTag vt ON v.Id = vt.VideoId
                       LEFT JOIN Tag t ON vt.TagId = t.Id
                ORDER BY tp.Title, t.Id
            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var videos = new List<Video>();
                        while (reader.Read())
                        {
                            var videoId = DbUtils.GetInt(reader, "VideoId");
                            var existingVideo = videos.FirstOrDefault(v => v.Id == videoId);

                            if (existingVideo == null)
                            {
                                existingVideo = new Video()
                                {
                                    Id = videoId,
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Info = DbUtils.GetString(reader, "Info"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    TopicId = DbUtils.GetInt(reader, "TopicId"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserProfileId"),
                                        Name = DbUtils.GetString(reader, "Name"),
                                        Email = DbUtils.GetString(reader, "Email"),
                                        DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                        FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                    },
                                    Tag = new List<Tag>()
                                };

                                videos.Add(existingVideo);
                            }

                            if (DbUtils.IsNotDbNull(reader, "TagId"))
                            {
                                existingVideo.Tag.Add(new Tag()
                                {
                                    Id = DbUtils.GetInt(reader, "TagId"),
                                    Name = DbUtils.GetString(reader, "TagName")
                                });
                            }
                        }

                        return videos;
                    }
                }
            }
        }

        public Video GetByVideoId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Url, Title, Info, TopicId, UserProfileId  FROM Video
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Video video = null;
                        if (reader.Read())
                        {
                            video = new Video()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Info = reader.GetString(reader.GetOrdinal("Info")),
                                TopicId = reader.GetInt32(reader.GetOrdinal("TopicId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                            };

                        }

                        return video;
                    }
                }
            }
        }


        public void Add(Video video)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                INSERT INTO Video (Url, Title, Info, TopicId, UserProfileId)
                OUTPUT INSERTED.ID
                VALUES (@url, @title, @info, @topicId, @userProfileId)";
                    cmd.Parameters.AddWithValue("@url", video.Url);
                    cmd.Parameters.AddWithValue("@title", video.Title);
                    cmd.Parameters.AddWithValue("@info", video.Info);
                    cmd.Parameters.AddWithValue("@topicId", video.TopicId);
                    cmd.Parameters.AddWithValue("@userProfileId", video.UserProfileId);

                    video.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Update(Video video)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                UPDATE Video 
                   SET Url = @url, 
                       Title = @title, 
                       Info = @info, 
                       TopicId = @topicId, 
                       UserProfileId = @userProfileId
                 WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", video.Id);
                    cmd.Parameters.AddWithValue("@url", video.Url);
                    cmd.Parameters.AddWithValue("@title", video.Title);
                    cmd.Parameters.AddWithValue("@info", video.Info);
                    cmd.Parameters.AddWithValue("@topicId", video.TopicId);
                    cmd.Parameters.AddWithValue("@userProfileId", video.UserProfileId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Video WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
