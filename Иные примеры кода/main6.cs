using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WorkerRepositoryApp
{
    struct Worker
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public Worker(int id, DateTime recordDate, string fio, int age, int height, DateTime birthDate, string birthPlace)
        {
            Id = id;
            RecordDate = recordDate;
            FIO = fio;
            Age = age;
            Height = height;
            BirthDate = birthDate;
            BirthPlace = birthPlace;
        }

        public override string ToString()
        {
            return $"{Id}#{RecordDate:dd.MM.yyyy HH:mm}#{FIO}#{Age}#{Height}#{BirthDate:dd.MM.yyyy}#{BirthPlace}";
        }

        public static Worker FromString(string data)
        {
            var parts = data.Split('#');
            return new Worker(
                int.Parse(parts[0]),
                DateTime.ParseExact(parts[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                parts[2],
                int.Parse(parts[3]),
                int.Parse(parts[4]),
                DateTime.ParseExact(parts[5], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                parts[6]
            );
        }
    }

    class Repository
    {
        private readonly string fileName;

        public Repository(string fileName)
        {
            this.fileName = fileName;
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
        }

        public Worker[] GetAllWorkers()
        {
            return File.ReadAllLines(fileName)
                .Select(Worker.FromString)
                .ToArray();
        }

        public Worker GetWorkerById(int id)
        {
            return GetAllWorkers().FirstOrDefault(w => w.Id == id);
        }

        public void DeleteWorker(int id)
        {
            var workers = GetAllWorkers().Where(w => w.Id != id).ToArray();
            File.WriteAllLines(fileName, workers.Select(w => w.ToString()));
        }

        public void AddWorker(Worker worker)
        {
            worker = new Worker(
                GetAllWorkers().Max(w => (int?)w.Id) ?? 0 + 1,
                DateTime.Now,
                worker.FIO,
                worker.Age,
                worker.Height,
                worker.BirthDate,
                worker.BirthPlace
            );
            File.AppendAllText(fileName, worker.ToString() + Environment.NewLine);
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            return GetAllWorkers()
                .Where(w => w.RecordDate >= dateFrom && w.RecordDate <= dateTo)
                .ToArray();
        }
    }

    class Program
    {
        static void Main()
        {
            var repository = new Repository("workers.txt");

            // Примеры добавления работников
            repository.AddWorker(new Worker(0, DateTime.Now, "Иванов Иван Иванович", 25, 176, new DateTime(1992, 5, 5), "город Москва"));
            repository.AddWorker(new Worker(0, DateTime.Now, "Алексеев Алексей Иванович", 24, 176, new DateTime(1980, 11, 5), "город Томск"));

            // Примеры вывода всех работников
            var workers = repository.GetAllWorkers();
            foreach (var worker in workers)
            {
                Console.WriteLine(worker);
            }

            // Пример получения записи по ID
            var workerById = repository.GetWorkerById(1);
            Console.WriteLine(workerById);

            // Пример удаления записи
            repository.DeleteWorker(1);

            // Пример получения записей в диапазоне дат
            var workersInRange = repository.GetWorkersBetweenTwoDates(DateTime.Now.AddMonths(-1), DateTime.Now);
            foreach (var worker in workersInRange)
            {
                Console.WriteLine(worker);
            }

            // Пример сортировки по FIO
            var sortedWorkersByFio = workers.OrderBy(w => w.FIO).ToArray();
            foreach (var worker in sortedWorkersByFio)
            {
                Console.WriteLine(worker);
            }

            // Пример сортировки по ID
            var sortedWorkersById = workers.OrderBy(w => w.Id).ToArray();
            foreach (var worker in sortedWorkersById)
            {
                Console.WriteLine(worker);
            }
        }
    }
}

