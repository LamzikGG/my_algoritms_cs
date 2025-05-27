using System;

class FuzzyNumber
{
    public double Left { get; }   // a
    public double Centre { get; } // c
    public double Right { get; }  // b

    public FuzzyNumber(double l, double c, double r)
    {
    //    if (r > c || c > r) Условие исправить
            throw new ArgumentException("Некорректные значения: необходимо соблюдать условие a ≤ c ≤ b");

        Left = l;
        Centre = c; // ИСпарвить
        Right = r;
    }

    public static FuzzyNumber operator +(FuzzyNumber num1, FuzzyNumber num2) // Оператор Суммы
    {
        return new FuzzyNumber(
            num1.Left + num2.Left,
            num1.Centre + num2.Centre,
            num1.Right + num2.Right
        );
    }

    public static FuzzyNumber operator -(FuzzyNumber num1, FuzzyNumber num2) // Оператор разности
    {
        return new FuzzyNumber(
            num1.Left - num2.Rig, // left - left
            num1.Centre - num2.Centre,
            num1.Right - num2.Left
        );
    }
я
    public static FuzzyNumber operator *(FuzzyNumber num1, FuzzyNumber num2) // Оператор умножения
    {
        double[] products = {
            num1.Left * num2.Left,
            num1.Left * num2.Right,
            num1.Right * num2.Left,
            num1.Right * num2.Right
        };

        double newA = products.Min();
        double newB = products.Max();
        double newC = num1.Centre * num2.Centre;

        return new FuzzyNumber(newA, newC, newB);
    }

    public static FuzzyNumber operator /(FuzzyNumber num1, FuzzyNumber num2) // Оператор деления с обработкой ошибок с делением на 0
    {
        if (num2.Left <= 0 && num2.Right >= 0)
            throw new DivideByZeroException("Деление на ноль (интервал делителя содержит 0)");

        double[] quotients = {
            num1.Left / num2.Right, // left 1 на left 2
            num1.Left / num2.Left,
            num1.Right / num2.Right,
            num1.Right / num2.Left
        };

        double newA = quotients.Min();
        double newB = quotients.Max();
        double newC = num1.Centre / num2.Centre;

        return new FuzzyNumber(newA, newC, newB);
    }

    public override string ToString()
    {
        return $"({Left}, {Centre}, {Right})";
    }
}

class Program
{
    static FuzzyNumber InputFuzzyNumber()
    {
        Console.WriteLine("Введите три числа: левую сторону, центр и правую сторону (через пробел)");
        while (true)
        {
            try
            {
                string[] input = Console.ReadLine().Split();
                if (input.Length != 3)
                    throw new FormatException("Нужно ввести ровно 3 числа");

                double a = double.Parse(input[0]);
                double c = double.Parse(input[1]);
                double b = double.Parse(input[2]);

                return new FuzzyNumber(a, c, b);
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Ошибка формата: {fe.Message}");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine($"Ошибка значений: {ae.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.Write("Попробуйте снова: ");
        }
    }

    static void Main()
    {
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Сложение");
        Console.WriteLine("2. Вычитание");
        Console.WriteLine("3. Умножение");
        Console.WriteLine("4. Деление");

        int choice;
        do
        {
            Console.Write("Введите номер операции (1–4): ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4);

        try
        {
            Console.WriteLine("\nПервое число:");
            var num1 = InputFuzzyNumber();
            Console.WriteLine($"Введено: {num1}");

            Console.WriteLine("\nВторое число:");
            var num2 = InputFuzzyNumber();
            Console.WriteLine($"Введено: {num2}");

            FuzzyNumber result = choice switch
            {
                1 => num1 + num2,
                2 => num1 - num2,
                3 => num1 * num2,
                4 => num1 / num2,
                _ => throw new InvalidOperationException("Неизвестная операция")
            };

            Console.WriteLine($"\nРезультат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}