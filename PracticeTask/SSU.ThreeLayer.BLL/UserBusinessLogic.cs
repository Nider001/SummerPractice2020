using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private IUserAccess userAccess;

        public UserBusinessLogic(IUserAccess userAccess)
        {
            this.userAccess = userAccess;
        }

        private string GetUserPasswordHashStr(User user)
        {
            if (user.IsPasswordKnown)
            {
                return System.BitConverter.ToString(System.BitConverter.GetBytes(user.Password.GetHashCode())).Replace("-", "").ToLower();
            }
            else
            {
                throw new System.NullReferenceException("The password is stored in protected form and therefore cannot be identified.");
            }
        }

        public User GetCurrentUser()
        {
            return userAccess.GetCurrentUser();
        }

        public void AddUser(User user)
        {
            userAccess.AddUser(user, GetUserPasswordHashStr(user));
        }

        public void ChangeCurrentUserDateOfBirth(System.DateTime newDate)
        {
            userAccess.ChangeCurrentUserDateOfBirth(newDate);
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            userAccess.ChangeCurrentUserInfo(newInfo);
        }

        public void ChangeCurrentUserName(string newName)
        {
            userAccess.ChangeCurrentUserName(newName);
        }

        public void ChangeCurrentUserPassword(string newPassword)
        {
            userAccess.ChangeCurrentUserPassword(newPassword, GetUserPasswordHashStr(new User(newPassword)));
        }

        public void DeleteUser(int index)
        {
            userAccess.DeleteUser(index);
        }

        public List<User> GetAllUsers()
        {
            return userAccess.GetAllUsers();
        }

        public bool LogIn(string login, string password)
        {
            return userAccess.LogIn(login, password);
        }

        public User GetUser(int index)
        {
            return userAccess.GetUser(index);
        }

        public User GetUser(string login)
        {
            return userAccess.GetUser(login);
        }

        public void RateShop(int shopId, int rating)
        {
            userAccess.RateShop(shopId, rating);
        }
    }
}
