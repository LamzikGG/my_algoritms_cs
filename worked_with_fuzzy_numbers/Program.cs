using System;

class FuzzyNumber
{
    public double A { get; } //Левая точка 
    public double C { get; } //Центральная точка 
    public double B { get; } // Правая точка

    public double left {get;} // промежуток между a и c 
    public double right{get;} // промежуток меду c и b

    public FuzzyNumber(double a, double c, double b)
    {
        A = Math.Min(Math.Min(a, c), b);
        B = Math.Max(Math.Max(a, c), b);
        C = a + c + b - A - B; 
        
        if (A > C || C > B)
            C = (A + B) / 2;  
    }

    public static FuzzyNumber operator +(FuzzyNumber num1, FuzzyNumber num2)
    {
        double newA = num1.A + num2.A;
        double newC = num1.C + num2.C;
        double newB = num1.B + num2.B;
        
        return new FuzzyNumber(newA, newC, newB);
    }

    public static FuzzyNumber operator -(FuzzyNumber num1, FuzzyNumber num2)
    {
        double newA = num1.A - num2.A;
        double newC = num1.C - num2.C;
        double newB = num1.B - num2.B;
        
        return new FuzzyNumber(newA, newC, newB);
    }

    public static FuzzyNumber operator *(FuzzyNumber num1, FuzzyNumber num2)
    {
        double[] products = {
            num1.A * num2.A,
            num1.A * num2.B,
            num1.B * num2.A,
            num1.B * num2.B
        };
        
        double newA = products.Min();
        double newB = products.Max();
        double newC = num1.C * num2.C;
        
        return new FuzzyNumber(newA, newC, newB);
    }

    public static FuzzyNumber operator /(FuzzyNumber num1, FuzzyNumber num2)
    {       
        double[] quotients = {
            num1.A / num2.A,
            num1.A / num2.B,
            num1.B / num2.A,
            num1.B / num2.B
        };
        
        double newA = quotients.Min();
        double newB = quotients.Max();
        double newC = num1.C / num2.C;
        
        return new FuzzyNumber(newA, newC, newB);
    }

    public override string ToString() => $"{A}, {B}, {C}";
    public override string ToString() => $"{left}, {right}";
}

class Program
{
    static FuzzyNumber InputFuzzyNumber()
    {
        Console.WriteLine("Введите a, c, b через пробел:");
        while (true)
        {
            try
            {
                string[] input = Console.ReadLine().Split();
                if (input.Length != 3)
                    throw new Exception("Нужно ввести ровно 3 числа");
                
                double a = double.Parse(input[0]);
                double c = double.Parse(input[1]);
                double b = double.Parse(input[2]);
                
                return new FuzzyNumber(a, c, b);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}");
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("1. Сложение");
        Console.WriteLine("2. Вычитание");
        Console.WriteLine("3. Умножение");
        Console.WriteLine("4. Деление");
        
        int choice;
        while (true)
        {
            Console.Write("Выберите операцию (1-4): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 4)
                break;
            Console.WriteLine("Ошибка: введите число от 1 до 4");
        }

        try
        {
            Console.WriteLine("\nПервое число:");
            FuzzyNumber num1 = InputFuzzyNumber();
            Console.WriteLine($"Введено: {num1}");

            Console.WriteLine("\nВторое число:");
            FuzzyNumber num2 = InputFuzzyNumber();
            Console.WriteLine($"Введено: {num2}");

            FuzzyNumber result;
            switch (choice)
            {
                case 1:
                    result = num1 + num2;
                    break;
                case 2:
                    result = num1 - num2;
                    break;
                case 3:
                    result = num1 * num2;
                    break;
                case 4:
                    result = num1 / num2;
                    break;
                default:
                    throw new InvalidOperationException("Неизвестная операция");
            }
            
            Console.WriteLine($"\nРезультат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}