using SSU.ThreeLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SSU.ThreeLayer.DAL
{
    public class BaseDatabase : IBaseDatabase
    {
        private SqlConnection cnn = new SqlConnection("Data Source=DOMASHNIY\\SQLEXPRESS;Initial Catalog=PracticeDatabase;Integrated Security=True");

        public User CurrentUser;

        public BaseDatabase() //конструктор класса
        {

        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }

        public void AddUser(User user)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                string t = BitConverter.ToString(BitConverter.GetBytes(user.GetHashCode())).Replace("-", "").ToLower();

                command.CommandText = "INSERT INTO Users(Login, HashPassword, Name, DateOfBirth, Info, IsAdmin) " +
                    "VALUES('" + user.Login + "', 0x" + t + ", '" + user.Name + "', '" + user.DateOfBirth.ToString("yyyy-MM-dd") + "', '" + user.Info + "', " + (user.IsAdmin ? 1 : 0) + ")";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void ChangeCurrentUserDateOfBirth(DateTime newDate)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Users SET DateOfBirth = '" + newDate.ToString("yyyy-MM-dd") + "' WHERE Login = '" + CurrentUser.Login + "';";
                command.Connection = cnn;
                command.ExecuteNonQuery();

                CurrentUser.DateOfBirth = newDate;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Users SET Info = '" + newInfo + "' WHERE Login = '" + CurrentUser.Login + "';";
                command.Connection = cnn;
                command.ExecuteNonQuery();

                CurrentUser.Info = newInfo;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void ChangeCurrentUserName(string newName)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Users SET Name = '" + newName + "' WHERE Login = '" + CurrentUser.Login + "';";
                command.Connection = cnn;
                command.ExecuteNonQuery();

                CurrentUser.Name = newName;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void ChangeCurrentUserPassword(string newPassword)
        {
            string oldPass = CurrentUser.Password;

            try
            {
                CurrentUser.Password = newPassword;

                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Users SET HashPassword = '" + BitConverter.GetBytes(CurrentUser.GetHashCode()) + "' WHERE Login = '" + CurrentUser.Login + "';";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                CurrentUser.Password = oldPass;
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteUser(int index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Users WHERE Id = " + index + ";";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public List<Shop> GetAllShops()
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Shops";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<Shop> shops = new List<Shop>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Shop temp = new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        shops.Add(temp);
                    }
                }

                reader.Close();

                return shops;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<User> users = new List<User>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        User temp = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
                        users.Add(temp);
                    }
                }

                reader.Close();

                return users;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public float GetShopRatingByIndex(int index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "GetShopRatingById";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = cnn;

                SqlParameter ShopId = new SqlParameter("@ShopId", SqlDbType.Int);
                ShopId.Value = index;
                command.Parameters.Add(ShopId);

                var ShopRating = command.Parameters.Add("@ShopRating", SqlDbType.Float);
                ShopRating.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                return (float)ShopRating.Value;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public float GetShopRatingByName(string shopName)
        {
            try
            {
                cnn.Open();
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "GetShopRatingByName";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = cnn;

                SqlParameter ShopName = new SqlParameter("@ShopName", SqlDbType.VarChar);
                ShopName.Value = shopName;
                command.Parameters.Add(ShopName);

                var ShopRating = command.Parameters.Add("@ShopRating", SqlDbType.Float);
                ShopRating.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                return (float)ShopRating.Value;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public bool LogIn(string login, string password)
        {
            try
            {
                bool result = false;
                int hash = password.GetHashCode();

                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users WHERE (Login = '" + login + "')";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        byte[] t = new byte[4];
                        reader.GetBytes(2, 0, t, 0, 4);

                        if (hash == BitConverter.ToInt32(t, 0))
                        {
                            result = true;
                            CurrentUser = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
                            break;
                        }
                    }
                }

                reader.Close();

                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public User GetUser(int index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users WHERE (Users.Id = " + index + ")";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                reader.Close();

                return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public User GetUser(string login)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users WHERE (Users.Login = '" + login + "')";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                reader.Close();

                return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetBoolean(6));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public Shop GetShop(int index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Shops WHERE (Shops.Id = " + index + ")";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                reader.Close();

                return new Shop(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }






        /*public void AddUser(User user)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Users(Name, DateOfBirth) VALUES('" + user.Name + "', '" + user.DateOfBirth.ToString("yyyy-MM-dd") + "')";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteUser(string name)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Users WHERE Name = '" + name + "'";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteUser(uint index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Users WHERE Id = " + index;
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void AddAward(Award award)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Awards(Title) VALUES('" + award.Title + "')";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteAward(string title)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Users WHERE Title = '" + title + "'";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteAward(uint index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Users WHERE Id = " + index;
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void AddLinker(User user, Award award)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Linkers(IdUser, IdAward) VALUES(" + user.Id + "," + award.Id + ")";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void AddLinker(uint userId, uint awardId)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Linkers(IdUser, IdAward) VALUES(" + userId + "," + awardId + ")";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteLinker(User user, Award award)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Linkers WHERE (IdUser = " + user.Id + " AND IdAward = " + award.Id + ")";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void DeleteLinker(uint userId, uint awardId)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Linkers WHERE (IdUser = " + userId + " AND IdAward = " + awardId + ")";
                command.Connection = cnn;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetAllUsers()
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                Hashtable users = new Hashtable();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        User temp = new User((uint)reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
                        users.Add(temp.Id, temp);
                    }
                }

                reader.Close();

                return users.Values;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetAllAwards()
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Awards";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                Hashtable awards = new Hashtable();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Award temp = new Award((uint)reader.GetInt32(0), reader.GetString(1));
                        awards.Add(temp.Id, temp);
                    }
                }

                reader.Close();

                return awards.Values;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetAllLinkers()
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Linkers";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<Pair<uint, uint>> linkers = new List<Pair<uint, uint>>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Pair<uint, uint> temp = new Pair<uint, uint>((uint)reader.GetInt32(0), (uint)reader.GetInt32(1));
                        linkers.Add(temp);
                    }
                }

                reader.Close();

                return linkers;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetUsersByAward(Award award)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users a WHERE EXISTS(SELECT * FROM Linkers WHERE (IdUser = a.Id AND IdAward = " + award.Id + "))";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<User> users = new List<User>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        User temp = new User((uint)reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
                        users.Add(temp);
                    }
                }

                reader.Close();

                return users;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetUsersByAward(string title)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users a WHERE " +
                    "EXISTS(SELECT * FROM (Linkers INNER JOIN Awards ON Linkers.IdAward = Awards.Id) WHERE (IdUser = a.Id AND Title = '" + title + "'))";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<User> users = new List<User>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        User temp = new User((uint)reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
                        users.Add(temp);
                    }
                }

                reader.Close();

                return users;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetAwardsByUser(User user)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Awards a WHERE EXISTS(SELECT * FROM Linkers WHERE (IdAward = a.Id AND IdUser = " + user.Id + "))";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<Award> awards = new List<Award>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Award temp = new Award((uint)reader.GetInt32(0), reader.GetString(1));
                        awards.Add(temp);
                    }
                }

                reader.Close();

                return awards;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public IEnumerable GetAwardsByUser(string name)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Awards a WHERE " +
                    "EXISTS(SELECT * FROM (Linkers INNER JOIN Users ON Linkers.IdUser = Users.Id) WHERE (IdAward = a.Id AND Name = '" + name + "'))";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                List<Award> awards = new List<Award>();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Award temp = new Award((uint)reader.GetInt32(0), reader.GetString(1));
                        awards.Add(temp);
                    }
                }

                reader.Close();

                return awards;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public User GetUser(uint index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users WHERE (Users.Id = " + index + ")";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                reader.Close();

                return new User((uint)reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public Award GetAward(uint index)
        {
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Awards WHERE (Award.Id = " + index + ")";
                command.Connection = cnn;
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                reader.Close();

                return new Award((uint)reader.GetInt32(0), reader.GetString(1));
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }*/
    }
}
