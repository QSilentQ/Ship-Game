using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Items
{
    public enum ItemType { Weapon, Armor, Ammunition }

    public abstract class Item(string name, ItemType type, double weight)
    {
        public string Name { get; set; } = name;
        public ItemType Type { get; set; } = type;
        public double Weight { get; set; } = weight;
    }
}
