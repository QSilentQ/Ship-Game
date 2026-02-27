using Ships.Entities.Armors;
using Ships.Entities.Items;
using Ships.Entities.Weapons;
using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Inventories
{
    public class Inventory(double allowableWeight)
    {
        private List<Item> Items { get; set; } = [];

        public Weapon? Weapon => Items.OfType<Weapon>().FirstOrDefault();
        public Armor? Armor => Items.OfType<Armor>().FirstOrDefault();
        public List<Ammunition> Ammunitions => Items.OfType<Ammunition>().ToList();

        public double AllowableWeight { get; set; } = allowableWeight;
        public double CurrentWeight => Items.Sum(item => item.Weight);

        public bool TryAdd(Item? item)
        {
            if (item == null) return false;

            if (CurrentWeight + item.Weight > AllowableWeight) return false;

            Items.Add(item);
            return true;
        }

        public bool Remove(Item item) 
        { 
            Items.Remove(item); 
            return true; 
        }

        public Ammunition? GetAmmunition()
        {
            if (Ammunitions.Count == 0) return null;

            Ammunition ammo = Ammunitions[new Random().Next(Ammunitions.Count)];
            //Items.Remove(ammo);
            return ammo;
        }
    }
}
