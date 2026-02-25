using Ships.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Ships
{
    public class Destroyer(string name) : Ship(name, 400, 15, 300)
    {
        public override bool CanEquipWeapon(Weapon weapon) => true;
    }
}
