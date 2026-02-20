using Ships.Entities.Armors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Ships
{
    // Это класс для кораблей
    public class Ship(string name, int heatPoints, int evasionChance)
    {
        public string Name { get; } = name;
        public int MaxHeatPoints { get; } = heatPoints;
        public int CurrentHeatPoints { get; protected set; } = heatPoints;
        public int EvasionChance { get; } = evasionChance;
        public Armor? Armor {  get; set; }

        public bool IsAlive()
        {
            return CurrentHeatPoints > 0;
        }

        public virtual void TakeDamage(int damage)
        {
            Random random = new();
            int chance = random.Next(100);

            if (chance < EvasionChance)
            {
                Console.WriteLine($"{Name} увернулся от атаки!");
            }

            CurrentHeatPoints -= damage;
            if (CurrentHeatPoints < 0)
            {
                CurrentHeatPoints = 0;
            }

            Console.WriteLine($"{Name} получил {damage} урона. HP: {CurrentHeatPoints}/{MaxHeatPoints}");
        }

        public virtual int DealDamage()
        {
            Random random = new();
            Console.Write($"{Name} наносит удар. ");
            return random.Next(20, 41);
        }
    }
}
