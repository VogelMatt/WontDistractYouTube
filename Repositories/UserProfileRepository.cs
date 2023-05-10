using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using WontDistractYouTube.Models;
using WontDistractYouTube.Models.DTOs;
using WontDistractYouTube.Utils;
using static WontDistractYouTube.Models.DTOs.UserProfileDto;
using VideoDto = WontDistractYouTube.Models.DTOs.UserProfileDto.VideoDto;

namespace WontDistractYouTube.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        
        //used to return userProfile details and make sure user exists at login
        public UserProfileDto GetUserProfileByFirebaseId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name, Email, DisplayName, FirebaseUserId
                                        FROM UserProfile
                                        WHERE FirebaseUserId = @firebaseUserId";
                    DbUtils.AddParameter(cmd, "@firebaseUserId", firebaseUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfileDto user = null;
                        if (reader.Read())
                        {
                            user = new UserProfileDto()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                            };
                        }
                        return user;
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



//public UserProfile GetUserProfileByIdWithVideos(int id)
//{
//    using (var conn = Connection)
//    {
//        conn.Open();
//        using (var cmd = conn.CreateCommand())
//        {
//            cmd.CommandText = @"SELECT up.Id AS UserId, up.Name, up.Email, up.DisplayName,
//                                v.Id AS VideoId, v.Title, v.Info, v.Url, v.UserProfileId,
//                                FROM UserProfile up
//                                JOIN Video v ON v.UserProfileId = up.Id
//                                WHERE up.Id = @Id";

//            DbUtils.AddParameter(cmd, "@Id", id);

//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                UserProfile user = null;
//                while (reader.Read())
//                {
//                    if (user == null)
//                    {
//                        user = new UserProfile()
//                        {
//                            Id = DbUtils.GetInt(reader, "UserId"),
//                            Name = DbUtils.GetString(reader, "Name"),
//                            Email = DbUtils.GetString(reader, "Email"),
//                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
//                            Videos = new List<Video>(),

//                        };
//                    }
//                    if (DbUtils.IsNotDbNull(reader, "VideoId"))
//                    {
//                        user.Videos.Add(new Video()
//                        {
//                            Id = DbUtils.GetInt(reader, "VideoId"),
//                            Title = DbUtils.GetString(reader, "Title"),
//                            Info = DbUtils.GetString(reader, "Description"),
//                            Url = DbUtils.GetString(reader, "Url"),
//                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
//                            TopicId = DbUtils.GetInt(reader, "TopicId")
//                        });
//                    }
//                }
//                return user;
//            }
//        }
//    }
//}

//public UserProfileDto GetUserProfileWithVideosTagsTopics(int id)
//{
//    using (var conn = Connection)
//    {
//        conn.Open();
//        using (var cmd = conn.CreateCommand())
//        {
//            cmd.CommandText = @"SELECT up.Id AS UserId, up.Name, up.Email, up.DisplayName,                                          up.FirebaseUserId,
//                                       v.Id AS VideoId, v.Title, v.Info, v.Url, v.UserProfileId,
//                                       tp.Id AS TopicId, tp.Title,
//                                       t.Id AS TagId, t.Name

//                                  FROM UserProfile up
//                                  LEFT JOIN Video v ON v.UserProfileId = up.Id
//                                  JOIN VideoTag vt ON vt.VideoId = v.Id
//                                  LEFT JOIN Tag t ON t.Id = vt.TagId
//                                  LEFT JOIN Topic tp ON tp.Id = v.TopicId
//                                  WHERE up.Id = @Id";


//            DbUtils.AddParameter(cmd, "@Id", id);
//            //cmd.Parameters.AddWithValue("@id", id);
//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                UserProfileDto user = null;
//                while (reader.Read())
//                {
//                        user = new UserProfileDto()
//                        {
//                            Id = DbUtils.GetInt(reader, "UserId"),
//                            Name = DbUtils.GetString(reader, "Name"),
//                            Email = DbUtils.GetString(reader, "Email"),
//                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
//                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
//                            Videos = new UserProfileDto.VideoDto()
//                            {
//                                Id = DbUtils.GetInt(reader, "VideoId"),
//                                Title = DbUtils.GetString(reader, "Title"),
//                                Info = DbUtils.GetString(reader, "Info"),
//                                Url = DbUtils.GetString(reader, "Url")
//                            },
//                            Topic = new TopicDto()
//                            {
//                                Id = DbUtils.GetInt(reader, "TopicId"),
//                                Title = DbUtils.GetString(reader, "TopicTitle")
//                            },
//                            Tags = new TagDto()

//                        };
//                        users.Add(existingUser);

//                    if (DbUtils.IsNotDbNull(reader, "TagId"))
//                    {
//                        existingUser.Tags.Add(new TagDto()
//                        {
//                            Id = DbUtils.GetInt(reader, "TagId"),
//                            Name = DbUtils.GetString(reader, "TagName")
//                        });
//                    }
//                }
//                return user;

//            }
//        }
//    }
//}

//public List<UserProfile> GetAll()
//{
//    using (var conn = Connection)
//    {
//        conn.Open();
//        using (var cmd = Connection.CreateCommand())
//        {
//            cmd.CommandText = @"SELECT Id, Name, Email, Url, DisplayName, FirebaseUserId
//                                FROM UserProfile";

//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                var users = new List<UserProfile>();
//                while (reader.Read())
//                {
//                    users.Add(new UserProfile()
//                    {
//                        Id = DbUtils.GetInt(reader, "Id"),
//                        Name = DbUtils.GetString(reader, "Name"),
//                        Email = DbUtils.GetString(reader, "Email"),
//                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
//                        FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
//                    });
//                }
//                return users;
//            }
//        }
//    }
//}

//public UserProfile GetById(int id)
//{
//    using (var conn = Connection)
//    {
//        conn.Open();
//        using (var cmd = conn.CreateCommand())
//        {
//            cmd.CommandText = @"SELECT Id, Name, Email, DisplayName
//                                FROM UserProfile
//                                WHERE Id = @Id";

//            DbUtils.AddParameter(cmd, "@Id", id);

//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                UserProfile user = null;
//                if (reader.Read())
//                {
//                    user = new UserProfile()
//                    {
//                        Id = DbUtils.GetInt(reader, "Id"),
//                        Name = DbUtils.GetString(reader, "Name"),
//                        Email = DbUtils.GetString(reader, "Email"),
//                        DisplayName = DbUtils.GetString(reader, "DisplayName"),

//                    };
//                }
//                return user;
//            }
//        }
//    }
//}