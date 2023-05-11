using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WontDistractYouTube.Models;
using WontDistractYouTube.Utils;
using System.Linq;
using WontDistractYouTube.Models.DTOs;
using System.Reflection.PortableExecutable;
using Microsoft.Extensions.Hosting;
//using static WontDistractYouTube.Models.DTOs.VideoDto;
//using UserProfileDto = WontDistractYouTube.Models.DTOs.VideoDto.UserProfileDto;

namespace WontDistractYouTube.Repositories
{
    public class VideoRepository : BaseRepository, IVideoRepository
    {

        public VideoRepository(IConfiguration configuration) : base(configuration) { }

        public List<VideoDto> GetAllVideos()
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
                       tp.Id AS TopicId, tp.Title as TopicTitle
                       
                  FROM Video v
                       LEFT JOIN Topic tp ON v.TopicId = tp.Id
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN VideoTag vt ON v.Id = vt.VideoId
                       LEFT JOIN Tag t ON vt.TagId = t.Id
                ORDER BY tp.Title, t.Id
            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var videos = new List<VideoDto>();
                        while (reader.Read())
                        {
                            var videoId = DbUtils.GetInt(reader, "VideoId");
                            var existingVideo = videos.FirstOrDefault(v => v.Id == videoId);

                            if (existingVideo == null)
                            {
                                existingVideo = new VideoDto()
                                {
                                    Id = videoId,
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Info = DbUtils.GetString(reader, "Info"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    UserProfile = new VideoDto.UserProfileDto()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserProfileId"),
                                        Name = DbUtils.GetString(reader, "Name"),
                                        Email = DbUtils.GetString(reader, "Email"),
                                        DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                        FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                    },
                                    Topic = new VideoDto.TopicDto()
                                    {
                                        Id = DbUtils.GetInt(reader, "TopicId"),
                                        Title = DbUtils.GetString(reader, "TopicTitle")
                                    },
                                    Tags = new List<VideoDto.TagDto>()
                                };

                                videos.Add(existingVideo);
                            }

                            if (DbUtils.IsNotDbNull(reader, "TagId"))
                            {
                                existingVideo.Tags.Add(new VideoDto.TagDto()
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



        public List<UserProfileDto.VideoDto> GetAllVideosByUserId(string firebaseUserId)
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
                       tp.Id AS TopicId, tp.Title as TopicTitle
                       
                  FROM Video v
                       LEFT JOIN Topic tp ON v.TopicId = tp.Id
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN VideoTag vt ON v.Id = vt.VideoId
                       LEFT JOIN Tag t ON vt.TagId = t.Id
                WHERE  up.FirebaseUserId = @firebaseUserId
                ORDER BY tp.Title, t.Id
            ";
                    DbUtils.AddParameter(cmd, "@firebaseUserId", firebaseUserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var videos = new List<UserProfileDto.VideoDto>();
                        while (reader.Read())
                        {
                            var videoId = DbUtils.GetInt(reader, "VideoId");
                            var existingVideo = videos.FirstOrDefault(v => v.Id == videoId);

                            if (existingVideo == null)
                            {
                                existingVideo = new UserProfileDto.VideoDto()
                                {
                                    Id = videoId,
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Info = DbUtils.GetString(reader, "Info"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    Topic = new UserProfileDto.TopicDto()
                                    {
                                        Id = DbUtils.GetInt(reader, "TopicId"),
                                        Title = DbUtils.GetString(reader, "TopicTitle")
                                    },
                                    Tags = new List<UserProfileDto.TagDto>()
                                };

                                videos.Add(existingVideo);
                            }

                            if (DbUtils.IsNotDbNull(reader, "TagId"))
                            {
                                existingVideo.Tags.Add(new UserProfileDto.TagDto()
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

        public List<VideoDto> GetAllVideosByTopicId(int TopicId)
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
                       tp.Id AS TopicId, tp.Title as TopicTitle
                       
                  FROM Video v
                       LEFT JOIN Topic tp ON v.TopicId = tp.Id
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN VideoTag vt ON v.Id = vt.VideoId
                       LEFT JOIN Tag t ON vt.TagId = t.Id
                WHERE  tp.Id = @TopicId
                ORDER BY t.Id
            ";
                    DbUtils.AddParameter(cmd, "@TopicId", TopicId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var videos = new List<VideoDto>();
                        while (reader.Read())
                        {
                            var videoId = DbUtils.GetInt(reader, "VideoId");
                            var existingVideo = videos.FirstOrDefault(v => v.Id == videoId);

                            if (existingVideo == null)
                            {
                                existingVideo = new VideoDto()
                                {
                                    Id = videoId,
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Info = DbUtils.GetString(reader, "Info"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    UserProfile = new VideoDto.UserProfileDto()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserProfileId"),
                                        Name = DbUtils.GetString(reader, "Name"),
                                        Email = DbUtils.GetString(reader, "Email"),
                                        DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                        FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                    },
                                    Topic = new VideoDto.TopicDto()
                                    {
                                        Id = DbUtils.GetInt(reader, "TopicId"),
                                        Title = DbUtils.GetString(reader, "TopicTitle")
                                    },
                                    Tags = new List<VideoDto.TagDto>()
                                };

                                videos.Add(existingVideo);
                            }

                            if (DbUtils.IsNotDbNull(reader, "TagId"))
                            {
                                existingVideo.Tags.Add(new VideoDto.TagDto()
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


        public VideoDto GetByVideoId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT v.Id AS VideoId, v.Title, v.Info, v.Url, v.TopicId as VideoTopicId,
                       up.Id AS UserProfileId, up.Name, up.Email, up.DisplayName, up.FirebaseUserId,
                       t.Id AS TagId, t.Name AS TagName,
                       tp.Id AS TopicId, tp.Title as TopicTitle
                       
                  FROM Video v
                       LEFT JOIN Topic tp ON v.TopicId = tp.Id
                       JOIN UserProfile up ON v.UserProfileId = up.Id
                       LEFT JOIN VideoTag vt ON v.Id = vt.VideoId
                       LEFT JOIN Tag t ON vt.TagId = t.Id                
                       WHERE VideoId = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        VideoDto existingVideo = null;
                        if (reader.Read())
                        {
                            existingVideo = new VideoDto()
                            {
                                Id = id,
                                Title = DbUtils.GetString(reader, "Title"),
                                Info = DbUtils.GetString(reader, "Info"),
                                Url = DbUtils.GetString(reader, "Url"),
                                UserProfile = new VideoDto.UserProfileDto()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                },
                                Topic = new VideoDto.TopicDto()
                                {
                                    Id = DbUtils.GetInt(reader, "TopicId"),
                                    Title = DbUtils.GetString(reader, "TopicTitle")
                                },
                                Tags = new List<VideoDto.TagDto>()
                            };

                            if (DbUtils.IsNotDbNull(reader, "TagId"))
                            {
                                existingVideo.Tags.Add(new VideoDto.TagDto()
                                {
                                    Id = DbUtils.GetInt(reader, "TagId"),
                                    Name = DbUtils.GetString(reader, "TagName")
                                });
                            };

                        }
                        return existingVideo;

                    }
                }
            }
        }


        //public void Add(Video video)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        INSERT INTO Video (Url, Title, Info, TopicId, UserProfileId)
        //        OUTPUT INSERTED.ID
        //        VALUES (@Url, @Title, @Info, @TopicId, @UserProfileId)";

        //            DbUtils.AddParameter(cmd, "@Url", video.Url);
        //            DbUtils.AddParameter(cmd, "@Title", video.Title);
        //            DbUtils.AddParameter(cmd, "@Info", video.Info);
        //            DbUtils.AddParameter(cmd, "@TopicId", video.TopicId);
        //            DbUtils.AddParameter(cmd, "@UserProfileId", video.UserProfileId);                    

        //            video.Id = (int)cmd.ExecuteScalar();
        //        }
        //    }
        //}

        public void Add(Video video, int selectedTopicId, List<int> selectedTagIds)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // Set the TopicId value to the selected topic
                    video.TopicId = selectedTopicId;

                    // Insert the new row into the Video table
                    cmd.CommandText = @"
                INSERT INTO Video (Url, Title, Info, TopicId, UserProfileId)
                OUTPUT INSERTED.ID
                VALUES (@Url, @Title, @Info, @TopicId, @UserProfileId)";


                    cmd.Parameters.AddWithValue("@id", video.Id);
                    cmd.Parameters.AddWithValue("@url", video.Url);
                    cmd.Parameters.AddWithValue("@title", video.Title);
                    cmd.Parameters.AddWithValue("@info", video.Info);
                    cmd.Parameters.AddWithValue("@topicId", video.TopicId);
                    cmd.Parameters.AddWithValue("@userProfileId", video.UserProfileId);
                    

                    video.Id = (int)cmd.ExecuteScalar();

                    // Insert rows into the VideoTag table for the selected tags
                    foreach (var tagId in selectedTagIds)
                    {
                        cmd.CommandText = "INSERT INTO VideoTag (VideoId, TagId) VALUES (@VideoId, @TagId)";
                        DbUtils.AddParameter(cmd, "@VideoId", video.Id);
                        DbUtils.AddParameter(cmd, "@TagId", tagId);
                        cmd.ExecuteNonQuery();
                    }
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


//DbUtils.AddParameter(cmd, "@Url", video.Url);
//DbUtils.AddParameter(cmd, "@Title", video.Title);
//DbUtils.AddParameter(cmd, "@Info", video.Info);
//DbUtils.AddParameter(cmd, "@TopicId", video.TopicId);
//DbUtils.AddParameter(cmd, "@UserProfileId", video.UserProfileId);




//Video video = null;
//if (reader.Read())
//{
//    video = new Video()
//    {
//        Id = reader.GetInt32(reader.GetOrdinal("VideoId")),
//        Url = reader.GetString(reader.GetOrdinal("Url")),
//        Title = reader.GetString(reader.GetOrdinal("Title")),
//        Info = reader.GetString(reader.GetOrdinal("Info")),
//        TopicId = reader.GetInt32(reader.GetOrdinal("VideoTopicId")),
//        UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
//    };

//}

//public List<Video> GetAll()
//{
//    using (var conn = Connection)
//    {
//        conn.Open();
//        using (var cmd = conn.CreateCommand())
//        {
//            cmd.CommandText = @"
//                        SELECT v.Id as VideoId, v.Url, v.Title, v.Info, v.TopicId as VideoTopicId, v.UserProfileId as VideoUserProfileId,
//                               up.Id as UserProfileId, up.Name,up.DisplayName,
//                               t.Id as TopicId, t.Title as TopicTitle,
//                               vt.Id as VideoTagId, vt.TagId as VideoTagTagId


//                        FROM Video v 
//                            LEFT JOIN VideoTag vt ON  v.Id = vt.VideoId 
//                            LEFT JOIN UserProfile up ON v.UserProfileId = up.Id
//                            LEFT JOIN Topic t ON v.TopicId = t.Id";

//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {

//                var videos = new List<Video>();
//                while (reader.Read())
//                {
//                    var videoId = reader.GetInt32(reader.GetOrdinal("VideoId"));
//                    var existingVideo = videos.FirstOrDefault(v => v.Id == videoId) ?? new Video()
//                    {
//                        Id = reader.GetInt32(reader.GetOrdinal("VideoId")),
//                        Url = reader.GetString(reader.GetOrdinal("Url")),
//                        Title = reader.GetString(reader.GetOrdinal("Title")),
//                        Info = reader.GetString(reader.GetOrdinal("Info")),
//                        TopicId = reader.GetInt32(reader.GetOrdinal("VideoTopicId")),
//                        UserProfileId = reader.GetInt32(reader.GetOrdinal("VideoUserProfileId")),
//                        UserProfile = new UserProfile()
//                        {
//                            Id = DbUtils.GetInt(reader, "UserProfileId"),
//                            Name = DbUtils.GetString(reader, "Name"),
//                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
//                        },
//                        TagIds = new List<int>()
//                    };
//                    videos.Add(existingVideo);

//                    if (!reader.IsDBNull(reader.GetOrdinal("VideoTagId")))
//                    {
//                        existingVideo.TagIds.Add(reader.GetInt32(reader.GetOrdinal("VideoTagTagId")));
//                    }
//                }
//                return videos;
//            }

//        }
//    }
//}