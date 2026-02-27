using Ships.Entities.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Weapons.Ammunitions
{
    // Это класс для боеприпасов
    public abstract class Ammunition(string name, int bonusDamage, double weight) : Item(name, ItemType.Ammunition, weight)
    {
        public int BonusDamage { get; } = bonusDamage;

        public virtual int ModifyDamage(int baseDamage)
        {
            return baseDamage + BonusDamage;
        }
    }
}
