using System;
using MySqlConnector;
using ArenaX.Models;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace ArenaX.Data
{
    public static class DatabaseHelper
    {
        // XAMPP Default Connection String
        private static string _connectionString = "Server=localhost;Database=ArenaXDB;Uid=root;Pwd=;";

        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public static User? AuthenticateUser(string username, string password)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT UserID, Username, PasswordHash, Role, Email, FullName, IsActive FROM Users WHERE Username = @Username";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isActive = reader.GetBoolean(6);
                            if (!isActive) return null;

                            string storedHash = reader.GetString(2);
                            if (VerifyPassword(password, storedHash))
                            {
                                return new User
                                {
                                    UserID = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Role = reader.GetString(3),
                                    Email = reader.GetString(4),
                                    FullName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    IsActive = isActive
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                if (storedHash.StartsWith("PBKDF2_V1$"))
                {
                    var parts = storedHash.Split('$');
                    if (parts.Length != 3) return false;

                    int iterations = int.Parse(parts[1]);
                    byte[] fullHashBytes = Convert.FromBase64String(parts[2]);

                    byte[] salt = new byte[32];
                    byte[] hash = new byte[32];
                    Array.Copy(fullHashBytes, 0, salt, 0, 32);
                    Array.Copy(fullHashBytes, 32, hash, 0, 32);

                    byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);
                    for (int i = 0; i < 32; i++)
                    {
                        if (testHash[i] != hash[i]) return false;
                    }
                    return true;
                }
            }
            catch { }
            return false;
        }

        // --- Team Operations ---
        public static List<Team> GetAllTeams()
        {
            var teams = new List<Team>();
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT TeamID, TeamName, University, CaptainName, CaptainEmail, CaptainPhone, ApprovalStatus, TeamDescription FROM Teams";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teams.Add(new Team
                        {
                            TeamID = reader.GetInt32(0),
                            TeamName = reader.GetString(1),
                            University = reader.IsDBNull(2) ? null : reader.GetString(2),
                            CaptainName = reader.GetString(3),
                            CaptainEmail = reader.GetString(4),
                            CaptainPhone = reader.IsDBNull(5) ? null : reader.GetString(5),
                            ApprovalStatus = reader.GetString(6),
                            TeamDescription = reader.IsDBNull(7) ? null : reader.GetString(7)
                        });
                    }
                }
            }
            return teams;
        }

        public static bool AddTeam(Team team)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Teams (TeamName, CaptainName, CaptainEmail, TeamDescription) 
                                 VALUES (@TeamName, @CaptainName, @CaptainEmail, @Description)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeamName", team.TeamName);
                    command.Parameters.AddWithValue("@CaptainName", team.CaptainName);
                    command.Parameters.AddWithValue("@CaptainEmail", team.CaptainEmail);
                    command.Parameters.AddWithValue("@Description", (object?)team.TeamDescription ?? DBNull.Value);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // --- Robot Operations ---
        public static List<RobotDetail> GetAllRobots()
        {
            var robots = new List<RobotDetail>();
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"SELECT r.RobotID, r.RobotName, r.Weight, r.Category, r.Description, r.IsApproved, t.TeamName 
                                 FROM Robots r 
                                 JOIN Teams t ON r.TeamID = t.TeamID";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        robots.Add(new RobotDetail
                        {
                            RobotID = reader.GetInt32(0),
                            RobotName = reader.GetString(1),
                            Weight = reader.GetDecimal(2),
                            Category = reader.GetString(3),
                            Description = reader.IsDBNull(4) ? null : reader.GetString(4),
                            IsApproved = reader.GetBoolean(5),
                            TeamName = reader.GetString(6)
                        });
                    }
                }
            }
            return robots;
        }

        public static bool AddRobot(Robot robot)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Robots (TeamID, RobotName, Weight, Category, Description) 
                                 VALUES (@TeamID, @RobotName, @Weight, @Category, @Description)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeamID", robot.TeamID);
                    command.Parameters.AddWithValue("@RobotName", robot.RobotName);
                    command.Parameters.AddWithValue("@Weight", robot.Weight);
                    command.Parameters.AddWithValue("@Category", robot.Category);
                    command.Parameters.AddWithValue("@Description", (object?)robot.Description ?? DBNull.Value);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // --- Tournament Operations ---
        public static List<Tournament> GetAllTournaments()
        {
            var tournaments = new List<Tournament>();
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT TournamentID, TournamentName, Description, StartDate, EndDate, Status, TournamentFormat, MaxTeams, Location FROM Tournaments";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tournaments.Add(new Tournament
                        {
                            TournamentID = reader.GetInt32(0),
                            TournamentName = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            StartDate = reader.GetDateTime(3),
                            EndDate = reader.GetDateTime(4),
                            Status = reader.GetString(5),
                            TournamentFormat = reader.GetString(6),
                            MaxTeams = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                            Location = reader.IsDBNull(8) ? null : reader.GetString(8)
                        });
                    }
                }
            }
            return tournaments;
        }
    }
}
