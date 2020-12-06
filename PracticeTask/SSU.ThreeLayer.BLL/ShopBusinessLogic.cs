using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;

namespace SSU.ThreeLayer.BLL
{
    public class ShopBusinessLogic : IShopBusinessLogic
    {
        private IShopAccess shopAccess;
        private IDataValidator dataValidator;

        public ShopBusinessLogic(IShopAccess shopAccess, IDataValidator dataValidator)
        {
            this.shopAccess = shopAccess;
            this.dataValidator = dataValidator;
        }

        public int GetMinRating()
        {
            return dataValidator.MinRatingValue();
        }

        public int GetMaxRating()
        {
            return dataValidator.MaxRatingValue();
        }

        public List<Shop> GetAllShops()
        {
            return shopAccess.GetAllShops();
        }

        public string GetShopRatingByIndex(int index)
        {
            string v = dataValidator.GetShopRatingByIndexValidator(index);
            if (v.Length != 0) throw new FormatException(v);

            return shopAccess.GetShopRatingByIndex(index);
        }

        public string GetShopRatingByName(string shopName)
        {
            string v = dataValidator.GetShopRatingByNameValidator(shopName);
            if (v.Length != 0) throw new FormatException(v);

            return shopAccess.GetShopRatingByName(shopName);
        }

        public Shop GetShop(int index)
        {
            string v = dataValidator.GetShopValidator(index);
            if (v.Length != 0) throw new FormatException(v);

            return shopAccess.GetShop(index);
        }

        public List<Shop> FindShopsByName(string shopName)
        {
            string v = dataValidator.FindShopsByNameValidator(shopName);
            if (v.Length != 0) throw new FormatException(v);

            return shopAccess.FindShopsByName(shopName);
        }

        public List<Shop> FindShopsByCity(string city)
        {
            string v = dataValidator.FindShopsByCityValidator(city);
            if (v.Length != 0) throw new FormatException(v);

            return shopAccess.FindShopsByCity(city);
        }

        public List<Shop> FindShopsByCityAndType(string city, string type)
        {
            string v = dataValidator.FindShopsByCityAndTypeValidator(city, type);
            if (v.Length != 0) throw new FormatException(v);

            return shopAccess.FindShopsByCityAndType(city, type);
        }

        public void AddShop(Shop shop)
        {
            string v = dataValidator.AddShopValidator(shop);
            if (v.Length != 0) throw new FormatException(v);

            shopAccess.AddShop(shop);
        }

        public void DeleteShop(int index)
        {
            string v = dataValidator.DeleteShopValidator(index);
            if (v.Length != 0) throw new FormatException(v);

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
