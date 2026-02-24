using Ships.Entities.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Services
{
    public static class TacticsService
    {
        private static Ship? concentrationTarget;

        public static Ship? GetTarget(int tacticId, Ship attacker, List<Ship> enemies)
        {
            if (enemies.Count == 0) return null;

            return tacticId switch
            {
                1 => enemies[0],
                2 => enemies.OrderByDescending(e => e.MaxHeatPoints).First(),
                3 => enemies.OrderBy(e => e.CurrentHeatPoints).First(),
                4 => GetConcentrationTarget(enemies),
                5 => GetPriorityTarget(attacker, enemies),
                _ => enemies[new Random().Next(enemies.Count)]
            };
        }

        private static Ship GetConcentrationTarget(List<Ship> enemies)
        {
            if (concentrationTarget == null || !concentrationTarget.IsAlive() || !enemies.Contains(concentrationTarget))
            {
                concentrationTarget = enemies[new Random().Next(enemies.Count)];
            }
            return concentrationTarget;
        }

        private static Ship GetPriorityTarget(Ship attacker, List<Ship> enemies)
        {
            Ship? priority = attacker switch
            {
                Destroyer => enemies.FirstOrDefault(e => e is Battleship),
                Cruiser => enemies.FirstOrDefault(e => e is Destroyer),
                Battleship => enemies.FirstOrDefault(e => e is Cruiser),
                _ => null
            };

            return priority ?? enemies[new Random().Next(enemies.Count)];
        }
    }
}
