using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Weapons.Ammunitions
{
    // Это класс для боеприпасов
    public abstract class Ammunition(string name, int bonusDamage)
    {
        public string Name { get; } = name;
        public int BonusDamage { get; } = bonusDamage;

        public virtual int ModifyDamage(int baseDamage)
        {
            return baseDamage + BonusDamage;
        }
    }
}
