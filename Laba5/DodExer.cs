using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba5
{
    using Student = (string lastName, string firstName, string patronymic, char gender, DateTime birthDate, int? math, int? physics, int? informatic, int scholarship);
    internal class DodExer
    {
        public static void Run()
        {

            Console.Clear();
            Console.WriteLine("=== Додаткові завдання ===");
            string inputFile = "input.txt";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Файл не знайдено!");
                Console.WriteLine("Натисніть будь-яку клавішу для повернення в меню...");
                Console.ReadKey();
                return;
            }

            var students = ReadStudentsFromFile(inputFile);
            double boysAverage = CalculateBoysAverage(students);

            if (boysAverage == -1)
            {
                Console.WriteLine("Немає студентів чоловічої статі для обчислення середнього балу.");
                return;
            }

            Console.WriteLine($"Середній бал студентів чоловічої статі: {boysAverage:F2}");
            PrintGirlsAboveAverage(students, boysAverage);
            Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися до меню...");
            Console.ReadKey();
        }
        static List<Student> ReadStudentsFromFile(string filePath)
{
    List<Student> students = new List<Student>();
    string[] lines = File.ReadAllLines(filePath);

    for (int i = 0; i < lines.Length; i++)
    {
        string line = lines[i];
        if (string.IsNullOrWhiteSpace(line))
            continue;

        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 9)
        {
            PrintError($"Рядок {i + 1}: недостатньо даних (очікується 9 полів)");
            continue;
        }

        string surname = parts[0];
        string name = parts[1];
        string patronymic = parts[2];
        string genderStr = parts[3];
        string birthdateStr = parts[4];

        if (genderStr != "Ч" && genderStr != "Ж")
        {
            PrintError($"Рядок {i + 1}: некоректне значення статі: '{genderStr}'");
            continue;
        }

        if (!DateTime.TryParse(birthdateStr, out DateTime birthDate))
        {
            PrintError($"Рядок {i + 1}: помилка у форматі дати");
            continue;
        }

        char gender = genderStr[0];

        bool ok1 = TryParseMark(parts[5], out int math);
        bool ok2 = TryParseMark(parts[6], out int phisics);
        bool ok3 = TryParseMark(parts[7], out int informatic);
        bool okScholar = int.TryParse(parts[8], out int scholarship);

        if (!ok1 || !ok2 || !ok3)
        {
            PrintError($"Рядок {i + 1}: помилка у записі оцінок (дозволено числа або '-')");
            continue;
        }

        if (!okScholar)
        {
            PrintError($"Рядок {i + 1}: помилка в полі стипендії");
            continue;
        }

        students.Add((surname, name, patronymic, gender, birthDate, math, phisics, informatic, scholarship));
    }

    return students;
}


        static double CalculateBoysAverage(List<Student> students)
        {
            int sum = 0;
            int count = 0;

            foreach (var student in students)
            {
                if (student.gender == 'Ч')
                {
                    int average = (student.math.GetValueOrDefault() + student.physics.GetValueOrDefault() + student.informatic.GetValueOrDefault()) / 3;
                    sum += average;
                    count++;
                }
            }

            if (count == 0)
                return -1;

            return (double)sum / count;
        }

        static void PrintGirlsAboveAverage(List<Student>students, double boysAverage)
        {
            bool found = false;

            foreach (var student in students)
            {
                if (student.gender == 'Ж')
                {
                    int average = (student.math.GetValueOrDefault() + student.physics.GetValueOrDefault() + student.informatic.GetValueOrDefault()) / 3;
                    if (average > boysAverage)
                    {
                        Console.WriteLine($"{student.lastName} {student.firstName} {student.patronymic} - середній бал: {average}");
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Немає студенток, середній бал яких перевищує середній бал студентів чоловічої статі.");
            }
        }

        static bool TryParseMark(string input, out int mark)
        {
            if (input == "-")
            {
                mark = 2;
                return true;
            }

            return int.TryParse(input, out mark);
        }

        static void PrintError(string message)
        {
            Console.WriteLine(message);
        }
    }
}

