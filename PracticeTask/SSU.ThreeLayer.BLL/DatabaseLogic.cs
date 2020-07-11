using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class DatabaseLogic : IDatabaseLogic
    {
        private IBaseDatabase baseDatabase;

        public DatabaseLogic(IBaseDatabase baseDatabase)
        {
            this.baseDatabase = baseDatabase;
        }

        public User GetCurrentUser()
        {
            return baseDatabase.GetCurrentUser();
        }

        public void AddUser(User user)
        {
            baseDatabase.AddUser(user);
        }

        public void ChangeCurrentUserDateOfBirth(System.DateTime newDate)
        {
            baseDatabase.ChangeCurrentUserDateOfBirth(newDate);
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            baseDatabase.ChangeCurrentUserInfo(newInfo);
        }

        public void ChangeCurrentUserName(string newName)
        {
            baseDatabase.ChangeCurrentUserName(newName);
        }

        public void ChangeCurrentUserPassword(string newPassword)
        {
            baseDatabase.ChangeCurrentUserPassword(newPassword);
        }

        public void DeleteUser(int index)
        {
            baseDatabase.DeleteUser(index);
        }

        public List<Shop> GetAllShops()
        {
            return baseDatabase.GetAllShops();
        }

        public List<User> GetAllUsers()
        {
            return baseDatabase.GetAllUsers();
        }

        public float GetShopRatingByIndex(int index)
        {
            return baseDatabase.GetShopRatingByIndex(index);
        }

        public float GetShopRatingByName(string shopName)
        {
            return baseDatabase.GetShopRatingByName(shopName);
        }

        public bool LogIn(string login, string password)
        {
            return baseDatabase.LogIn(login, password);
        }

        public User GetUser(int index)
        {
            return baseDatabase.GetUser(index);
        }

        public User GetUser(string login)
        {
            return baseDatabase.GetUser(login);
        }

        public Shop GetShop(int index)
        {
            return baseDatabase.GetShop(index);
        }
    }
}
