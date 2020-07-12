using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SSU.ThreeLayer.DAL
{
    public class UserAccess : IUserAccess
    {
        private string connectionString = "Data Source=DOMASHNIY\\SQLEXPRESS;Initial Catalog=PracticeDatabase;Integrated Security=True";

        private User currentUser;

        public User GetCurrentUser()
        {
            return currentUser;
        }

        public void AddUser(User user, string passwordHashStr)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = string.Format("INSERT INTO Users(Login, HashPassword, Name, DateOfBirth, Info, IsAdmin) VALUES('{0}', 0x{1}, '{2}', '{3}', {4}, {5})", user.Login, passwordHashStr, user.Name, user.DateOfBirth.ToString("yyyy-MM-dd"), (user.Info.Length != 0 ? string.Format("'{0}'", user.Info) : "NULL"), (user.IsAdmin ? 1 : 0));
                        command.Connection = cnn;
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    if (e.Number == 2627)
                    {
                        throw new ArgumentException("This login is already occupied.");
                    }
                }
            }
        }

        public void ChangeCurrentUserDateOfBirth(DateTime newDate)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("UPDATE Users SET DateOfBirth = '{0}' WHERE Login = '{1}';", newDate.ToString("yyyy-MM-dd"), currentUser.Login);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();

                    currentUser.DateOfBirth = newDate;
                }
            }
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = String.Format("UPDATE Users SET Info = {0} WHERE Login = '{1}';", (newInfo.Length != 0 ? string.Format("'{0}'", newInfo) : "NULL"), currentUser.Login);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();

                    currentUser.Info = newInfo;
                }
            }
        }

        public void ChangeCurrentUserName(string newName)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("UPDATE Users SET Name = '{0}' WHERE Login = '{1}';", newName, currentUser.Login);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();

                    currentUser.Name = newName;
                }
            }
        }

        public void ChangeCurrentUserPassword(string newPassword, string passwordHashStr)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("UPDATE Users SET HashPassword = 0x{0} WHERE Login = '{1}';", passwordHashStr, currentUser.Login);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();
                }
                currentUser.Password = newPassword;
            }
        }

        public void DeleteUser(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("DELETE FROM Users WHERE Id = {0};", index);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM Users";
                    command.Connection = cnn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        List<User> users = new List<User>();

                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                User temp = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.IsDBNull(5) ? "" : reader.GetString(5), reader.GetBoolean(6));
                                users.Add(temp);
                            }
                        }

                        return users;
                    }
                }
            }
        }

        public bool LogIn(string login, string password)
        {
            if (password.Length < User.PasswordMinLength)
            {
                return false;
            }

            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                bool result = false;
                int hash = password.GetHashCode();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM Users WHERE (Login = '{0}')", login);
                    command.Connection = cnn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                byte[] t = new byte[4];
                                reader.GetBytes(2, 0, t, 0, 4);

                                if (hash == BitConverter.ToInt32(t, 0))
                                {
                                    result = true;
                                    currentUser = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.IsDBNull(5) ? "" : reader.GetString(5), reader.GetBoolean(6));
                                    break;
                                }
                            }
                        }

                        return result;
                    }
                }
            }
        }

        public User GetUser(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM Users WHERE (Users.Id = {0})", index);
                    command.Connection = cnn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.IsDBNull(5) ? "" : reader.GetString(5), reader.GetBoolean(6));
                    }
                }
            }
        }

        public User GetUser(string login)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM Users WHERE (Users.Login = '{0}')", login);
                    command.Connection = cnn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        reader.Read();
                        return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.IsDBNull(5) ? "" : reader.GetString(5), reader.GetBoolean(6));
                    }
                }
            }
        }

        public void RateShop(int shopId, int rating)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "RateShopOrUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = cnn;

                    SqlParameter ShopId = new SqlParameter("@ShopId", SqlDbType.Int);
                    ShopId.Value = shopId;
                    command.Parameters.Add(ShopId);

                    SqlParameter UserId = new SqlParameter("@UserId", SqlDbType.Int);
                    UserId.Value = currentUser.Id;
                    command.Parameters.Add(UserId);

                    SqlParameter Rating = new SqlParameter("@Rating", SqlDbType.Int);
                    Rating.Value = rating;
                    command.Parameters.Add(Rating);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
