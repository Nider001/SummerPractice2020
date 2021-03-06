﻿using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSU.ThreeLayer.DAL
{
    public interface IShopAccess
    {
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
