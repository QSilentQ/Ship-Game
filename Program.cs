using Ships.Entities.Ships;

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
                Console.Write("  Неверный ввод. Пожалуйста, выберите 1 или 2: ");
            }

            switch (userChoise)
            {
                case "1":
                    Console.WriteLine("             Вы выбрали: Играть\n");
                    break;
                case "2":
                    Console.WriteLine("             Вы выбрали: Выйти\n");
                    break;
            }

            var Destroyer = new Destroyer("Эсминец №1");
            var Cruiser = new Cruiser("Крейсер №1");

            while (Destroyer.IsAlive() && Cruiser.IsAlive())
            {
                int damage = Destroyer.DealDamage();
                Cruiser.TakeDamage(damage);

                if (!Cruiser.IsAlive()) break;

                damage = Cruiser.DealDamage();
                Destroyer.TakeDamage(damage);

                Console.WriteLine();
            }

            Console.WriteLine("Бой закончен!");
        }

        static string Spaces(int count)
        {
            return new string(' ', count);
        }
    }
}
