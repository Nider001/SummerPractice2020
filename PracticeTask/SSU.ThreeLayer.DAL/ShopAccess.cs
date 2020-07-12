using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SSU.ThreeLayer.DAL
{
    public class ShopAccess : IShopAccess
    {
        private string connectionString = "Data Source=DOMASHNIY\\SQLEXPRESS;Initial Catalog=PracticeDatabase;Integrated Security=True";

        public List<Shop> GetAllShops()
        {
            using (var cnn = new SqlConnection(connectionString))
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
        }

        public string GetShopRatingByIndex(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
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
        }

        public string GetShopRatingByName(string shopName)
        {
            using (var cnn = new SqlConnection(connectionString))
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
        }

        public Shop GetShop(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
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
        }

        public List<Shop> FindShopsByName(string shopName)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM Shops INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id WHERE Shops.Name = '{0}'", shopName);
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
        }

        public List<Shop> FindShopsByCity(string city)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM Shops INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id WHERE Addresses.City = '{0}'", city);
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
        }

        public List<Shop> FindShopsByCityAndType(string city, string type)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM Shops INNER JOIN ShopTypes ON Shops.TypeId = ShopTypes.Id INNER JOIN Addresses ON Shops.AddressId = Addresses.Id WHERE (Addresses.City = '{0}' AND ShopTypes.Name = '{1}')", city, type);
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
        }

        private int GetAddressId(string city, string street, string building)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "HandleShopAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = cnn;

                    SqlParameter CityPar = new SqlParameter("@City", SqlDbType.VarChar);
                    CityPar.Value = city;
                    command.Parameters.Add(CityPar);

                    SqlParameter StreetPar = new SqlParameter("@Street", SqlDbType.VarChar);
                    StreetPar.Value = street;
                    command.Parameters.Add(StreetPar);

                    SqlParameter BuildingPar = new SqlParameter("@Building", SqlDbType.VarChar);
                    BuildingPar.Value = building;
                    command.Parameters.Add(BuildingPar);

                    SqlParameter Index = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    Index.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(Index);

                    command.ExecuteNonQuery();

                    return (int)Index.Value;
                }
            }
        }

        private int GetShopTypeId(string typeName)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "HandleShopType";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = cnn;

                    SqlParameter Name = new SqlParameter("@Name", SqlDbType.VarChar);
                    Name.Value = typeName;
                    command.Parameters.Add(Name);

                    SqlParameter Index = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    Index.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(Index);

                    command.ExecuteNonQuery();

                    return (int)Index.Value;
                }
            }
        }

        public void AddShop(Shop shop)
        {
            int addressId = GetAddressId(shop.Address_City, shop.Address_Street, shop.Address_Building);
            int typeId = GetShopTypeId(shop.Type);

            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand()) // add shop type if needed
                {
                    command.CommandText = string.Format("INSERT INTO Shops(Name, TypeId, AddressId) VALUES('{0}', {1}, {2})", shop.Name, typeId, addressId);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteShop(int index)
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = string.Format("DELETE FROM Shops WHERE Id = {0};", index);
                    command.Connection = cnn;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ClearAddresses()
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "DELETE FROM Addresses WHERE NOT EXISTS (SELECT * FROM Shops WHERE Shops.AddressId = Addresses.Id);";
                    command.Connection = cnn;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ClearShopTypes()
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "DELETE FROM ShopTypes WHERE NOT EXISTS (SELECT * FROM Shops WHERE Shops.TypeId = ShopTypes.Id);";
                    command.Connection = cnn;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
