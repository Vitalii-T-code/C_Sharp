using System;

class Program
{
    static void Main()
    {
        Random rand = new Random();

        // Задание 1: Случайная матрица
        Console.Write("Введите количество строк: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Введите количество столбцов: ");
        int cols = int.Parse(Console.ReadLine());

        int[,] matrix1 = new int[rows, cols];
        FillMatrix(matrix1, rand);
        Console.WriteLine("Матрица 1:");
        PrintMatrix(matrix1);
        Console.WriteLine("Сумма всех элементов матрицы 1: " + SumMatrix(matrix1));

        // Задание 2: Сложение матриц
        int[,] matrix2 = new int[rows, cols];
        FillMatrix(matrix2, rand);
        Console.WriteLine("Матрица 2:");
        PrintMatrix(matrix2);

        int[,] sumMatrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sumMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }
        Console.WriteLine("Сумма матриц:");
        PrintMatrix(sumMatrix);

        // Дополнительное задание: Игра "Жизнь"
        Console.WriteLine("Запустить игру 'Жизнь'? (да/нет): ");
        if (Console.ReadLine().ToLower() == "да")
        {
            GameOfLife(rows, cols, rand);
        }
    }

    static void FillMatrix(int[,] matrix, Random rand)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = rand.Next(1, 10); // Диапазон случайных чисел от 1 до 9
            }
        }
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static int SumMatrix(int[,] matrix)
    {
        int sum = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                sum += matrix[i, j];
            }
        }
        return sum;
    }

    static void GameOfLife(int rows, int cols, Random rand)
    {
        bool[,] grid = new bool[rows, cols];
        FillLifeGrid(grid, rand);
        Console.CursorVisible = false;

        while (true)
        {
            Console.Clear();
            PrintLifeGrid(grid);
            grid = UpdateLifeGrid(grid);
            System.Threading.Thread.Sleep(500); // Задержка 500 миллисекунд
        }
    }

    static void FillLifeGrid(bool[,] grid, Random rand)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = rand.Next(2) == 0; // 50% шанс, что клетка будет заселена бактерией
            }
        }
    }

    static void PrintLifeGrid(bool[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j] ? "O" : " ");
            }
            Console.WriteLine();
        }
    }

    static bool[,] UpdateLifeGrid(bool[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        bool[,] newGrid = new bool[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int aliveNeighbors = CountAliveNeighbors(grid, i, j);

                if (grid[i, j])
                {
                    newGrid[i, j] = aliveNeighbors == 2 || aliveNeighbors == 3;
                }
                else
                {
                    newGrid[i, j] = aliveNeighbors == 3;
                }
            }
        }

        return newGrid;
    }

    static int CountAliveNeighbors(bool[,] grid, int row, int col)
    {
        int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int aliveNeighbors = 0;

        for (int i = 0; i < 8; i++)
        {
            int newRow = row + dr[i];
            int newCol = col + dc[i];

            if (newRow >= 0 && newRow < grid.GetLength(0) && newCol >= 0 && newCol < grid.GetLength(1))
            {
                aliveNeighbors += grid[newRow, newCol] ? 1 : 0;
            }
        }

        return aliveNeighbors;
    }
}
