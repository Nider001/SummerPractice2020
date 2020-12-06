using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSU.ThreeLayer.WebPublicLogic.Models
{
    public class ShopVM : Shop
    {
        public string Rating { get; set; }

        public ShopVM(string name, string type, string city, string street, string building, string rating) : base(name, type, city, street, building)
        {
            Rating = rating;
        }

        public ShopVM(int id, string name, string type, string city, string street, string building, string rating) : base(id, name, type, city, street, building)
        {
            Rating = rating;
        }

        public override string ToString()
        {
            string shopAddress = String.Format("city: '{0}', street: '{1}', nuilding: '{2}'", AddressCity, AddressStreet, AddressBuilding);
            return $"{Id} {Name} {Type} {shopAddress} {Rating}";
        }
    }
}