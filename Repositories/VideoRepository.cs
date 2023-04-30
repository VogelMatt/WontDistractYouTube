using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly string _connectionString;
        public VideoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Video> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Url, Title, Info, TopicId, UserProfileId  FROM Video;";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var videos = new List<Video>();
                        while (reader.Read())
                        {
                            var video = new Video()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Info = reader.GetString(reader.GetOrdinal("Info")),
                                TopicId = reader.GetInt32(reader.GetOrdinal("TopicId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                            };

                            videos.Add(video);
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
                        SELECT Id, [Name], Region, Notes 
                          FROM Video
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
