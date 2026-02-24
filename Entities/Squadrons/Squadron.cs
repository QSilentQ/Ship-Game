using Ships.Entities.Ships;
using Ships.Services;
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
            ship.MySquadron = this;
            Ships.Add(ship);
        }

        public void Attack(List<Squadron> allSquadrons, int tacticId)
        {
            var enemies = allSquadrons
                .Where(sq => sq != this)
                .SelectMany(sq => sq.Ships)
                .Where(s => s.IsAlive())
                .ToList();

            if (enemies.Count == 0) return;

            Ship? commanderTarget = enemies[new Random().Next(enemies.Count)];

            foreach (var ship in Ships.Where(s => s.IsAlive()))
            {
                Ship? target = (tacticId == 1) ? commanderTarget : TacticsService.GetTarget(tacticId, ship, enemies);

                if (target != null)
                {
                    ship.DealDamage(target);

                    if (!target.IsAlive())
                    {
                        enemies.Remove(target);
                        if (enemies.Count == 0) break;
                        commanderTarget = enemies.Count > 0 ? enemies[new Random().Next(enemies.Count)] : null;
                    }
                }
            }
        }
    }
}
