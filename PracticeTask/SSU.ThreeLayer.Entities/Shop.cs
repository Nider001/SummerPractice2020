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

        public Shop(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public Shop(int id, string name, string type) : this (name, type)
        {
            Id = id;
        }

        public string GetStringToShow()
        {
            return "Shop № " + Id + Environment.NewLine + "   Name: " + Name + Environment.NewLine + "   Type: " + Type;
        }
    }
}
