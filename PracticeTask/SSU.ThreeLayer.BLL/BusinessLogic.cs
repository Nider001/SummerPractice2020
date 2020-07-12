using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private IShopAccess shopAccess;
        private IUserAccess userAccess;

        public BusinessLogic(IShopAccess shopAccess, IUserAccess userAccess)
        {
            this.shopAccess = shopAccess;
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

        public List<Shop> GetAllShops()
        {
            return shopAccess.GetAllShops();
        }

        public List<User> GetAllUsers()
        {
            return userAccess.GetAllUsers();
        }

        public string GetShopRatingByIndex(int index)
        {
            return shopAccess.GetShopRatingByIndex(index);
        }

        public string GetShopRatingByName(string shopName)
        {
            return shopAccess.GetShopRatingByName(shopName);
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

        public Shop GetShop(int index)
        {
            return shopAccess.GetShop(index);
        }

        public List<Shop> FindShopsByName(string shopName)
        {
            return shopAccess.FindShopsByName(shopName);
        }

        public List<Shop> FindShopsByCity(string city)
        {
            return shopAccess.FindShopsByCity(city);
        }

        public List<Shop> FindShopsByCityAndType(string city, string type)
        {
            return shopAccess.FindShopsByCityAndType(city, type);
        }

        public void RateShop(int shopId, int rating)
        {
            userAccess.RateShop(shopId, rating);
        }

        public void AddShop(Shop shop)
        {
            shopAccess.AddShop(shop);
        }

        public void DeleteShop(int index)
        {
            shopAccess.DeleteShop(index);
        }

        public void ClearAddresses()
        {
            shopAccess.ClearAddresses();
        }

        public void ClearShopTypes()
        {
            shopAccess.ClearShopTypes();
        }
    }
}
