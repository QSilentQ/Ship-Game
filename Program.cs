using Ships.Entities.Ships;
using Ships.Entities.Squadrons;

namespace Ships
{
    internal class ShipGame
    {
        static void Main(string[] args)
        {
            string? userChoise;

            // Это главный файл для запуска игры
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

            var firstSquad = new Squadron("Первая эскадрилия (А)");
            var secondSquad = new Squadron("Вторая эскадрилия (Б)");

            firstSquad.AddShip(new Destroyer("Эсминец А"));
            firstSquad.AddShip(new Cruiser("Крейсер А"));
            firstSquad.AddShip(new Battleship("Линкор А"));

            secondSquad.AddShip(new Destroyer("Эсминец Б"));
            secondSquad.AddShip(new Cruiser("Крейсер Б"));
            secondSquad.AddShip(new Battleship("Линкор Б"));

            while (firstSquad.IsAlive() && secondSquad.IsAlive())
            {
                foreach (var fqShip in firstSquad.Ships)
                {
                    if (!firstSquad.IsAlive()) continue;

                    var targets = secondSquad.Ships.FindAll(ship => ship.IsAlive());
                    if (targets.Count == 0) break;

                    var target = targets[new Random().Next(targets.Count)];
                    int damage = fqShip.DealDamage();
                    target.TakeDamage(damage);
                }

                if (!secondSquad.IsAlive()) break;

                foreach (var sqShip in secondSquad.Ships)
                {
                    if (!secondSquad.IsAlive()) continue;

                    var targets = firstSquad.Ships.FindAll(ship => ship.IsAlive());
                    if (targets.Count == 0) break;

                    var target = targets[new Random().Next(targets.Count)];
                    int damage = sqShip.DealDamage();
                    target.TakeDamage(damage);
                }

                Console.WriteLine();
            }

            Console.WriteLine("Бой закончился!");

            
            string answer = firstSquad.IsAlive() ? "Первая команда победила" : "Вторая команда победила";
            Console.WriteLine(answer);
        }

        static string Spaces(int count)
        {
            return new string(' ', count);
        }
    }
}
