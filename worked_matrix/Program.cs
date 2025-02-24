using System;

class worked_matrix
{
    public static int[,] vvod_matrix()
    {
        Console.WriteLine("Введите ширину матрицы");
        int shirina = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите длину матрицы");
        int dlina = Convert.ToInt32(Console.ReadLine());
        Random random = new Random();
        int[,] matrix = new int[shirina, dlina];

        for (int i = 0; i < shirina; i++){
            for (int j = 0; j < dlina; j++){
                matrix[i, j] = random.Next(0, 10);
            }
        }
        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        return matrix; 
    }

    
    public static void proizvedenie(int[,] matrix)
    {
        int shirina = matrix.GetLength(0); 
        int dlina = matrix.GetLength(1);   

        Console.WriteLine("Произведение элементов матрицы:");
        int product = 1;
        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                product *= matrix[i, j];
            }
        }
        Console.WriteLine(product);
    }

    public static void gaus(int[,] matrix)
    {
        int shirina = matrix.GetLength(0); // Получение инфы
        int dlina = matrix.GetLength(1);   

        Console.WriteLine("Матрица до преобразования:");
        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        //
        for (int i = 0; i < shirina; i++)
        {
            for (int j = i + 1; j < shirina; j++)
            {
                if (matrix[i, i] != 0)
                {
                    double coeff = matrix[j, i] / (double)matrix[i, i];
                    for (int k = 0; k < dlina; k++)
                    {
                        matrix[j, k] -= (int)(coeff * matrix[i, k]);
                    }
                }
            }
        }

        Console.WriteLine("Матрица после преобразования:");
        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class vector
{
    public static void worked_vector()
    {
        List<int> list = new List<int>(Enumerable.Repeat(0, 101));
        list.Insert(10, 12345);
        list[10] = 12345;
        Console.WriteLine(list);
    }

    public static void skalarnoe_proizvedenie()
    {
        Console.WriteLine("Скалярное произведение");
    }
    public static void vectornoe_proizvedenie()
    {
        Console.WriteLine("Векторное произведение");
    }
}

class Program
{
    public static void Main()
    {
        int[,] matrix = worked_matrix.vvod_matrix();

        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1 - Метод Гаусса");
        Console.WriteLine("2 - Произведение матрицы");
        Console.WriteLine("3 - Векторные чудеса");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                worked_matrix.gaus(matrix);
                break;
            case 2:
                worked_matrix.proizvedenie(matrix);
                break;
            case 3:
                vector.worked_vector();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }
}