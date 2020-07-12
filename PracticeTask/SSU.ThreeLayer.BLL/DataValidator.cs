using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SSU.ThreeLayer.BLL
{
    public class DataValidator : IDataValidator
    {
        #region VALUELIMITS
        public int Users_Login_MaxLength { get; } = 255;
        public int Users_Name_MaxLength { get; } = 255;
        public int Users_Info_MaxLength { get; } = 2550;
        public int Users_Password_MinLength { get; } = User.PasswordMinLength;

        public int Ratings_Rating_MinValue { get; } = Shop.MinRating;
        public int Ratings_Rating_MaxValue { get; } = Shop.MaxRating;

        public int Shops_Name_MaxLength { get; } = 255;

        public int ShopTypes_Name_MaxLength { get; } = 255;

        public int Addresses_City_MaxLength { get; } = 255;
        public int Addresses_Street_MaxLength { get; } = 255;
        public int Addresses_Building_MaxLength { get; } = 10;
        #endregion

        #region FORMATVALIDATORS
        public string Users_Login_Format { get; } = @"(\w|\d)+";
        public string Users_Name_Format { get; } = @"(\w|\d)+";
        //public string Users_Info_Format { get; } = @"(\w|\d| |\.|,)+";
        public string Users_Password_Format { get; } = @"(\S)+";

        public string Shops_Name_Format { get; } = @"(\w|\d| |\.|,)+";

        public string ShopTypes_Name_Format { get; } = @"(\w|\d| |\.|,)+";

        public string Addresses_City_Format { get; } = @"(\w|.| |)+";
        public string Addresses_Street_Format { get; } = @"(\w|.| |)+";
        public string Addresses_Building_Format { get; } = @"\d+\w*";
        #endregion

        private string separatorStr = " ";
        private string outOfRangeStr = " - invalid or out of range values";
        private string appendStr = "Error: ";

        #region INDIVIDUALVALIDATORS
        public bool UsersLoginValidate(string login)
        {
            return login.Length <= Users_Login_MaxLength && Regex.IsMatch(login, Users_Login_Format);
        }

        public bool UsersNameValidate(string name)
        {
            return name.Length <= Users_Name_MaxLength && Regex.IsMatch(name, Users_Name_Format);
        }

        public bool UsersInfoValidate(string info)
        {
            return info.Length <= Users_Info_MaxLength /*&& Regex.IsMatch(login, Users_Info_Format)*/;
        }

        public bool UsersPasswordValidate(string password)
        {
            return password.Length >= Users_Password_MinLength && Regex.IsMatch(password, Users_Password_Format);
        }

        public bool RatingsRatingValidate(int rating)
        {
            return rating >= Ratings_Rating_MinValue && rating <= Ratings_Rating_MaxValue;
        }
        
        public bool ShopsNameValidate(string name)
        {
            return name.Length <= Shops_Name_MaxLength && Regex.IsMatch(name, Shops_Name_Format);
        }

        public bool ShopTypesNameValidate(string name)
        {
            return name.Length <= ShopTypes_Name_MaxLength && Regex.IsMatch(name, ShopTypes_Name_Format);
        }

        public bool AddressesCityValidate(string city)
        {
            return city.Length <= Addresses_City_MaxLength && Regex.IsMatch(city, Addresses_City_Format);
        }

        public bool AddressesStreetValidate(string street)
        {
            return street.Length <= Addresses_Street_MaxLength && Regex.IsMatch(street, Addresses_Street_Format);
        }

        public bool AddressesBuildingValidate(string building)
        {
            return building.Length <= Addresses_Building_MaxLength && Regex.IsMatch(building, Addresses_Building_Format);
        }
        #endregion

        public string AddShopValidator(Shop shop)
        {
            string result = "";

            if (!ShopsNameValidate(shop.Name)) result += separatorStr + "name";

            if (!ShopTypesNameValidate(shop.Type)) result += separatorStr + "type";

            if (!AddressesCityValidate(shop.Address_City)) result += separatorStr + "city";

            if (!AddressesStreetValidate(shop.Address_Street)) result += separatorStr + "street";

            if (!AddressesBuildingValidate(shop.Address_Building)) result += separatorStr + "building";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string AddUserValidator(User user)
        {
            string result = "";

            if (!UsersLoginValidate(user.Login)) result += separatorStr + "login";

            if (!UsersNameValidate(user.Name)) result += separatorStr + "name";

            if (!UsersInfoValidate(user.Info)) result += separatorStr + "info";

            if (!UsersNameValidate(user.Password)) result += separatorStr + "password";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string ChangeCurrentUserDateOfBirthValidator(DateTime newDate)
        {
            return ""; // all good
        }

        public string ChangeCurrentUserInfoValidator(string newInfo)
        {
            string result = "";

            if (!UsersInfoValidate(newInfo)) result += separatorStr + "info";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string ChangeCurrentUserNameValidator(string newName)
        {
            string result = "";

            if (!UsersNameValidate(newName)) result += separatorStr + "name";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string ChangeCurrentUserPasswordValidator(string newPassword)
        {
            string result = "";

            if (!UsersInfoValidate(newPassword)) result += separatorStr + "password";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string DeleteShopValidator(int index)
        {
            return "";
        }

        public string DeleteUserValidator(int index)
        {
            return "";
        }

        public string FindShopsByCityAndTypeValidator(string city, string type)
        {
            string result = "";

            if (!AddressesCityValidate(city)) result += separatorStr + "city";

            if (!ShopTypesNameValidate(type)) result += separatorStr + "type";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string FindShopsByCityValidator(string city)
        {
            string result = "";

            if (!AddressesCityValidate(city)) result += separatorStr + "city";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string FindShopsByNameValidator(string shopName)
        {
            string result = "";

            if (!ShopsNameValidate(shopName)) result += separatorStr + "name";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string GetShopRatingByIndexValidator(int index)
        {
            return "";
        }

        public string GetShopRatingByNameValidator(string shopName)
        {
            string result = "";

            if (!ShopsNameValidate(shopName)) result += separatorStr + "name";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string GetShopValidator(int index)
        {
            return "";
        }

        public string GetUserValidator(int index)
        {
            return "";
        }

        public string GetUserValidator(string login)
        {
            string result = "";

            if (!UsersLoginValidate(login)) result += separatorStr + "login";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string LogInValidator(string login, string password)
        {
            string result = "";

            if (!UsersLoginValidate(login)) result += separatorStr + "login";

            if (!UsersPasswordValidate(password)) result += separatorStr + "password";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string RateShopValidator(int shopId, int rating)
        {
            string result = "";

            if (!RatingsRatingValidate(rating)) result += separatorStr + "rating";

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }
    }
}
