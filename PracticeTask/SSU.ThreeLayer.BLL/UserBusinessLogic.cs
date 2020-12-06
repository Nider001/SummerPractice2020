using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private IUserAccess userAccess;
        private IDataValidator dataValidator;

        public UserBusinessLogic(IUserAccess userAccess, IDataValidator dataValidator)
        {
            this.userAccess = userAccess;
            this.dataValidator = dataValidator;
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
            string v = dataValidator.AddUserValidator(user);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.AddUser(user, GetUserPasswordHashStr(user));
        }

        public void ChangeCurrentUserDateOfBirth(System.DateTime newDate)
        {
            string v = dataValidator.ChangeCurrentUserDateOfBirthValidator(newDate);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.ChangeCurrentUserDateOfBirth(newDate);
        }

        public void ChangeCurrentUserInfo(string newInfo)
        {
            string v = dataValidator.ChangeCurrentUserInfoValidator(newInfo);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.ChangeCurrentUserInfo(newInfo);
        }

        public void ChangeCurrentUserName(string newName)
        {
            string v = dataValidator.ChangeCurrentUserNameValidator(newName);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.ChangeCurrentUserName(newName);
        }

        public void ChangeCurrentUserPassword(string newPassword)
        {
            string v = dataValidator.ChangeCurrentUserPasswordValidator(newPassword);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.ChangeCurrentUserPassword(newPassword, GetUserPasswordHashStr(new User(newPassword)));
        }

        public void DeleteUser(int index)
        {
            string v = dataValidator.DeleteUserValidator(index);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.DeleteUser(index);
        }

        public List<User> GetAllUsers()
        {
            return userAccess.GetAllUsers();
        }

        public bool LogIn(string login, string password)
        {
            string v = dataValidator.LogInValidator(login, password);
            if (v.Length != 0) throw new FormatException(v);

            return userAccess.LogIn(login, password);
        }

        public User GetUser(int index)
        {
            string v = dataValidator.GetUserValidator(index);
            if (v.Length != 0) throw new FormatException(v);

            return userAccess.GetUser(index);
        }

        public User GetUser(string login)
        {
            string v = dataValidator.GetUserValidator(login);
            if (v.Length != 0) throw new FormatException(v);

            return userAccess.GetUser(login);
        }

        public void RateShop(int shopId, int rating)
        {
            string v = dataValidator.RateShopValidator(shopId, rating);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.RateShop(shopId, rating);
        }

        public void RateShop(int shopId, int rating, int userId)
        {
            string v = dataValidator.RateShopValidator(shopId, rating);
            if (v.Length != 0) throw new FormatException(v);

            userAccess.RateShop(shopId, rating, userId);
        }
    }
}
