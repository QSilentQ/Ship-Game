using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Armors
{
    // Это класс для брони
    public class Armor(string name, int protectionPercent)
    {
        public string Name { get; set; } = name;
        public int ProtectionPercent { get; set; } = protectionPercent;

        public virtual int ReduceDamage(int damage)
        {
            int reducedDamage = damage - (damage * ProtectionPercent / 100);
            return reducedDamage;
        }
    }
}
