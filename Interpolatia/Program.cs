using System;

public struct Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return new Vector2
        {
            // Исправлено: Добавлен вызов через InterpolationDemo
            X = InterpolationDemo.LinearLerp(a.X, b.X, t),
            Y = InterpolationDemo.LinearLerp(a.Y, b.Y, t)
        };
    }

    public override string ToString() => $"({X:0.00}, {Y:0.00})";
}


public class CubicSpline
{
    private readonly double[] _x;
    private readonly double[] _y;
    private readonly double[] _a;
    private readonly double[] _b;
    private readonly double[] _c;
    private readonly double[] _d;

    public CubicSpline(double[] x, double[] y)
    {
        _x = x;
        _y = y;
        int n = x.Length;
        
        // Расчет коэффициентов сплайна
        var h = new double[n - 1];
        var alpha = new double[n - 1];
        var l = new double[n];
        var mu = new double[n];
        var z = new double[n];

        for (int i = 0; i < n - 1; i++)
        {
            h[i] = x[i + 1] - x[i];
            alpha[i] = 3 * ((y[i + 1] - y[i]) / h[i] - (i > 0 ? (y[i] - y[i - 1]) / h[i - 1] : 0));
        }

        l[0] = 1;
        for (int i = 1; i < n - 1; i++)
        {
            l[i] = 2 * (x[i + 1] - x[i - 1]) - h[i - 1] * mu[i - 1];
            mu[i] = h[i] / l[i];
            z[i] = (alpha[i] - h[i - 1] * z[i - 1]) / l[i];
        }

        l[n - 1] = 1;
        _c = new double[n];
        _b = new double[n - 1];
        _d = new double[n - 1];

        for (int j = n - 2; j >= 0; j--)
        {
            _c[j] = z[j] - mu[j] * _c[j + 1];
            _b[j] = (y[j + 1] - y[j]) / h[j] - h[j] * (_c[j + 1] + 2 * _c[j]) / 3;
            _d[j] = (_c[j + 1] - _c[j]) / (3 * h[j]);
        }
        _a = y;
    }

    public double Interpolate(double x)
    {
        int i = Array.BinarySearch(_x, x);
        if (i < 0) i = ~i - 1;
        i = Math.Max(0, Math.Min(i, _x.Length - 2));
        
        double dx = x - _x[i];
        return _a[i] + _b[i] * dx + _c[i] * dx * dx + _d[i] * dx * dx * dx;
    }
}

public class InterpolationDemo
{
    // 1. Линейная интерполяция
    public static float LinearLerp(float a, float b, float t)
    {
        t = Math.Clamp(t, 0, 1);
        return a + (b - a) * t;
    }

    // 2. Интерполяция сплайнами
    public static void SplineInterpolationDemo()
    {
        double[] x = { 1, 2, 3, 4 };
        double[] y = { 1, 4, 9, 16 };
        
        var spline = new CubicSpline(x, y);
        double result = spline.Interpolate(2.5);
        
        Console.WriteLine($"Сплайн-интерполяция в точке 2.5: {result:0.00}");
    }

    // 3. Демонстрация всех методов
    public static void Main()
    {
        // Линейная интерполяция чисел
        float start = 10f;
        float end = 20f;
        float mid = LinearLerp(start, end, 0.5f);
        Console.WriteLine($"Линейная интерполяция: {start} -> {end} при t=0.5: {mid}");

        // Интерполяция векторов
        Vector2 pointA = new Vector2 { X = 1, Y = 2 };
        Vector2 pointB = new Vector2 { X = 4, Y = 6 };
        Vector2 middlePoint = Vector2.Lerp(pointA, pointB, 0.5f);
        Console.WriteLine($"Интерполяция векторов: между {pointA} и {pointB} -> {middlePoint}");

        // Интерполяция сплайнами
        SplineInterpolationDemo();
    }
}