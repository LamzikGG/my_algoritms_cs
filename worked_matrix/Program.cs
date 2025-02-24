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

        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                matrix[i, j] = random.Next(0, 10);
            }
        }

        Console.WriteLine("Матрица:");
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
        int shirina = matrix.GetLength(0);
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

        // Приведение к ступенчатому виду
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

    public static void summa_and_vichetanie(int[,] matrix1, int[,] matrix2)
    {
        int shirina = matrix1.GetLength(0);
        int dlina = matrix1.GetLength(1);

        if (matrix2.GetLength(0) != shirina || matrix2.GetLength(1) != dlina)
        {
            Console.WriteLine("Матрицы должны быть одинакового размера!");
            return;
        }

        int[,] sum = new int[shirina, dlina];
        int[,] diff = new int[shirina, dlina];

        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                sum[i, j] = matrix1[i, j] + matrix2[i, j];
                diff[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }

        Console.WriteLine("Сумма матриц:");
        for (int i = 0; i < shirina; i++)
        {
            for (int j = 0; j < dlina; j++)
            {
                Console.Write(sum[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Разность матриц:");
        for (int i = 0; i < shirina; i++){
            for (int j = 0; j < dlina; j++)
            {
                Console.Write(diff[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class vector{
    public static int[] worked_vector(){
        Console.WriteLine("Введите размер вектора:");
        int size = Convert.ToInt32(Console.ReadLine());
        int[] vector = new int[size];
        Random random = new Random();
        for (int i = 0; i < size; i++){
            vector[i] = random.Next(0, 10);
        }
        Console.WriteLine("Вектор:");
        for (int i = 0; i < size; i++){
            Console.Write(vector[i] + " ");
        }
        Console.WriteLine();
        return vector;
    }

    public static void skalarnoe_proizvedenie(int[] vector1, int[] vector2){
        if (vector1.Length != vector2.Length){
            Console.WriteLine("Векторы должны быть одинаковой длины!");
            return;
        }
        int result = 0;
        for (int i = 0; i < vector1.Length; i++){
            result += vector1[i] * vector2[i];
        }
        Console.WriteLine("Скалярное произведение векторов: " + result);
    }
    public static void vectornoe_proizvedenie(int[] vector1, int[] vector2){
        if (vector1.Length != 3 || vector2.Length != 3){
            Console.WriteLine("Векторное произведение определено только для 3D векторов!");
            return;
        }
        int[] result = new int[3];
        result[0] = vector1[1] * vector2[2] - vector1[2] * vector2[1];
        result[1] = vector1[2] * vector2[0] - vector1[0] * vector2[2];
        result[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
        Console.WriteLine("Векторное произведение:");
        for (int i = 0; i < 3; i++){
            Console.Write(result[i] + " ");
        }
        Console.WriteLine();
    }
}

class Program{
    public static void Main(){
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1 - Метод Гаусса");
        Console.WriteLine("2 - Произведение элементов матрицы");
        Console.WriteLine("3 - Сложение и вычитание матриц");
        Console.WriteLine("4 - Векторные операции");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                int[,] matrix1 = worked_matrix.vvod_matrix();
                worked_matrix.gaus(matrix1);
                break;
            case 2:
                int[,] matrix2 = worked_matrix.vvod_matrix();
                worked_matrix.proizvedenie(matrix2);
                break;
            case 3:
                int[,] matrixA = worked_matrix.vvod_matrix();
                int[,] matrixB = worked_matrix.vvod_matrix();
                worked_matrix.summa_and_vichetanie(matrixA, matrixB);
                break;
            case 4:
                Console.WriteLine("Первый вектор:");
                int[] vector1 = vector.worked_vector();
                Console.WriteLine("Второй вектор:");
                int[] vector2 = vector.worked_vector();
                Console.WriteLine("Выберите векторную операцию:");
                Console.WriteLine("1 - Скалярное произведение");
                Console.WriteLine("2 - Векторное произведение");
                int vectorChoice = Convert.ToInt32(Console.ReadLine());
                switch (vectorChoice)
                {
                    case 1:
                        vector.skalarnoe_proizvedenie(vector1, vector2);
                        break;
                    case 2:
                        vector.vectornoe_proizvedenie(vector1, vector2);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }
}