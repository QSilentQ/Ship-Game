using Ships.Entities.Armors;
using Ships.Entities.Ships;
using Ships.Entities.Weapons;
using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Services
{
    public static class Warehouse
    {
        private static readonly Random random = new();

        public static Armor GetRandomArmor()
        {
            return random.Next(3) switch
            {
                0 => new ArmorBelt(),
                1 => new CasemateArmor(),
                _ => new AntiTorpedoArmor()
            };
        }

        public static Weapon GetRandomWeapon(Ship ship)
        {
            while (true)
            {
                Weapon weapon = random.Next(3) switch
                {
                    0 => new MainTowers(),
                    1 => new UniversalGuns(),
                    _ => new TorpedoDevices()
                };

                if (ship.CanEquipWeapon(weapon))
                {
                    return weapon;
                }
            }
        }

        public static Ammunition GetRandomAmmo(Weapon weapon)
        {
            while (true)
            {
                Ammunition ammo = random.Next(3) switch
                {
                    0 => new ArmorPiercing(),
                    1 => new HighExplosive(),
                    _ => new Torpedoes()
                };

                if (weapon.CanLoadAmmo(ammo))
                {
                    return ammo;
                }
            }
        }
    }
}
