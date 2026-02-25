using Ships.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Ships
{
    public class Cruiser(string name) : Ship(name, 580, 7, 500)
    {
        public override bool CanEquipWeapon(Weapon weapon) => true;
    }
}
