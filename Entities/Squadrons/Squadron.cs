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

        public bool IsAlive() {
            return Ships.Exists(ship => ship.IsAlive());
        }

        public void AddShip(Ship ship)
        {
            Ships.Add(ship);
        }
    }
}
