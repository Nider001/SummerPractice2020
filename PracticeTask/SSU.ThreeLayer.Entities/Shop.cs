using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSU.ThreeLayer.Entities
{
    public class Shop
    {
        public static int MaxRating { get; } = 5;

        public int Id { get; set; } = 0;

        public string Name { get; set; }

        public string Type { get; set; }

        public string Address_City { get; set; }
        public string Address_Street { get; set; }
        public string Address_Building { get; set; }

        public Shop(string name, string type, string city, string street, string building)
        {
            Name = name;
            Type = type;
            Address_City = city;
            Address_Street = street;
            Address_Building = building;
        }

        public Shop(int id, string name, string type, string city, string street, string building) : this (name, type, city, street, building)
        {
            Id = id;
        }

        public override string ToString()
        {
            string shopAddress = String.Format("city: '{0}', street: '{1}', nuilding: '{2}'", Address_City, Address_Street, Address_Building);
            return "Shop № " + Id + Environment.NewLine + "   Name: " + Name + Environment.NewLine + "   Type: " + Type + Environment.NewLine + "   Address: [" + shopAddress + "]";
        }
    }
}
