using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите предложение:");
        string input = Console.ReadLine();

        // Вызов методов для задания 1
        string[] words = SplitIntoWords(input);
        PrintWords(words);

        // Вызов метода для задания 2
        Console.WriteLine("Результат перестановки слов:");
        string reversedPhrase = ReverseWords(input);
        Console.WriteLine(reversedPhrase);
    }

    // Метод для разделения строки на слова
    static string[] SplitIntoWords(string inputPhrase)
    {
        return inputPhrase.Split(' ');
    }

    // Метод для вывода каждого слова на отдельной строке
    static void PrintWords(string[] words)
    {
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }

    // Метод для инвертирования слов в предложении
    static string ReverseWords(string inputPhrase)
    {
        string[] words = SplitIntoWords(inputPhrase);
        Array.Reverse(words);
        return string.Join(" ", words);
    }
}
