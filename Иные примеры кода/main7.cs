using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1. Работа с листом
            List<int> numbers = GenerateRandomNumbers(100, 0, 100);
            Console.WriteLine("Первоначальный список чисел:");
            DisplayList(numbers);

            numbers = RemoveNumbersInRange(numbers, 25, 50);
            Console.WriteLine("Список чисел после удаления чисел в диапазоне от 25 до 50:");
            DisplayList(numbers);

            // Задание 2. Телефонная книга
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();
            CollectPhoneBookEntries(phoneBook);
            Console.WriteLine("Поиск владельца по номеру телефона:");
            SearchOwnerByPhoneNumber(phoneBook);

            // Задание 3. Проверка повторов
            HashSet<int> numberSet = new HashSet<int>();
            CheckForDuplicateNumbers(numberSet);

            // Задание 4. Записная книжка
            CreateXmlContactFile();
        }

        // Методы для задания 1
        static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
        {
            Random random = new Random();
            List<int> numbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                numbers.Add(random.Next(minValue, maxValue + 1));
            }
            return numbers;
        }

        static void DisplayList(List<int> numbers)
        {
            foreach (var number in numbers)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }

        static List<int> RemoveNumbersInRange(List<int> numbers, int minValue, int maxValue)
        {
            return numbers.Where(number => number <= minValue || number >= maxValue).ToList();
        }

        // Методы для задания 2
        static void CollectPhoneBookEntries(Dictionary<string, string> phoneBook)
        {
            while (true)
            {
                Console.WriteLine("Введите номер телефона (пустая строка для завершения):");
                string phoneNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    break;
                }
                Console.WriteLine("Введите ФИО владельца:");
                string owner = Console.ReadLine();
                phoneBook[phoneNumber] = owner;
            }
        }

        static void SearchOwnerByPhoneNumber(Dictionary<string, string> phoneBook)
        {
            Console.WriteLine("Введите номер телефона для поиска:");
            string phoneNumber = Console.ReadLine();
            if (phoneBook.TryGetValue(phoneNumber, out string owner))
            {
                Console.WriteLine($"Владелец номера {phoneNumber}: {owner}");
            }
            else
            {
                Console.WriteLine("Владелец с таким номером телефона не зарегистрирован.");
            }
        }

        // Методы для задания 3
        static void CheckForDuplicateNumbers(HashSet<int> numberSet)
        {
            while (true)
            {
                Console.WriteLine("Введите число (пустая строка для завершения):");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                if (int.TryParse(input, out int number))
                {
                    if (numberSet.Add(number))
                    {
                        Console.WriteLine("Число успешно сохранено.");
                    }
                    else
                    {
                        Console.WriteLine("Число уже вводилось ранее.");
                    }
                }
                else
                {
                    Console.WriteLine("Введите корректное число.");
                }
            }
        }

        // Методы для задания 4
        static void CreateXmlContactFile()
        {
            Console.WriteLine("Введите ФИО:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите улицу:");
            string street = Console.ReadLine();

            Console.WriteLine("Введите номер дома:");
            string houseNumber = Console.ReadLine();

            Console.WriteLine("Введите номер квартиры:");
            string flatNumber = Console.ReadLine();

            Console.WriteLine("Введите мобильный телефон:");
            string mobilePhone = Console.ReadLine();

            Console.WriteLine("Введите домашний телефон:");
            string flatPhone = Console.ReadLine();

            XElement contact = new XElement("Person",
                new XAttribute("name", name),
                new XElement("Address",
                    new XElement("Street", street),
                    new XElement("HouseNumber", houseNumber),
                    new XElement("FlatNumber", flatNumber)
                ),
                new XElement("Phones",
                    new XElement("MobilePhone", mobilePhone),
                    new XElement("FlatPhone", flatPhone)
                )
            );

            XDocument doc = new XDocument(contact);
            doc.Save("contact.xml");

            Console.WriteLine("Контакт сохранен в файл contact.xml");
        }
    }
}
