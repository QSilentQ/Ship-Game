using Ships.Entities.Armors;
using Ships.Entities.Ships;
using Ships.Entities.Squadrons;
using Ships.Entities.Weapons;
using Ships.Entities.Weapons.Ammunitions;
using Ships.Services;

namespace Ships
{
    internal class ShipGame
    {
        static void Main(string[] args)
        {
            string? userChoise;

            Console.WriteLine($"{Spaces(10)}Добро пожаловать в игру!");
            Console.WriteLine($"{Spaces(15)}Морские войны\n");
            Console.WriteLine($"{Spaces(19)}Меню\n");

            Console.WriteLine($"{Spaces(16)}1. Играть\n");
            Console.WriteLine($"{Spaces(16)}2. Выйти\n");

            Console.Write($"{Spaces(12)}Выберите действие: ");

            while (true)
            {
                userChoise = Console.ReadLine()?.Trim();

                if (userChoise == "1" || userChoise == "2") break;
                Console.Write($"{Spaces(2)}Неверный ввод. Пожалуйста, выберите 1 или 2: ");
            }

            switch (userChoise)
            {
                case "1":
                    Console.WriteLine($"{Spaces(13)}Вы выбрали: Играть\n");
                    break;  
                case "2":
                    Console.WriteLine($"{Spaces(13)}Вы выбрали: Выйти\n");
                    break;
            }

            Squadron firstSquadron = new("Первая команда");
            Squadron secondSquadron = new("Вторая команда");

            FillSquadron(firstSquadron, 3);
            Console.WriteLine();
            FillSquadron(secondSquadron, 3);

            Console.WriteLine($"\nБОЙ НАЧИНАЕТСЯ: {firstSquadron.Name} vs {secondSquadron.Name}!");
            Console.WriteLine("Нажмите любую кнопку, чтобы начать игру...");
            Console.ReadKey();

            int round = 1;

            while (firstSquadron.IsAlive() && secondSquadron.IsAlive())
            {
                Console.WriteLine($"\n--- РАУНД {round} ---\n");

                foreach (var ship in firstSquadron.Ships) ship.ProcessDelayedAttacks();
                foreach (var ship in secondSquadron.Ships) ship.ProcessDelayedAttacks();

                if (!firstSquadron.IsAlive() || !secondSquadron.IsAlive()) break;

                Console.WriteLine($"\n--- Ход эскадры: {firstSquadron.Name} ---\n");
                firstSquadron.Attack([secondSquadron]);

                if (!secondSquadron.IsAlive()) break;

                Console.WriteLine($"\n--- Ход эскадры: {secondSquadron.Name} ---\n");
                secondSquadron.Attack([firstSquadron]);

                UpdateCooldowns(firstSquadron);
                UpdateCooldowns(secondSquadron);

                round++;
                Console.WriteLine("\nКонец раунда. Нажмите любую кнопку...");
                Console.ReadKey();
            }

            Console.WriteLine("\n--- КОНЕЦ БОЯ ---");
            if (firstSquadron.IsAlive()) Console.WriteLine($"Победу одержала {firstSquadron.Name}");
            else if (secondSquadron.IsAlive()) Console.WriteLine($"Победу одержала {secondSquadron.Name}");
            else Console.WriteLine("НИЧЬЯ! ВСЕ КОРАБЛИ УНИЧТОЖЕНЫ");
        }

        static void FillSquadron(Squadron squadron, int count)
        {
            Random random = new();
            for (int i = 0; i < count; i++)
            {
                Ship ship = random.Next(3) switch
                {
                    0 => new Destroyer($"{squadron.Name}: Эсминец #{i+1}"),
                    1 => new Cruiser($"{squadron.Name}: Крейсер #{i + 1}"),
                    _ => new Battleship($"{squadron.Name}: Линкор #{i + 1}")
                };

                ship.Armor = Warehouse.GetRandomArmor();
                ship.Weapon = Warehouse.GetRandomWeapon(ship);
                ship.Weapon.LoadedAmmo = Warehouse.GetRandomAmmo(ship.Weapon);

                squadron.AddShip(ship);
                Console.WriteLine($"+ {ship.Name} добавлен в отряд. Орудие: {ship.Weapon.Name}, Снаряды: {ship.Weapon.LoadedAmmo.Name}.");
            }
        }

        static void UpdateCooldowns(Squadron squadron)
        {
            foreach (var ship in squadron.Ships) ship.Weapon?.ReduceCoolDown();
        }

        static string Spaces(int count)
        {
            return new string(' ', count);
        }
    }
}
