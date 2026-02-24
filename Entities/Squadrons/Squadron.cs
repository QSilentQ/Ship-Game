using Ships.Entities.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Squadrons
{
    public class Squadron(string name)
    {
        public string Name { get; } = name;
        public List<Ship> Ships { get; } = [];

        public string CurrentTactic { get; set; } = "Концентрация";

        public bool IsAlive() {
            return Ships.Exists(ship => ship.IsAlive());
        }

        public void AddShip(Ship ship)
        {
            Ships.Add(ship);
        }

        public void Attack(List<Squadron> enemySquadron)
        {
            var allEnenemies = enemySquadron.SelectMany(squadron => squadron.Ships).Where(ship => ship.IsAlive()).ToList();
            if (allEnenemies.Count == 0) return;

            foreach (var ship in Ships.Where(ship => ship.IsAlive()))
            {
                var target = allEnenemies[new Random().Next(allEnenemies.Count)];
                ship.DealDamage(target);
            }
        }
    }
}
