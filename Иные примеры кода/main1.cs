using System;

class Program
{
    static void Main()
    {
        // Задание 1: Создание переменных и вывод
        string fullName = "Третьяков Виталий Анатольевич";
        int age = 28;
        string email = "vitaliy@example.com";
        float programmingScore = 85.5f;
        float mathScore = 90.0f;
        float physicsScore = 78.5f;

        // Форматированный вывод данных на экран
        Console.WriteLine("Student Information:");
        Console.WriteLine($"Full Name: {fullName}");
        Console.WriteLine($"Age: {age}");
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Programming Score: {programmingScore:F1}");
        Console.WriteLine($"Math Score: {mathScore:F1}");
        Console.WriteLine($"Physics Score: {physicsScore:F1}");

        // Задание 2: Реализация подсчёта количества баллов по всем предметам
        float totalScore = programmingScore + mathScore + physicsScore;
        float averageScore = totalScore / 3;

        // Вывод информации на экран после нажатия на любую клавишу
        Console.WriteLine("\nScores Summary:");
        Console.WriteLine($"Total Score: {totalScore:F1}");
        Console.WriteLine($"Average Score: {averageScore:F1}");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

