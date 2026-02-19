using System;
using System.Collections.Generic;
using System.Text;

namespace Ships.Entities
{
    // Это класс для орудия
    internal class Guns
    {
        public void callGuns()
        {
            Console.WriteLine("Всего есть 3 орудия:");
            Console.WriteLine("1. Башенные установки главного калибра - урон 40-50, медленные (стреляют раз в 2 хода), пробивают любую броню.");
            Console.WriteLine("2. Универсальные орудия - урон 20-30.");
            Console.WriteLine("3. Торпедные аппараты - урон 35-45, игнорируют броню, но торпеда идет до цели 1 ход (выстрел сейчас - попадание в следующем ходу).");
        }
    }
}
