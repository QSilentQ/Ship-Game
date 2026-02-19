using Ships.Entities;

namespace Ships
{
    internal class ShipGame
    {
        static void Main(string[] args)
        {
            string? userChoise;

            // Это главный файл для запуска игры
            Console.WriteLine("          Добро пожаловать в игру!");
            Console.WriteLine("               Морские войны\n");
            Console.WriteLine("                   Меню\n");

            Console.WriteLine("                1. Играть\n");
            Console.WriteLine("                2. Выйти\n");

            Console.Write("            Выберите действие: ");
            while (true)
            {
                userChoise = Console.ReadLine()?.Trim();

                if (userChoise == "1" || userChoise == "2") break;
                Console.Write("  Неверный ввод. Пожалуйста, выберите 1 или 2: ");
            }

            switch (userChoise)
            {
                case "1":
                    Console.WriteLine("             Вы выбрали: Играть");
                    break;
                case "2":
                    Console.WriteLine("             Вы выбрали: Выйти");
                    break;
            }
        }
    }
}
