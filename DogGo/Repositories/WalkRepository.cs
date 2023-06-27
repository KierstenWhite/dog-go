using DogGo.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Date, Duration, WalkerId, DogId
                            FROM Walks
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();
                    while (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public Walk GetWalkById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId
                        FROM Walks w
                        WHERE w.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };

                        reader.Close();
                        return walk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public List<Walk> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection) 
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walks.Date, Owner.Name AS OwnerName, Walks.Duration
                        FROM Walks
                        JOIN Walker ON Walks.WalkerId = Walker.Id
                        JOIN Dog ON Walks.DogId = Dog.Id
                        JOIN Owner ON Dog.OwnerId = Owner.Id
                    ";

                    cmd.Parameters.AddWithValue("walkerId", walkerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            Owner = new Owner()
                            {
                                Name = reader.GetString(reader.GetOrdinal("OwnerName"))
                            }
                        };

                        walks.Add(walk);
                    }

                    reader.Close();
                    return walks;
                }
            }
        }
    }
}

//        public void AddDog(Dog dog)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                    INSERT INTO Dog ([Name], Breed, Notes, ImageUrl, OwnerId)
//                    OUTPUT INSERTED.ID
//                    VALUES (@name, @breed, @notes, @imageUrl, @ownerId);
//                ";

//                    cmd.Parameters.AddWithValue("@name", dog.Name);
//                    cmd.Parameters.AddWithValue("@breed", dog.Breed);
//                    cmd.Parameters.AddWithValue("@notes", dog.Notes == null ? DBNull.Value : dog.Notes);
//                    cmd.Parameters.AddWithValue("@imageUrl", dog.ImageUrl == null ? DBNull.Value : dog.ImageUrl);
//                    cmd.Parameters.AddWithValue("@ownerId", dog.OwnerId);

//                    int id = (int)cmd.ExecuteScalar();

//                    dog.Id = id;
//                }
//            }
//        }

//        public void UpdateDog(Dog dog)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();

//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                            UPDATE Dog
//                            SET 
//                                [Name] = @name, 
//                                Breed = @breed, 
//                                Notes = @notes, 
//                                ImageUrl = @imageUrl, 
//                                OwnerId = @ownerId
//                            WHERE Id = @id";

//                    cmd.Parameters.AddWithValue("@name", dog.Name);
//                    cmd.Parameters.AddWithValue("@breed", dog.Breed);
//                    cmd.Parameters.AddWithValue("@notes", dog.Notes);
//                    cmd.Parameters.AddWithValue("@imageUrl", dog.ImageUrl);
//                    cmd.Parameters.AddWithValue("@ownerId", dog.OwnerId);
//                    cmd.Parameters.AddWithValue("@id", dog.Id);

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        public void DeleteDog(int dogId)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();

//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                            DELETE FROM Dog
//                            WHERE Id = @id
//                        ";

//                    cmd.Parameters.AddWithValue("@id", dogId);

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//    }
//}