using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Armors
{
    public class AntiTorpedoArmor() : Armor("Противоторпедная броня", 15, 120)
    {
        public override int ReduceDamage(int damage, Ammunition? ammo)
        {

            if (ammo is Torpedoes)
            {
                Console.WriteLine($"Броня {Name} сработала против торпеды. Урон снижен на 50%.");
                return damage / 2;
            }

            if (ammo is ArmorPiercing)
            {
                Console.WriteLine($"{Name} полностью поглотила снаряд!");
                return 0;
            }

            return base.ReduceDamage(damage, ammo);
        }
    }
}
