using System.Collections;
using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.DAL
{
    public interface IShopAccess
    {
        string GetShopRatingByName(string shopName);
        string GetShopRatingByIndex(int index);
        List<Shop> GetAllShops();

        List<Shop> FindShopsByName(string shopName);
        List<Shop> FindShopsByCity(string city);
        List<Shop> FindShopsByCityAndType(string city, string type);

        void RateShop(int shopId, int rating);
        
        Shop GetShop(int index);
    }
}
