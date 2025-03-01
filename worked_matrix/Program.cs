using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using ConsoleChMethod;

namespace ConsoleChMethod
{ // Добавить методы матриц их 10
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
                    // Console.WriteLine(" Индексы вышли за пределы матрицы ");
                    return Double.NaN;
                }
                else
                    return data[i, j];
            }
            set
            {
                if (i < 0 || j < 0 || i >= rows || j >= columns)
                {
                    //Console.WriteLine(" Индексы вышли за пределы матрицы ");
                }
                else
                    data[i, j] = value;
            }
        }
        public Vector GetRow(int r)
        {
            if (r >= 0 && r < rows)
            {
                Vector row = new Vector(columns);
                for (int j = 0; j < columns; j++) row[j] = data[r, j];
                return row;
            }
            return null;
        }
        public Vector GetColumn(int c)
        {
            if (c >= 0 && c < columns)
            {
                Vector column = new Vector(rows);
                for (int i = 0; i < rows; i++) column[i] = data[i, c];
                return column;
            }
            return null;
        }
        public bool SetRow(int index, Vector r)
        {
            if (index < 0 || index > rows) return false;
            if (r.Size != columns) return false;
            for (int k = 0; k < columns; k++) data[index, k] = r[k];
            return true;
        }
        public bool SetColumn(int index, Vector c)
        {
            if (index < 0 || index > columns) return false;
            if (c.Size != rows) return false;
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
        //умножение матрицы на вектор
        public static Vector operator *(Matrix a, Vector b)
        {
            if (a.columns != b.Size) return null;
            Vector r = new Vector(a.rows);
            for (int i = 0; i < a.rows; i++)
            {
                r[i] = a.GetRow(i) * b;
            }
            return r;
        }
        //печать
        public void Print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{data[i, j]} \t");             //t - горизонтальная табуляция
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
            return @s;
        }
        //Транспонированние
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
        
        //Умножение матрицы на чило
        public static Matrix MultByNum(Matrix m, double c)   //Умножаем матрицу на число
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
        //Умножение матриц
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.columns != m2.rows)
            {
                throw new Exception("Количество столбцов первой матрицы не равно количеству строк второй");
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
       
        //Сложение матриц
        public static Matrix operator +(Matrix m1, Matrix m2)     //Сложение матриц
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new Exception("Матрицы не совпадают по размерности");
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
        //Вычитание матриц
        public static Matrix operator -(Matrix m1, Matrix m2)     //Вычитание матриц
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new Exception("Матрицы не совпадают по размерности");
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
        public static Matrix operator -(Matrix m1)     //Отрицание матриц
        {

            Matrix result = new Matrix(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.columns; j++)
                {
                    result[i, j] = -m1[i, j];
                }
            }
            return result;
        }
        public static Matrix EdMatrix(int size)
        {
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
            if (r1 < 0 || r2 < 0 || r1 >= rows || r2 >= rows || (r1 == r2)) return;
            Vector v1 = GetRow(r1);
            Vector v2 = GetRow(r2);
            SetRow(r2, v1);
            SetRow(r1, v2);
        }
        public void SwapColumns(int c1, int c2)
        {
            if (c1 < 0 || c2 < 0 || c1 >= columns || c2 >= columns || (c1 == c2)) return;
            Vector v1 = GetColumn(c1);
            Vector v2 = GetColumn(c2);
            SetColumn(c2, v1);
            SetColumn(c1, v2);
        }
              public static Vector Solve_LU_DOWN_Treug(Matrix a, Vector b)
        {
            int rows = a.rows; int columns = a.columns;
            if (columns != rows || rows != b.Size) return null;
            for (int i = 0; i < rows; i++)
            {
                if (a.data[i, i] == 0) return null;
                for (int j = i + 1; j < rows; j++)
                    if (Math.Abs(a.data[i, j] )> Eps) return null;
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
        public static void gaus(){
            Matrix r = new Matrix(rows, columns);
            for(int i = 0; i < rows; i++){
                for(int j = 0; j < ConsoleChMethodlums; j++){
                    
                }
            }
        }
    }
   
}

class Program{
    public static void Main(string[] args){
        Console.Write(Matrix.gaus);
        Console.Write(Matrix.EdMatrix);
    }
}