using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class ShopBusinessLogic : IShopBusinessLogic
    {
        private IShopAccess shopAccess;

        public ShopBusinessLogic(IShopAccess shopAccess)
        {
            this.shopAccess = shopAccess;
        }

        public List<Shop> GetAllShops()
        {
            return shopAccess.GetAllShops();
        }

        public string GetShopRatingByIndex(int index)
        {
            return shopAccess.GetShopRatingByIndex(index);
        }

        public string GetShopRatingByName(string shopName)
        {
            return shopAccess.GetShopRatingByName(shopName);
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
