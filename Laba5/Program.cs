using Laba5;
using System;

class Program
{
    static void Main()
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Меню лабораторної роботи ===");
            Console.WriteLine("1 - Блок 1");
            Console.WriteLine("2 - Блок 2");
            Console.WriteLine("3 - Додаткове завдання");
            Console.WriteLine("0 - Вийти");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Block1.Run();
                    break;
                case "2":
                    Block2.Run();
                    break;
                case "3":
                    DodExer.Run();
                    break;
                case "0":
                    return; 
                default:
                    Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}