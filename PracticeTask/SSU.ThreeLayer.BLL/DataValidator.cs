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

        public int Ratings_Rating_MinValue { get; } = 1;
        public int Ratings_Rating_MaxValue { get; } = 5;

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
        private string outOfRangeStr = "";
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

        public int MinRatingValue()
        {
            return Ratings_Rating_MinValue;
        }

        public int MaxRatingValue()
        {
            return Ratings_Rating_MaxValue;
        }

        public string AddShopValidator(Shop shop)
        {
            string result = "";

            if (!ShopsNameValidate(shop.Name)) result += separatorStr + string.Format("name (out of max ({0}) range or invalid syntax)", Shops_Name_MaxLength);

            if (!ShopTypesNameValidate(shop.Type)) result += separatorStr + string.Format("type (out of max ({0}) range or invalid syntax)", ShopTypes_Name_MaxLength);

            if (!AddressesCityValidate(shop.AddressCity)) result += separatorStr + string.Format("city (out of max ({0}) range or invalid syntax)", Addresses_City_MaxLength);

            if (!AddressesStreetValidate(shop.AddressStreet)) result += separatorStr + string.Format("street (out of max ({0}) range or invalid syntax)", Addresses_Street_MaxLength);

            if (!AddressesBuildingValidate(shop.AddressBuilding)) result += separatorStr + string.Format("building (out of max ({0}) range or invalid syntax)", Addresses_Building_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string AddUserValidator(User user)
        {
            string result = "";

            if (!UsersLoginValidate(user.Login)) result += separatorStr + string.Format("login (out of max ({0}) range or invalid syntax)", Users_Login_MaxLength);

            if (!UsersNameValidate(user.Name)) result += separatorStr + string.Format("name (out of max ({0}) range or invalid syntax)", Users_Name_MaxLength);

            if (!UsersInfoValidate(user.Info)) result += separatorStr + string.Format("info (out of max ({0}) range)", Users_Info_MaxLength);

            if (!UsersNameValidate(user.Password)) result += separatorStr + string.Format("password (below min ({0}) range or invalid syntax)", Users_Password_MinLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string ChangeCurrentUserDateOfBirthValidator(DateTime newDate)
        {
            return ""; // all good
        }

        public string ChangeCurrentUserInfoValidator(string newInfo)
        {
            string result = "";

            if (!UsersInfoValidate(newInfo)) result += separatorStr + string.Format("info (out of max ({0}) range)", Users_Info_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string ChangeCurrentUserNameValidator(string newName)
        {
            string result = "";

            if (!UsersNameValidate(newName)) result += separatorStr + string.Format("name (out of max ({0}) range or invalid syntax)", Users_Name_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string ChangeCurrentUserPasswordValidator(string newPassword)
        {
            string result = "";

            if (!UsersInfoValidate(newPassword)) result += separatorStr + string.Format("password (below min ({0}) range or invalid syntax)", Users_Password_MinLength);

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

            if (!AddressesCityValidate(city)) result += separatorStr + string.Format("city (out of max ({0}) range or invalid syntax)", Addresses_City_MaxLength);

            if (!ShopTypesNameValidate(type)) result += separatorStr + string.Format("type (out of max ({0}) range or invalid syntax)", ShopTypes_Name_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string FindShopsByCityValidator(string city)
        {
            string result = "";

            if (!AddressesCityValidate(city)) result += separatorStr + string.Format("city (out of max ({0}) range or invalid syntax)", Addresses_City_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string FindShopsByNameValidator(string shopName)
        {
            string result = "";

            if (!ShopsNameValidate(shopName)) result += separatorStr + string.Format("name (out of max ({0}) range or invalid syntax)", Shops_Name_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string GetShopRatingByIndexValidator(int index)
        {
            return "";
        }

        public string GetShopRatingByNameValidator(string shopName)
        {
            string result = "";

            if (!ShopsNameValidate(shopName)) result += separatorStr + string.Format("name (out of max ({0}) range or invalid syntax)", Shops_Name_MaxLength);

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

            if (!UsersLoginValidate(login)) result += separatorStr + string.Format("login (out of max ({0}) range or invalid syntax)", Users_Login_MaxLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string LogInValidator(string login, string password)
        {
            string result = "";

            if (!UsersLoginValidate(login)) result += separatorStr + string.Format("login (out of max ({0}) range or invalid syntax)", Users_Login_MaxLength);

            if (!UsersPasswordValidate(password)) result += separatorStr + string.Format("password (below min ({0}) range or invalid syntax)", Users_Password_MinLength);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }

        public string RateShopValidator(int shopId, int rating)
        {
            string result = "";

            if (!RatingsRatingValidate(rating)) result += separatorStr + string.Format("rating (out of range ({0}-{1}) or invalid syntax)", Ratings_Rating_MinValue, Ratings_Rating_MaxValue);

            return result.Length != 0 ? appendStr + result + outOfRangeStr : result;
        }
    }
}
