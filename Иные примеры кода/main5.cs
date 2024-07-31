using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1 - Вывести данные на экран");
        Console.WriteLine("2 - Заполнить данные и добавить новую запись в конец файла");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                DisplayEmployees();
                break;
            case "2":
                AddEmployee();
                break;
            default:
                Console.WriteLine("Неверный выбор. Повторите попытку.");
                break;
        }
    }

    // Метод для вывода данных сотрудников на экран
    static void DisplayEmployees()
    {
        if (File.Exists("staff.txt"))
        {
            string[] lines = File.ReadAllLines("staff.txt");
            foreach (string line in lines)
            {
                string[] data = line.Split('#');
                Console.WriteLine($"ID: {data[0]}");
                Console.WriteLine($"Дата и время добавления: {data[1]}");
                Console.WriteLine($"Ф. И. О.: {data[2]}");
                Console.WriteLine($"Возраст: {data[3]}");
                Console.WriteLine($"Рост: {data[4]}");
                Console.WriteLine($"Дата рождения: {data[5]}");
                Console.WriteLine($"Место рождения: {data[6]}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Файл с сотрудниками не найден.");
        }
    }

    // Метод для добавления новой записи сотрудника в файл
    static void AddEmployee()
    {
        int id = GetNextId();
        DateTime now = DateTime.Now;

        Console.WriteLine("Введите Ф. И. О.:");
        string name = Console.ReadLine();

        Console.WriteLine("Введите возраст:");
        int age = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите рост:");
        int height = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите дату рождения (дд.мм.гггг):");
        string birthDate = Console.ReadLine();

        Console.WriteLine("Введите место рождения:");
        string birthPlace = Console.ReadLine();

        string record = $"{id}#{now:dd.MM.yyyy HH:mm}#{name}#{age}#{height}#{birthDate}#{birthPlace}";

        using (StreamWriter sw = new StreamWriter("staff.txt", true))
        {
            sw.WriteLine(record);
        }

        Console.WriteLine("Запись добавлена успешно.");
    }

    // Метод для получения следующего ID
    static int GetNextId()
    {
        int id = 1;

        if (File.Exists("staff.txt"))
        {
            string[] lines = File.ReadAllLines("staff.txt");
            if (lines.Length > 0)
            {
                string lastLine = lines[lines.Length - 1];
                string[] data = lastLine.Split('#');
                id = int.Parse(data[0]) + 1;
            }
        }

        return id;
    }
}
