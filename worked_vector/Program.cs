using System;

class Vector
{
    protected int size;
    protected double[] data;
    static Random rnd = new Random();
    public int Size { get { return size; } }

    public Vector(int size)
    {
        this.size = size;
        data = new double[size];
    }

    public double[] GetElements()
    {
        return data;
    }

    public Vector(double[] v)
    {
        this.size = v.Length;
        data = new double[size];
        for (int i = 0; i < size; i++) data[i] = v[i];
    }

    public double this[int index]
    {
        get { return data[index]; }
        set { data[index] = value; }
    }

    public int GetSize() { return size; }

    public bool SetElement(double el, int index)
    {
        if (index < 0 || index >= size) return false;
        data[index] = el;
        return true;
    }

    public double GetElement(int index)
    {
        if (index < 0 || index >= size) return default(double);
        return data[index];
    }

    public Vector Copy()
    {
        Vector rez = new Vector(data);
        return rez;
    }

    public override string ToString()
        => $"{{{string.Join(";", this.data)}}}";

    public double Norma1()
    {
        double s = 0;
        for (int i = 0; i < size; i++)
            s += data[i] * data[i];
        return Math.Sqrt(s);
    }

    public double Norma2()
    {
        double s = 0;
        for (int i = 0; i < size; i++)
            if (Math.Abs(data[i]) > s) s = Math.Abs(data[i]);
        return s;
    }

    public double Norma3()
    {
        double s = 0;
        for (int i = 0; i < size; i++)
            s += Math.Abs(data[i]);
        return s;
    }

    public double ScalarMultiply(Vector b)
    {
        if (size != b.size) return 0;
        double s = 0;
        for (int i = 0; i < size; i++)
            s += data[i] * b.data[i];
        return s;
    }

    public Vector MultiplyScalar(double c)
    {
        Vector rez = new Vector(size);
        for (int i = 0; i < size; i++) rez.data[i] = data[i] * c;
        return rez;
    }

    public Vector Normalize()
    {
        Vector rez = new Vector(size);
        double d = Norma1();
        for (int i = 0; i < size; i++)
            if (d != 0) rez.data[i] = data[i] / d; else rez.data[i] = data[i];
        return rez;
    }

    public static Vector NormalizeRandom(int size)
    {
        Vector rez = new Vector(size);
        for (int i = 0; i < size; i++)
            rez.data[i] = (rnd.NextDouble() - 0.5) * 2.0;
        return rez.Normalize();
    }

    public Vector UMinus()
    {
        Vector rez = new Vector(size);
        for (int i = 0; i < size; i++) rez.data[i] = -data[i];
        return rez;
    }

    public Vector Add(Vector c)
    {
        Vector rez = new Vector(size);
        for (int i = 0; i < size; i++) rez.data[i] = data[i] + c.data[i];
        return rez;
    }

    public Vector Minus(Vector c)
    {
        Vector rez = new Vector(size);
        for (int i = 0; i < size; i++) rez.data[i] = data[i] - c.data[i];
        return rez;
    }

    public static Vector operator +(Vector a, Vector b)
    {
        if (a.size == b.size)
        {
            Vector c = new Vector(a.size);
            for (int i = 0; i < a.size; i++)
                c[i] += a[i] + b[i];
            return c;
        }
        return null;
    }

    public static Vector operator -(Vector a, Vector b)
    {
        if (a.size == b.size)
        {
            Vector c = new Vector(a.size);
            for (int i = 0; i < a.size; i++)
                c[i] += a[i] - b[i];
            return c;
        }
        return null;
    }

    public static Vector operator *(Vector a, double c)
    {
        Vector r = new Vector(a.size);
        for (int i = 0; i < a.size; i++)
            r[i] = a[i] * c;
        return r;
    }

    public static Vector operator *(double c, Vector a)
    {
        Vector r = new Vector(a.size);
        for (int i = 0; i < a.size; i++)
            r[i] = a[i] * c;
        return r;
    }

    public static double operator *(Vector a, Vector b)
    {
        if (a.size == b.size)
        {
            double s = 0.0;
            for (int i = 0; i < a.size; i++)
                s += a[i] * b[i];
            return s;
        }
        return Double.NaN;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 }); // Создаем вектор
        Vector v2 = new Vector(new double[] { 4, 5, 6 });
        Console.WriteLine("Вектор v1: " + v1); // Выводим какие векторы у нас создались
        Console.WriteLine("Вектор v2: " + v2);
        Vector sum = v1 + v2; // Складываем вектора
        Console.WriteLine("Сумма v1 и v2: " + sum);
        Vector diff = v1 - v2; // Вычитание вектора
        Console.WriteLine("Разность v1 и v2: " + diff);
        Vector scaled = v1 * 2.5; // Умножение вектора на число
        Console.WriteLine("v1 * 2.5: " + scaled);
        double dotProduct = v1 * v2; // Скалярное произведение
        Console.WriteLine("Скалярное произведение v1 и v2: " + dotProduct);
        Vector normalized = v1.Normalize(); // Нормализация вектора
        Console.WriteLine("Нормализованный v1: " + normalized);
        Console.WriteLine("Норма L2 вектора v1: " + v1.Norma1()); // Нормы вектора 1
        Console.WriteLine("Норма L∞ вектора v1: " + v1.Norma2()); // Нормы вектора 2
        Console.WriteLine("Норма L1 вектора v1: " + v1.Norma3()); // Нормы вектора 3
        Vector randomVector = Vector.NormalizeRandom(3); // Случайный нормализованный вектор
        Console.WriteLine("Случайный нормализованный вектор: " + randomVector);
    }
}