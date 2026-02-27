using Ships.Entities.Items;
using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Armors
{
    // Это класс для брони
    public class Armor(string name, int protectionPercent, double weight) : Item(name, ItemType.Armor, weight)
    {
        public int ProtectionPercent { get; set; } = protectionPercent;

        public virtual int ReduceDamage(int damage, Ammunition? ammo)
        {
            return damage - (damage * ProtectionPercent / 100);
        }
    }
}
