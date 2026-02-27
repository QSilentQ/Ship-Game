using Ships.Entities.Armors;
using Ships.Entities.Inventories;
using Ships.Entities.Squadrons;
using Ships.Entities.Weapons;
using Ships.Entities.Weapons.Ammunitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities.Ships
{
    // Это класс для кораблей
    public class Ship(string name, int heatPoints, int evasionChance, double allowableWeight)
    {
        public string Name { get; } = name;
        public int MaxHeatPoints { get; } = heatPoints;
        public int CurrentHeatPoints { get; protected set; } = heatPoints;
        public int EvasionChance { get; } = evasionChance;
        public double AllowableWeight { get; } = allowableWeight;

        public Inventory Inventory { get; set; } = new(allowableWeight);
        public Squadron? MySquadron { get; set; }

        private readonly List<(int damage, Ammunition ammo)> currentRoundHits = [];
        private readonly List<(int damage, Ammunition ammo)> delayedAttacks = [];

        public void RegisterHit(int damage, Ammunition loadedAmmo)
        {
            currentRoundHits.Add((damage, loadedAmmo));
            Console.WriteLine($"В {Name} летит {loadedAmmo.Name}");
        }

        public void ApplyRoundDamage()
        {
            if (currentRoundHits.Count == 0) return;

            foreach (var (damage, ammo) in currentRoundHits)
            {
                TakeDamage(damage, ammo);
            }
            currentRoundHits.Clear();
        }

        public void AddDelayedAttack(int damage, Ammunition ammo)
        {
            delayedAttacks.Add((damage, ammo));
            Console.WriteLine($"Торпеда выпущена в {Name}! Она достигнет цели в следующем ходу.");
        }

        public void ProcessDelayedAttacks()
        {
            if (delayedAttacks.Count == 0) return;
            Console.WriteLine($"\nТорпеды достигли {Name}!");

            var attacksToProcess = delayedAttacks.ToList();
            delayedAttacks.Clear();

            foreach ( var (damage, ammo) in attacksToProcess )
            {
                TakeDamage(damage, ammo);
            }
        }

        public bool IsAlive()
        {
            return CurrentHeatPoints > 0;
        }

        public virtual void TakeDamage(int damage, Ammunition ammo)
        {
            if (new Random().Next(100) < EvasionChance)
            {
                Console.WriteLine($"{Name} уклонился!");
                return;
            }

            if (Inventory.Armor != null)
            {
                if (ammo is HighExplosive && Inventory.Armor is CasemateArmor)
                {
                    damage = (int)(damage * 1.5);
                    Console.WriteLine("Попадание фугасом по казематной броне! (+50% урона)");
                }

                damage = Inventory.Armor.ReduceDamage(damage, ammo);
            }

            if (ammo is Torpedoes)
            {
                Console.WriteLine("Торпеда пробила броню! (Двойной урон)");
                damage *= 2;
            }

            CurrentHeatPoints -= damage;
            if (CurrentHeatPoints < 0)
            {
                CurrentHeatPoints = 0;
            }
            Console.WriteLine($"{Name} получил {damage} урона. HP: {CurrentHeatPoints}/{MaxHeatPoints}");
        }

        public virtual void DealDamage(Ship target)
        {
            if (Inventory.Weapon == null) return;

            if (Inventory.Weapon.IsCooldown())
            {
                Console.WriteLine($"{Name} на перезарядке...");
                return;
            }

            Ammunition? ammo = Inventory.GetAmmunition();
            if (ammo == null) return;

            var (damage, loadedAmmo) = Inventory.Weapon.Shoot(ammo);
            if (loadedAmmo == null) return;

            if (damage > 0)
            {
                if (loadedAmmo is Torpedoes)
                {
                    Console.Write($"{Name} стреляет. ");
                    target.AddDelayedAttack(damage, loadedAmmo);
                }
                else
                {
                    Console.WriteLine($"{Name} стреляет по {target.Name}!");
                    target.RegisterHit(damage, loadedAmmo);
                }
            }
        }

        public virtual bool CanEquipWeapon(Weapon weapon)
        {
            if (weapon is TorpedoDevices) return false;
            return true;
        }
    }
}
