using System;

namespace ConsoleChMethod
{
    class Matrix
    {
        protected int rows, columns;
        protected double[,] data;
        private const double Eps = 0.000001;

        public Matrix(int r, int c)
        {
            this.rows = r; this.columns = c;
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++) data[i, j] = 0;
        }

        public Matrix(double[,] mm)
        {
            this.rows = mm.GetLength(0); this.columns = mm.GetLength(1);
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    data[i, j] = mm[i, j];
        }

        public Matrix(Matrix M)
        {
            this.rows = M.rows; this.columns = M.columns;
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    data[i, j] = M[i, j];
        }

        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= rows || j >= columns)
                {
                    throw new IndexOutOfRangeException("Индексы вышли за пределы матрицы");
                }
                return data[i, j];
            }
            set
            {
                if (i < 0 || j < 0 || i >= rows || j >= columns)
                {
                    throw new IndexOutOfRangeException("Индексы вышли за пределы матрицы");
                }
                data[i, j] = value;
            }
        }

        public Vector GetRow(int r)
        {
            if (r < 0 || r >= rows) throw new IndexOutOfRangeException("Индекс строки вне диапазона");
            Vector row = new Vector(columns);
            for (int j = 0; j < columns; j++) row[j] = data[r, j];
            return row;
        }

        public Vector GetColumn(int c)
        {
            if (c < 0 || c >= columns) throw new IndexOutOfRangeException("Индекс столбца вне диапазона");
            Vector column = new Vector(rows);
            for (int i = 0; i < rows; i++) column[i] = data[i, c];
            return column;
        }

        public bool SetRow(int index, Vector r)
        {
            if (index < 0 || index >= rows || r.Size != columns) return false;
            for (int k = 0; k < columns; k++) data[index, k] = r[k];
            return true;
        }

        public bool SetColumn(int index, Vector c)
        {
            if (index < 0 || index >= columns || c.Size != rows) return false;
            for (int k = 0; k < rows; k++) data[k, index] = c[k];
            return true;
        }

        public double Norma1()
        {
            double s = 0;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    s += data[i, j] * data[i, j];
            return Math.Sqrt(s);
        }

        public double Norma2()
        {
            double max = 0, s = 0;
            for (int i = 0; i < rows; i++)
            {
                s = 0;
                for (int j = 0; j < columns; j++)
                    s += Math.Abs(data[i, j]);
                if (s > max) max = s;
            }
            return max;
        }

        public double Norma3()
        {
            double max = 0, s = 0;
            for (int j = 0; j < columns; j++)
            {
                s = 0;
                for (int i = 0; i < rows; i++)
                    s += Math.Abs(data[i, j]);
                if (s > max) max = s;
            }
            return max;
        }

        public static Vector operator *(Matrix a, Vector b)
        {
            if (a.columns != b.Size) throw new ArgumentException("Количество столбцов матрицы не совпадает с размерностью вектора");
            Vector r = new Vector(a.rows);
            for (int i = 0; i < a.rows; i++)
            {
                r[i] = a.GetRow(i) * b;
            }
            return r;
        }

        public void Print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{data[i, j]} \t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        public override string ToString()
        {
            string s = "{\n";
            for (int i = 0; i < rows; i++)
                s += GetRow(i).ToString() + "\n";
            s += "}";
            return s;
        }

        public Matrix Transpose()
        {
            Matrix transposeMatrix = new Matrix(columns, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposeMatrix.data[j, i] = data[i, j];
                }
            }
            return transposeMatrix;
        }

        public static Matrix MultByNum(Matrix m, double c)
        {
            Matrix result = new Matrix(m.rows, m.columns);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++)
                {
                    result[i, j] = m[i, j] * c;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix m, double c)
        {
            return Matrix.MultByNum(m, c);
        }

        public static Matrix operator *(double c, Matrix m)
        {
            return Matrix.MultByNum(m, c);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.columns != m2.rows)
            {
                throw new ArgumentException("Количество столбцов первой матрицы не равно количеству строк второй");
            }
            Matrix result = new Matrix(m1.rows, m2.columns);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < m1.columns; k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new ArgumentException("Матрицы не совпадают по размерности");
            }
            Matrix result = new Matrix(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new ArgumentException("Матрицы не совпадают по размерности");
            }
            Matrix result = new Matrix(m1.rows, m2.columns);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix m1){
            Matrix result = new Matrix(m1.rows, m1.columns);
            for (int i = 0; i < m1.rows; i++){
                for (int j = 0; j < m1.columns; j++)
                {
                    result[i, j] = -m1[i, j];
                }
            }
            return result;
        }

        public static Matrix EdMatrix(int size){
            Matrix tm = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    tm.data[i, j] = 0.0;
                tm.data[i, i] = 1.0;
            }
            return tm;
        }

        public void SwapRows(int r1, int r2)
        {
            if (r1 < 0 || r2 < 0 || r1 >= rows || r2 >= rows || (r1 == r2)) throw new ArgumentException("Некорректные индексы строк");
            Vector v1 = GetRow(r1);
            Vector v2 = GetRow(r2);
            SetRow(r2, v1);
            SetRow(r1, v2);
        }

        public void SwapColumns(int c1, int c2)
        {
            if (c1 < 0 || c2 < 0 || c1 >= columns || c2 >= columns || (c1 == c2)) throw new ArgumentException("Некорректные индексы столбцов");
            Vector v1 = GetColumn(c1);
            Vector v2 = GetColumn(c2);
            SetColumn(c2, v1);
            SetColumn(c1, v2);
        }

        public static Vector Solve_LU_DOWN_Treug(Matrix a, Vector b)
        {
            int rows = a.rows; int columns = a.columns;
            if (columns != rows || rows != b.Size) throw new ArgumentException("Некорректные размеры матрицы или вектора");
            for (int i = 0; i < rows; i++)
            {
                if (a.data[i, i] == 0) throw new ArgumentException("Матрица содержит нулевые элементы на диагонали");
                for (int j = i + 1; j < rows; j++)
                    if (Math.Abs(a.data[i, j]) > Eps) throw new ArgumentException("Матрица не является нижней треугольной");
            }
            Vector x = new Vector(rows);
            x[0] = b[0] / a.data[0, 0];
            for (int i = 1; i < rows; i++)
            {
                double s = 0;
                for (int k = 0; k < i; k++)
                    s += a.data[i, k] * x[k];
                x[i] = (b[i] - s) / a.data[i, i];
            }
            return x;
        }

        public Matrix Copy()
        {
            Matrix r = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++) r[i, j] = data[i, j];
            return r;
        }
    }

    class Program
    {
        public static void Main()
        {
            // Пример использования
            double[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix m = new Matrix(data);
            m.Print();

            Vector v = new Vector(new double[] { 5, 6 });
            Vector result = m * v;
            result.Print();
        }
    }
}