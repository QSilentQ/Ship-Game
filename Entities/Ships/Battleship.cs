using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Ships
{
    public class Battleship(string name) : Ship(name, 820, 0)
    {
        public override void TakeDamage(int damage, Ammunition? ammo)
        {
            Random random = new();
            int chance = random.Next(100);

            if (chance < 20)
            {
                Console.WriteLine($"Рикошет! {Name} не получил урона!");
                return;
            }

            base.TakeDamage(damage, ammo);
        }
    }
}
