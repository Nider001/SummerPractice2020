using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private IDataAccess dataAccess;

        public BusinessLogic(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public User GetCurrentUser()
        {
            return dataAccess.GetCurrentUser();
        }

        public void AddUser(User user)
        {
            dataAccess.AddUser(user);
        }

        public void ChangeCurrentUserDateOfBirth(System.DateTime newDate)
        {
            dataAccess.ChangeCurrentUserDateOfBirth(newDate);
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            dataAccess.ChangeCurrentUserInfo(newInfo);
        }

        public void ChangeCurrentUserName(string newName)
        {
            dataAccess.ChangeCurrentUserName(newName);
        }

        public void ChangeCurrentUserPassword(string newPassword)
        {
            dataAccess.ChangeCurrentUserPassword(newPassword);
        }

        public void DeleteUser(int index)
        {
            dataAccess.DeleteUser(index);
        }

        public List<Shop> GetAllShops()
        {
            return dataAccess.GetAllShops();
        }

        public List<User> GetAllUsers()
        {
            return dataAccess.GetAllUsers();
        }

        public string GetShopRatingByIndex(int index)
        {
            return dataAccess.GetShopRatingByIndex(index);
        }

        public string GetShopRatingByName(string shopName)
        {
            return dataAccess.GetShopRatingByName(shopName);
        }

        public bool LogIn(string login, string password)
        {
            return dataAccess.LogIn(login, password);
        }

        public User GetUser(int index)
        {
            return dataAccess.GetUser(index);
        }

        public User GetUser(string login)
        {
            return dataAccess.GetUser(login);
        }

        public Shop GetShop(int index)
        {
            return dataAccess.GetShop(index);
        }

        public List<Shop> FindShopsByName(string shopName)
        {
            return dataAccess.FindShopsByName(shopName);
        }

        public List<Shop> FindShopsByCity(string city)
        {
            return dataAccess.FindShopsByCity(city);
        }

        public List<Shop> FindShopsByCityAndType(string city, string type)
        {
            return dataAccess.FindShopsByCityAndType(city, type);
        }

        public void RateShop(int shopId, int rating)
        {
            dataAccess.RateShop(shopId, rating);
        }
    }
}
