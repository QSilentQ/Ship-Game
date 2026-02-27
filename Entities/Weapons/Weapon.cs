using Ships.Entities.Items;
using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Weapons
{
    // Это класс для орудия
    public class Weapon(string name, int minDamage, int maxDamage, int cooldown, double weight) : Item(name, ItemType.Weapon, weight)
    {
        public int MinDamage { get; } = minDamage;
        public int MaxDamage { get; } = maxDamage;
        public int Cooldown { get; } = cooldown;

        public int CurrentCooldown = 0;

        public Ammunition? LoadedAmmo { get; set; }

        public bool IsCooldown()
        {
            return CurrentCooldown > 0;
        }

        public virtual bool CanLoadAmmo(Ammunition ammo)
        {
            if (ammo is Torpedoes) return false;
            return true;
        }

        public (int damage, Ammunition? ammo) Shoot(Ammunition ammo)
        {
            if (IsCooldown()) return (0, null);

            int baseDamage = new Random().Next(MinDamage, MaxDamage + 1);

            int totalDamage = ammo != null ? ammo.ModifyDamage(baseDamage) : baseDamage;

            CurrentCooldown = Cooldown;

            return (totalDamage, ammo);
        }

        public void ReduceCoolDown()
        {
            if (CurrentCooldown > 0) CurrentCooldown--;
        }
    }
}
