using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.BLL
{
    public interface IBusinessLogic
    {
        User GetCurrentUser();

        void AddUser(User user);
        void DeleteUser(int index);

        void ChangeCurrentUserPassword(string newPassword);
        void ChangeCurrentUserName(string newName);
        void ChangeCurrentUserInfo(string newInfo);
        void ChangeCurrentUserDateOfBirth(System.DateTime newDate);

        string GetShopRatingByName(string shopName);
        string GetShopRatingByIndex(int index);

        List<Shop> GetAllShops();
        List<User> GetAllUsers();

        List<Shop> FindShopsByName(string shopName);
        List<Shop> FindShopsByCity(string city);
        List<Shop> FindShopsByCityAndType(string city, string type);

        void RateShop(int shopId, int rating);

        bool LogIn(string login, string password);
        User GetUser(int index);
        User GetUser(string login);
        Shop GetShop(int index);

        void AddShop(Shop shop);
        void DeleteShop(int index);

        void ClearAddresses();
        void ClearShopTypes();
    }
}