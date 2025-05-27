using System;

// Определение класса FuzzyNumber
public class FuzzyNumber
{
    public double Left { get; }   // нижняя граница (минимально возможное значение),
    public double Centre { get; } // центральное значение (ядро, наиболее вероятное значение),
    public double Right { get; }  // верхняя граница (максимально возможное значение).

    // Конструктор
    public FuzzyNumber(double left, double centre, double right)
    {
        if (left > centre || centre > right)
            throw new ArgumentException("Некорректные значения: необходимо соблюдать условие a ≤ c ≤ b");

        Left = left;
        Centre = centre;
        Right = right;
    }

    // Реализация оператора +
    public static FuzzyNumber operator +(FuzzyNumber num1, FuzzyNumber num2)
    {
        return new FuzzyNumber(
            num1.Left + num2.Left,
            num1.Centre + num2.Centre,
            num1.Right + num2.Right
            );
    }

    // Реализация оператора -
    public static FuzzyNumber operator -(FuzzyNumber num1, FuzzyNumber num2)
    {
        return new FuzzyNumber(
            num1.Left - num2.Right,
            num1.Centre - num2.Centre,
            num1.Right - num2.Left
            );
    }

    // Реализация оператора *
    public static FuzzyNumber operator *(FuzzyNumber num1, FuzzyNumber num2)
    {
        //принцип обработки не определенности в нечетких множествах 
        double[] products =
        {
            num1.Left * num2.Left,
            num1.Left * num2.Right,
            num1.Right * num2.Left,
            num1.Right * num2.Right
        };

        double newA = products.Min();  // Мин. произведение
        double newB = products.Max();  // Макс. произведение
        double newC = num1.Centre * num2.Centre;  // Произведение центральных значений

        return new FuzzyNumber(newA, newC, newB);
    }

    // Реализация оператора /
    public static FuzzyNumber operator /(FuzzyNumber num1, FuzzyNumber num2)
    {
        if ((num2.Left <= 0 && num2.Right >= 0))
            throw new DivideByZeroException("Делитель содержит ноль");

        //принцип обработки не определенности в нечетких множествах 
        double[] quotients =
        {
            num1.Left / num2.Left,
            num1.Left / num2.Right,
            num1.Right / num2.Left,
            num1.Right / num2.Right
        };

        double newA = quotients.Min();  // Мин. частное
        double newB = quotients.Max();  // Макс. частное
        double newC = num1.Centre / num2.Centre;  // Частное центральных значений

        return new FuzzyNumber(newA, newC, newB);
    }

    // Переопределение метода ToString() для красивого отображения
    public override string ToString()
    {
        return $"({Left}, {Centre}, {Right})";
    }
}

// Основной класс программы
class Program
{
    // Функция для ввода fuzzy числа
    static FuzzyNumber InputFuzzyNumber()
    {
        while (true)
        {
            try
            {
                string[] input = Console.ReadLine().Split();
                if (input.Length != 3)
                    throw new FormatException("Необходимо ввести ровно 3 числа");

                double a = double.Parse(input[0]); // Нижняя граница
                double c = double.Parse(input[1]); // Центральное значение
                double b = double.Parse(input[2]); // Верхняя граница

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
            catch (DivideByZeroException de)
            {
                Console.WriteLine($"Ошибка: {de.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.Write("Попробуйте снова: ");
        }
    }

    // Основная точка входа
    static void Main()
    {
        Console.WriteLine("Выберите операцию:\n1. Сложение\n2. Вычитание\n3. Умножение\n4. Деление");

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