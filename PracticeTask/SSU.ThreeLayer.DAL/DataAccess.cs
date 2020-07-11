using SSU.ThreeLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SSU.ThreeLayer.DAL
{
    public class DataAccess : IDataAccess
    {
        private string connectionString = "Data Source=DOMASHNIY\\SQLEXPRESS;Initial Catalog=PracticeDatabase;Integrated Security=True";

        public User currentUser;

        public DataAccess() //конструктор класса
        {

        }

        public User GetCurrentUser()
        {
            return currentUser;
        }

        private int GetUserPasswordHash(User user)
        {
            if (user.IsPasswordKnown)
            {
                return user.Password.GetHashCode();
            }
            else
            {
                throw new NullReferenceException("The password is stored in protected form and therefore cannot be identified.");
            }
        }

        public void AddUser(User user)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        string t = BitConverter.ToString(BitConverter.GetBytes(GetUserPasswordHash(user))).Replace("-", "").ToLower();

                        command.CommandText = "INSERT INTO Users(Login, HashPassword, Name, DateOfBirth, Info, IsAdmin) " +
                            "VALUES('" + user.Login + "', 0x" + t + ", '" + user.Name + "', '" + user.DateOfBirth.ToString("yyyy-MM-dd") + "', '" + user.Info + "', " + (user.IsAdmin ? 1 : 0) + ")";
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
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public void ChangeCurrentUserDateOfBirth(DateTime newDate)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "UPDATE Users SET DateOfBirth = '" + newDate.ToString("yyyy-MM-dd") + "' WHERE Login = '" + currentUser.Login + "';";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();

                        currentUser.DateOfBirth = newDate;
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "UPDATE Users SET Info = '" + newInfo + "' WHERE Login = '" + currentUser.Login + "';";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();

                        currentUser.Info = newInfo;
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public void ChangeCurrentUserName(string newName)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "UPDATE Users SET Name = '" + newName + "' WHERE Login = '" + currentUser.Login + "';";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();

                        currentUser.Name = newName;
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public void ChangeCurrentUserPassword(string newPassword)
        {
            string oldPass = currentUser.Password;

            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    currentUser.Password = newPassword;
                    string t = BitConverter.ToString(BitConverter.GetBytes(GetUserPasswordHash(currentUser))).Replace("-", "").ToLower();

                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "UPDATE Users SET HashPassword = 0x" + t + " WHERE Login = '" + currentUser.Login + "';";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    currentUser.Password = oldPass;
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public void DeleteUser(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "DELETE FROM Users WHERE Id = " + index + ";";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public List<Shop> GetAllShops()
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Shops INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id";
                        command.Connection = cnn;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Shop> shops = new List<Shop>();

                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read()) // построчно считываем данные
                                {
                                    Shop temp = new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(5), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                    shops.Add(temp);
                                }
                            }

                            return shops;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
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
                                    User temp = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
                                    users.Add(temp);
                                }
                            }

                            return users;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public string GetShopRatingByIndex(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "GetShopRatingById";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = cnn;

                        SqlParameter ShopId = new SqlParameter("@ShopId", SqlDbType.Int);
                        ShopId.Value = index;
                        command.Parameters.Add(ShopId);

                        SqlParameter ShopRating = new SqlParameter("@ShopRating", SqlDbType.Float);
                        ShopRating.Direction = ParameterDirection.Output;
                        command.Parameters.Add(ShopRating);

                        command.ExecuteNonQuery();

                        return ShopRating.Value.ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public string GetShopRatingByName(string shopName)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "GetShopRatingByName";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = cnn;

                        SqlParameter ShopName = new SqlParameter("@ShopName", SqlDbType.VarChar);
                        ShopName.Value = shopName;
                        command.Parameters.Add(ShopName);

                        SqlParameter ShopRating = new SqlParameter("@ShopRating", SqlDbType.Float);
                        ShopRating.Direction = ParameterDirection.Output;
                        command.Parameters.Add(ShopRating);

                        command.ExecuteNonQuery();

                        return ShopRating.Value.ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
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
                try
                {
                    cnn.Open();
                    bool result = false;
                    int hash = password.GetHashCode();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Users WHERE (Login = '" + login + "')";
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
                                        currentUser = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
                                        break;
                                    }
                                }
                            }

                            return result;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public User GetUser(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Users WHERE (Users.Id = " + index + ")";
                        command.Connection = cnn;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public User GetUser(string login)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Users WHERE (Users.Login = '" + login + "')";
                        command.Connection = cnn;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            reader.Read();
                            return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public Shop GetShop(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Shops INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id";
                        command.Connection = cnn;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            return new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(5), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public List<Shop> FindShopsByName(string shopName)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Shops " +
                        "INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id WHERE Shops.Name = '" + shopName + "'";
                        command.Connection = cnn;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Shop> shops = new List<Shop>();

                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read()) // построчно считываем данные
                                {
                                    Shop temp = new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(5), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                    shops.Add(temp);
                                }
                            }

                            return shops;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public List<Shop> FindShopsByCity(string city)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Shops " +
                        "INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id WHERE Addresses.City = '" + city + "'";
                        command.Connection = cnn;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Shop> shops = new List<Shop>();

                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read()) // построчно считываем данные
                                {
                                    Shop temp = new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(5), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                    shops.Add(temp);
                                }
                            }

                            return shops;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public List<Shop> FindShopsByCityAndType(string city, string type)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Shops " +
                        "INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id " +
                        "WHERE (Addresses.City = '" + city + "' AND ShopTypes.Name = '" + type + "')";
                        command.Connection = cnn;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Shop> shops = new List<Shop>();

                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read()) // построчно считываем данные
                                {
                                    Shop temp = new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(5), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                    shops.Add(temp);
                                }
                            }

                            return shops;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        private void UpdateRating(int shopId, int rating)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "UPDATE Ratings SET Rating = " + rating + " WHERE (shopId = " + shopId + " AND userId = " + currentUser.Id + ");";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public void RateShop(int shopId, int rating)
        {
            if (rating < 0 || rating > Shop.MaxRating)
            {
                throw new ArgumentException("Rating value is out of bounds.");
            }

            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "INSERT INTO Ratings(ShopId, UserId, Rating) VALUES (" + shopId + ", " + currentUser.Id + ", " + rating + ")";
                        command.Connection = cnn;
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    if (e.Number == 2601)
                    {
                        try
                        {
                            cnn.Close();
                            UpdateRating(shopId, rating);
                        }
                        catch (Exception ex)
                        {
                            throw new ArgumentException(ex.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }
    }
}
