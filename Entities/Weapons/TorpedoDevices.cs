using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Weapons
{
    internal class TorpedoDevices() : Weapon("Торпедные аппараты", 35, 45, 1, 90)
    {
        public override bool CanLoadAmmo(Ammunition ammo)
        {
            return ammo is Torpedoes;
        }
    }
}
