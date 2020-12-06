using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.BLL
{
    public interface IUserBusinessLogic
    {
        User GetCurrentUser();

        void ChangeCurrentUserPassword(string newPassword);
        void ChangeCurrentUserName(string newName);
        void ChangeCurrentUserInfo(string newInfo);
        void ChangeCurrentUserDateOfBirth(System.DateTime newDate);

        bool LogIn(string login, string password);

        void AddUser(User user);
        void DeleteUser(int index);

        List<User> GetAllUsers();

        void RateShop(int shopId, int rating);
        void RateShop(int shopId, int rating, int userId);

        User GetUser(int index);
        User GetUser(string login);
    }
}