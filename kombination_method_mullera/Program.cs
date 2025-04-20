using System;
using System.Numerics;

public class MullerMethod
{
    // Функция, корни которой мы ищем (пример: f(x) = x^3 - 2x^2 + x - 2)
    public static Complex F(Complex x)
    {
        return Complex.Pow(x, 3) - 2 * Complex.Pow(x, 2) + x - 2;
    }

    public static Complex FindRoot(Complex x0, Complex x1, Complex x2, double tolerance = 1e-10, int maxIterations = 100)
    {
        Complex h1 = x1 - x0;
        Complex h2 = x2 - x1;
        Complex delta1 = (F(x1) - F(x0)) / h1;
        Complex delta2 = (F(x2) - F(x1)) / h2;
        Complex d = (delta2 - delta1) / (h2 + h1);
        Complex x3 = new Complex();

        for (int i = 0; i < maxIterations; i++)
        {
            // Коэффициенты квадратного уравнения
            Complex b = delta2 + h2 * d;
            Complex D = Complex.Sqrt(Complex.Pow(b, 2) - 4 * F(x2) * d);
            
            // Выбираем знак для максимального знаменателя
            Complex denominator = (Complex.Abs(b + D) > Complex.Abs(b - D)) ? (b + D) : (b - D);
            
            // Вычисляем следующий x
            x3 = x2 - 2 * F(x2) / denominator;
            
            // Проверка на сходимость
            if (Complex.Abs(x3 - x2) < tolerance)
                return x3;
            
            // Обновляем точки для следующей итерации
            x0 = x1;
            x1 = x2;
            x2 = x3;
            h1 = x1 - x0;
            h2 = x2 - x1;
            delta1 = (F(x1) - F(x0)) / h1;
            delta2 = (F(x2) - F(x1)) / h2;
            d = (delta2 - delta1) / (h2 + h1);
        }
        
        throw new Exception($"Метод не сошелся за {maxIterations} итераций");
    }

    public static void Main()
    {
        try
        {
            Complex x0 = new Complex(0, 0);
            Complex x1 = new Complex(1, 0);
            Complex x2 = new Complex(2, 0);
            
            Complex root = FindRoot(x0, x1, x2);
            Console.WriteLine($"Найденный корень: {root}");
            Console.WriteLine($"Проверка F(root) = {F(root)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}