using System;

class Program
{
    static void Main()
    {
        // Задание 1: Определение чётного или нечётного числа
        Console.WriteLine("Задание 1: Введите целое число для проверки на чётность:");
        int number = int.Parse(Console.ReadLine());
        if (number % 2 == 0)
        {
            Console.WriteLine("Число чётное.");
        }
        else
        {
            Console.WriteLine("Число нечётное.");
        }

        // Задание 2: Подсчёт суммы карт в игре «21»
        Console.WriteLine("\nЗадание 2: Сколько у вас на руках карт?");
        int cardCount = int.Parse(Console.ReadLine());
        int sum = 0;

        for (int i = 0; i < cardCount; i++)
        {
            Console.WriteLine("Введите номинал карты (2-10, J, Q, K, T):");
            string card = Console.ReadLine().ToUpper();
            switch (card)
            {
                case "J":
                case "Q":
                case "K":
                case "T":
                    sum += 10;
                    break;
                default:
                    int cardValue;
                    if (int.TryParse(card, out cardValue))
                    {
                        sum += cardValue;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        i--; // Повторить ввод для текущей карты
                    }
                    break;
            }
        }

        Console.WriteLine($"Сумма ваших карт: {sum}");

        // Задание 3: Проверка простого числа
        Console.WriteLine("\nЗадание 3: Введите целое число для проверки на простоту:");
        int primeNumber = int.Parse(Console.ReadLine());
        bool isPrime = true;
        int iPrime = 2;

        while (iPrime <= Math.Sqrt(primeNumber))
        {
            if (primeNumber % iPrime == 0)
            {
                isPrime = false;
                break;
            }
            iPrime++;
        }

        if (isPrime && primeNumber > 1)
        {
            Console.WriteLine("Число простое.");
        }
        else
        {
            Console.WriteLine("Число не является простым.");
        }

        // Задание 4: Наименьший элемент в последовательности
        Console.WriteLine("\nЗадание 4: Введите длину последовательности:");
        int length = int.Parse(Console.ReadLine());
        int min = int.MaxValue;

        for (int i = 0; i < length; i++)
        {
            Console.WriteLine("Введите число:");
            int seqNumber = int.Parse(Console.ReadLine());
            if (seqNumber < min)
            {
                min = seqNumber;
            }
        }

        Console.WriteLine($"Наименьшее число в последовательности: {min}");

        // Задание 5: Игра «Угадай число»
        Console.WriteLine("\nЗадание 5: Введите максимальное целое число диапазона:");
        int maxRange = int.Parse(Console.ReadLine());
        Random random = new Random();
        int secretNumber = random.Next(0, maxRange + 1);
        bool guessed = false;

        Console.WriteLine("Попробуйте угадать число. Для выхода нажмите Enter без ввода числа.");

        while (!guessed)
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"Вы вышли из игры. Загаданное число было: {secretNumber}");
                break;
            }

            int guessedNumber;
            if (int.TryParse(input, out guessedNumber))
            {
                if (guessedNumber < secretNumber)
                {
                    Console.WriteLine("Загаданное число больше.");
                }
                else if (guessedNumber > secretNumber)
                {
                    Console.WriteLine("Загаданное число меньше.");
                }
                else
                {
                    Console.WriteLine("Поздравляем! Вы угадали число.");
                    guessed = true;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
            }
        }
    }
}
