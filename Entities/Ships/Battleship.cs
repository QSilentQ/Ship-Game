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
            if (random.Next(100) < 20 && MySquadron != null)
            {
                Console.WriteLine($"Рикошет! {Name} не получил урона!");

                var otherAllies = MySquadron.Ships.Where(s => s.IsAlive() && s != this).ToList();

                if (otherAllies.Count > 0 )
                {
                    var target = otherAllies[random.Next(otherAllies.Count)];
                    Console.WriteLine($"Снаряд отскочил прямо в союзный {target.Name}!");
                    target.TakeDamage(damage, ammo);
                    return;
                }
            }
            base.TakeDamage(damage, ammo);
        }
    }
}
