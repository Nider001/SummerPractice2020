using System.Collections;
using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.DAL
{
    public interface IUserAccess
    {
        User GetCurrentUser();

        void ChangeCurrentUserPassword(string newPassword, string passwordHashStr);
        void ChangeCurrentUserName(string newName);
        void ChangeCurrentUserInfo(string newInfo);
        void ChangeCurrentUserDateOfBirth(System.DateTime newDate);

        bool LogIn(string login, string password);

        void AddUser(User user, string passwordHashStr);
        void DeleteUser(int index);

        List<User> GetAllUsers();

        void RateShop(int shopId, int rating);

        User GetUser(int index);
        User GetUser(string login);
    }
}
