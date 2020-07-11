using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.BLL
{
    public interface IDatabaseLogic
    {
        User GetCurrentUser();

        void AddUser(User user);
        void DeleteUser(int index);

        void ChangeCurrentUserPassword(string newPassword);
        void ChangeCurrentUserName(string newName);
        void ChangeCurrentUserInfo(string newInfo);
        void ChangeCurrentUserDateOfBirth(System.DateTime newDate);

        float GetShopRatingByName(string shopName);
        float GetShopRatingByIndex(int index);

        List<Shop> GetAllShops();
        List<User> GetAllUsers();

        bool LogIn(string login, string password);
        User GetUser(int index);
        User GetUser(string login);
        Shop GetShop(int index);
    }
}