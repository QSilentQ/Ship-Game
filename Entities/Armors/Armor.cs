using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Armors
{
    // Это класс для брони
    public class Armor(string name, int protectionPercent, double weight)
    {
        public string Name { get; set; } = name;
        public int ProtectionPercent { get; set; } = protectionPercent;
        public double Weight { get; set; } = weight;

        public virtual int ReduceDamage(int damage, Ammunition? ammo)
        {
            return damage - (damage * ProtectionPercent / 100);
        }
    }
}
