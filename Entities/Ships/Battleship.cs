using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Ships
{
    public class Battleship(string name) : Ship(name, 820, 0, 700)
    {
        public override void TakeDamage(int damage, Ammunition? ammo)
        {
            if (ammo is Torpedoes)
            {
                base.TakeDamage(damage, ammo);
                return;
            }

            Random random = new();
            if (random.Next(100) < 20)
            {
                if (MySquadron != null)
                {
                    var otherAllies = MySquadron.Ships
                        .Where(s => s.IsAlive() && s != this)
                        .ToList();

                    if (otherAllies.Count > 0)
                    {
                        Ship target = otherAllies[random.Next(otherAllies.Count)];
                        Console.WriteLine($"Рикошет! Броня {Name} не пробита.");
                        Console.WriteLine($"Снаряд попал в союзный {target.Name} на {damage} урона!");

                        target.TakeDamage(damage, ammo);
                    }
                    else
                    {
                        Console.WriteLine($"\n Рикошет! Но рядом никого нет.");
                        return;
                    }
                }
            }

            // Если рикошет не случился
            base.TakeDamage(damage, ammo);
        }
    }
}
