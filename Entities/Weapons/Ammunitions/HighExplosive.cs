using Ships.Entities.Armors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Weapons.Ammunitions
{
    internal class HighExplosive() : Ammunition("Фугасные", 15)
    {
        public override int ModifyDamage(int baseDamage)
        {
            return baseDamage + 15;
        }

        public int GetBonusDamage(Armor? targetArmor)
        {
            if (targetArmor is CasemateArmor) return 50;
            return 0;
        }
    }
}
