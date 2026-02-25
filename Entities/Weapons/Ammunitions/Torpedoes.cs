using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Weapons.Ammunitions
{
    internal class Torpedoes() : Ammunition("Торпеды", 40, 3)
    {
        public override int ModifyDamage(int baseDamage)
        {
            return baseDamage + BonusDamage;
        }
    }
}
