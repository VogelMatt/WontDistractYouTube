using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using WontDistractYouTube.Models;
using WontDistractYouTube.Utils;

namespace WontDistractYouTube.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }


        //public List<UserProfile> GetAll()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                        SELECT up.Id, up.Name, up.Email, up.DisplayName, up.FirebaseUserId
        //                        v.Id, v.Url, v.Title, v.Info, v.TopicId, v.UserProfileId,



        //                        FROM UserProfile up 
        //                        JOIN Video v ON v.UserProfileId = up.Id
        //                        ORDER BY up.Id
        //    ";

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {

        //                var userProfile = new List<UserProfile>();
        //                while (reader.Read())
        //                {
        //                    userProfile.Add(new UserProfile()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "UserProfileId"),
        //                        Name = DbUtils.GetString(reader, "Name"),
        //                        Email = DbUtils.GetString(reader, "Email"),
        //                        DisplayName = DbUtils.GetString(reader, "DisplayName"),
        //                        FirebaseUserId = DbUtils.GetInt(reader, "FirebaseUserId"),

        //                    });
        //                }
        //                return userProfile;

        //            }
        //        }
        //    }
        //}

        //public List<UserProfile> GetAllUserProfilesWithVideosTagsAndTopics()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        SELECT v.Id AS VideoId, v.Title, v.Info, v.Url, v.TopicId,

        //               up.Id AS UserProfileId, up.Name, up.Email, up.DisplayName, up.FirebaseUserId,

        //               t.Id AS TagId, t.Name AS TagName,

        //               tp.Id AS TopicId, tp.Title,

        //               vt.TagId, vt.VideoId,

        //          FROM Video v
        //               LEFT JOIN Topic tp ON v.TopicId = tp.Id
        //               JOIN UserProfile up ON v.UserProfileId = up.Id
        //               LEFT JOIN VideoTag vt ON v.Id = vt.VideoId
        //               LEFT JOIN Tag t ON vt.TagId = t.Id
        //        ORDER BY tp.Title, t.Id
        //    ";

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                var videos = new List<Video>();
        //                while (reader.Read())
        //                {
        //                    var videoId = DbUtils.GetInt(reader, "VideoId");
        //                    var existingVideo = videos.FirstOrDefault(v => v.Id == videoId);

        //                    if (existingVideo == null)
        //                    {
        //                        existingVideo = new Video()
        //                        {
        //                            Id = videoId,
        //                            Title = DbUtils.GetString(reader, "Title"),
        //                            Info = DbUtils.GetString(reader, "Info"),
        //                            Url = DbUtils.GetString(reader, "Url"),
        //                            TopicId = DbUtils.GetInt(reader, "TopicId"),
        //                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
        //                            UserProfile = new UserProfile()
        //                            {
        //                                Id = DbUtils.GetInt(reader, "UserProfileId"),
        //                                Name = DbUtils.GetString(reader, "Name"),
        //                                Email = DbUtils.GetString(reader, "Email"),
        //                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
        //                                FirebaseUserId = DbUtils.GetInt(reader, "FirebaseUserId")
        //                            },
        //                            Tag = new List<Tag>()
        //                        };

        //                        videos.Add(existingVideo);
        //                    }

        //                    if (DbUtils.IsNotDbNull(reader, "TagId"))
        //                    {
        //                        existingVideo.Tag.Add(new Tag()
        //                        {
        //                            Id = DbUtils.GetInt(reader, "TagId"),
        //                            Name = DbUtils.GetString(reader, "TagName")
        //                        });
        //                    }
        //                }

        //                return videos;
        //            }
        //        }
        //    }
        //}

        public UserProfile GetAllVideosByUserProfileId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Name, Email, DisplayName, FirebaseUserId  FROM UserProfile
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                        if (reader.Read())
                        {
                            userProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                FirebaseUserId = reader.GetInt32(reader.GetOrdinal("FirebaseUserId")),
                            };

                        }

                        return userProfile;
                    }
                }
            }
        }


        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                INSERT INTO UserProfile (Name, Email, DisplayName, FirebaseUserId)
                OUTPUT INSERTED.ID
                VALUES (@Name, @Email, @DisplayName, @FirebaseUserId)";
                    cmd.Parameters.AddWithValue("@Name", userProfile.Name);
                    cmd.Parameters.AddWithValue("@Email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@DisplayName", userProfile.DisplayName);
                    cmd.Parameters.AddWithValue("@FirebaseUserId", userProfile.FirebaseUserId);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                UPDATE UserProfile 
                   SET Name = @Name, 
                       Email = @Email, 
                       DisplayName = @DisplayName, 
                       FirebaseUserId = @FirebaseUserId, 
                       
                 WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", userProfile.Id);
                    cmd.Parameters.AddWithValue("@Name", userProfile.Name);
                    cmd.Parameters.AddWithValue("@Email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@DisplayName", userProfile.DisplayName);
                    cmd.Parameters.AddWithValue("@FirebaseUserId", userProfile.FirebaseUserId);


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
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
