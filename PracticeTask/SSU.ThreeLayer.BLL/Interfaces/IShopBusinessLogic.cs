using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.BLL
{
    public interface IShopBusinessLogic
    {
        int GetMinRating();
        int GetMaxRating();

        void AddShop(Shop shop);
        void DeleteShop(int index);

        void ClearAddresses();
        void ClearShopTypes();

        string GetShopRatingByName(string shopName);
        string GetShopRatingByIndex(int index);
        List<Shop> GetAllShops();

        List<Shop> FindShopsByName(string shopName);
        List<Shop> FindShopsByCity(string city);
        List<Shop> FindShopsByCityAndType(string city, string type);

        Shop GetShop(int index);
    }
}