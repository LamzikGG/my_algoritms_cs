using System;

class Roat
{
    // Метод половинного деления
    public static double MethodPolovinogoDelenia(Func<double, double> f, double a, double b, double eps = 1e-6)
    {
        if (f(a) * f(b) >= 0)
        {
            Console.WriteLine("Ошибка: f(a) и f(b) должны иметь разные знаки.");
            return double.NaN;
        }

        double c = 0;
        while ((b - a) / 2 > eps)
        {
            c = (a + b) / 2;
            if (Math.Abs(f(c)) < eps)
            {
                return c; // Корень с хорошей точностью
            }
            else if (f(a) * f(c) < 0)
            {
                b = c;
            }
            else
            {
                a = c;
            }
        }
        return (a + b) / 2;
    }

    // Метод Ньютона
    public static (double, int) method_nuiton(Func<double, double> f, double x0, double eps = 1e-6)
    {
        double x = x0;
        double h = eps / 2;
        double xPrev = x + 10 * eps;
        int k = 0;

        while (true)
        {
            k++;
            double fx = f(x);
            if (Math.Abs(x - xPrev) < eps)
                return (x, k);

            double dfx = (f(x + h) - fx) / h;
            if (Math.Abs(dfx) < 1e-12)
                throw new InvalidOperationException("Производная близка к нулю. Метод не сошелся.");

            xPrev = x;
            x -= fx / dfx;
        }
    }
    public static (double, int) method_posledovatelnih_priblishenie(Func<double, double> f, double x0, int maxIterations, double eps = 1e-6)
    {
        double x = x0;
        int iterations = 0;

        while (iterations < maxIterations)
        {
            double xNew = f(x);
            if (Math.Abs(xNew - x) < eps)
                return (xNew, iterations);

            x = xNew;
            iterations++;
        }

        throw new InvalidOperationException($"Метод не сошелся за {maxIterations} итераций.");
    }
}

class Rezult
{
    public static void Main(string[] args)
    {
        Func<double, double> f = x => x * x * x - x - 2;

        Console.WriteLine("Введите, какой метод протестить:");
        Console.WriteLine("1 - Метод половинного деления");
        Console.WriteLine("2 - Метод Ньютона");
        Console.WriteLine("3 - Метод последовательных приближений");
        int viborka = Convert.ToInt32(Console.ReadLine());

        try
        {
            switch (viborka)
            {
                case 1:
                    Console.WriteLine("Введите a и b:");
                    double a = Convert.ToDouble(Console.ReadLine());
                    double b = Convert.ToDouble(Console.ReadLine());
                    double root1 = Roat.MethodPolovinogoDelenia(f, a, b);
                    Console.WriteLine($"Корень: {root1}");
                    break;

                case 2:
                    Console.WriteLine("Введите начальное предположение:");
                    double x0 = Convert.ToDouble(Console.ReadLine());
                    var result2 = Roat.method_nuiton(f, x0);
                    Console.WriteLine($"Корень: {result2.Item1}, Итераций: {result2.Item2}");
                    break;

                case 3:
                    Console.WriteLine("Введите начальное предположение и максимальное количество итераций:");
                    double x1 = Convert.ToDouble(Console.ReadLine());
                    int maxIterations = Convert.ToInt32(Console.ReadLine());
                    var result3 = Roat.method_posledovatelnih_priblishenie(f, x1, maxIterations);
                    Console.WriteLine($"Корень: {result3.Item1}, Итераций: {result3.Item2}");
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}