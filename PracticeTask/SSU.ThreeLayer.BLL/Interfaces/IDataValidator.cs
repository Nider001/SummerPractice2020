using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.BLL
{
    public interface IDataValidator
    {
        bool UsersLoginValidate(string login);
        bool UsersNameValidate(string name);
        bool UsersInfoValidate(string info);
        bool UsersPasswordValidate(string password);

        bool RatingsRatingValidate(int rating);

        bool ShopsNameValidate(string name);
        bool ShopTypesNameValidate(string name);

        bool AddressesCityValidate(string city);
        bool AddressesStreetValidate(string street);
        bool AddressesBuildingValidate(string building);

        //---------------------------------------------------------------------------------------------------

        string AddShopValidator(Shop shop);
        string DeleteShopValidator(int index);

        string GetShopRatingByNameValidator(string shopName);
        string GetShopRatingByIndexValidator(int index);

        string FindShopsByNameValidator(string shopName);
        string FindShopsByCityValidator(string city);
        string FindShopsByCityAndTypeValidator(string city, string type);

        string GetShopValidator(int index);

        string ChangeCurrentUserPasswordValidator(string newPassword);
        string ChangeCurrentUserNameValidator(string newName);
        string ChangeCurrentUserInfoValidator(string newInfo);
        string ChangeCurrentUserDateOfBirthValidator(System.DateTime newDate);

        string LogInValidator(string login, string password);

        string AddUserValidator(User user);
        string DeleteUserValidator(int index);

        string RateShopValidator(int shopId, int rating);

        string GetUserValidator(int index);
        string GetUserValidator(string login);
    }
}