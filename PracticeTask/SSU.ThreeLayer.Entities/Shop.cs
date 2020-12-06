using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSU.ThreeLayer.Entities
{
    public class Shop
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; }

        public string Type { get; set; }

        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressBuilding { get; set; }

        public Shop(string name, string type, string city, string street, string building)
        {
            Name = name;
            Type = type;
            AddressCity = city;
            AddressStreet = street;
            AddressBuilding = building;
        }

        public Shop(int id, string name, string type, string city, string street, string building) : this(name, type, city, street, building)
        {
            Id = id;
        }

        public override string ToString()
        {
            string shopAddress = String.Format("city: '{0}', street: '{1}', nuilding: '{2}'", AddressCity, AddressStreet, AddressBuilding);
            return "Shop № " + Id + Environment.NewLine + "   Name: " + Name + Environment.NewLine + "   Type: " + Type + Environment.NewLine + "   Address: [" + shopAddress + "]";
        }
    }
}
