using Ships.Entities.Armors;
using Ships.Entities.Ships;
using Ships.Entities.Squadrons;
using Ships.Entities.Weapons;
using Ships.Entities.Weapons.Ammunitions;
using Ships.Services;
using System.Formats.Asn1;

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
                Console.Write($"{Spaces(2)}Неверный ввод. Пожалуйста, попробуйте еще раз: ");
            }

            if (userChoise == "2")
            {
                Console.WriteLine($"{Spaces(13)}Вы выбрали: Выйти\n");
            }
            else
            {
                Console.WriteLine($"{Spaces(13)}Вы выбрали: Играть\n");

                int squadronCount;
                Console.Write("Введите количество эскадр (от 2 до 10): ");

                while (true)
                {
                    string? input = Console.ReadLine();

                    if (int.TryParse(input, out squadronCount) && squadronCount >= 2 && squadronCount <= 10)
                    {
                        break;
                    }

                    Console.Write("Ошибка! Пожалуйста, введите целое число от 2 до 10: ");
                }

                Console.WriteLine($"Создано эскадр: {squadronCount}");
                Console.WriteLine();

                Console.WriteLine("\nВыберите тактику для всех команд\n");
                Console.WriteLine("1. Приказ командира");
                Console.WriteLine("2. Охота на лидера");
                Console.WriteLine("3. Добивание");
                Console.WriteLine("4. Концентрация");
                Console.WriteLine("5. По приоритету типов\n");
                Console.Write("\nВаш выбор (от 1 до 5): ");

                int tacticId;

                while (true)
                {
                    string? input = Console.ReadLine();

                    if (int.TryParse(input, out tacticId) && tacticId >= 1 && tacticId <= 5)
                    {
                        break;
                    }

                    Console.Write("Неверный выбор. Пожалуйста, введите число от 1 до 5: ");
                }

                Console.WriteLine($"Выбрана тактика #{tacticId}");

                List<Squadron> allSquadrons = [];

                for (int i = 0; i < squadronCount; i++)
                {
                    Console.WriteLine($"\nВведите название для эскадры #{i + 1} (или Enter для автоназвания");
                    string? name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name)) name = $"Флот-{i + 1}";

                    Squadron squadron = new(name);

                    FillSquadronRandomly(squadron, 3);
                    allSquadrons.Add(squadron);
                }

                Console.WriteLine("\nВсе корабли вышли в море. Бой начинается");
                Console.WriteLine("Нажмите любую кнопку чтобы начать...");
                Console.ReadKey();

                int round = 1;
                while (allSquadrons.Count(squadron => squadron.IsAlive()) > 1)
                {
                    Console.WriteLine($"\n--- Раунд {round} начался ---\n");

                    foreach (var squadron in allSquadrons)
                    {
                        foreach (var ship in squadron.Ships)
                        {
                            ship.ProcessDelayedAttacks();
                        }
                    }

                    if (allSquadrons.Count(sq => sq.IsAlive()) <= 1) break;

                    foreach (var currentSquadron in allSquadrons.Where(s => s.IsAlive()))
                    {
                        Console.WriteLine($"\nХод эскадры: {currentSquadron.Name}\n");
                        currentSquadron.Attack(allSquadrons, tacticId);

                        if (allSquadrons.Count(sq => sq.IsAlive()) <= 1) break;
                    }

                    foreach (var ship in allSquadrons.SelectMany(ship => ship.Ships))
                    {
                        ship.Weapon?.ReduceCoolDown();
                    }

                    Console.WriteLine($"\n--- Раунд {round} закончился ---");
                    Console.WriteLine("Нажмите любую кнопку, чтобы продолжить...\n");
                    Console.ReadKey();
                    round++;
                }

                var winner = allSquadrons.FirstOrDefault(squadron => squadron.IsAlive());
                Console.WriteLine("\n------------------------------------------------");
                if (winner != null)
                {
                    Console.WriteLine($"Победу одержала команда: {winner.Name}!");
                    Console.WriteLine($"Количество уцелевших кораблей: {winner.Ships.Count(ship => ship.IsAlive())}");
                }
                else
                {
                    Console.WriteLine("Ничья! На воде не осталость уцелевших кораблей...");
                }
            }

            static void FillSquadronRandomly(Squadron squadron, int count)
            {
                Random random = new();
                for (int i = 0; i < count; i++)
                {
                    Ship ship = random.Next(3) switch
                    {
                        0 => new Destroyer($"Эсминец '{squadron.Name} #{i + 1}'"),
                        1 => new Cruiser($"Крейсер '{squadron.Name} #{i + 1}'"),
                        _ => new Battleship($"Линкор '{squadron.Name} #{i + 1}'")
                    };

                    ship.Armor = Warehouse.GetRandomArmor();
                    ship.Weapon = Warehouse.GetRandomWeapon(ship);
                    ship.Weapon.LoadedAmmo = Warehouse.GetRandomAmmo(ship.Weapon);

                    squadron.AddShip(ship);
                    Console.WriteLine($"+ Создан {ship.Name} (Орудие: {ship.Weapon.Name}, Снаряды: {ship.Weapon.LoadedAmmo.Name})");
                }
            }

            static string Spaces(int count)
            {
                return new string(' ', count);
            }
        }
    }
}
