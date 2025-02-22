using System;
//Нахождения корня скалярного уравнения
//Сделать метод перестановки 
class Roat{
    public static double MethodPolovinogoDelenia(Func<double, double>f, double eps = 1e-6){
            double a = Convert.ToDouble(Console.ReadLine());
            double b = Convert.ToDouble(Console.ReadLine());
            double c = 0;
            if(f(a)*f(b) >= 0){
                Console.WriteLine("Абоба");
                return double.NaN;
            }
                while(((b - a)/2) > eps){
                    c = ((b + a)/2);
                    if((f(c) == 0) || Math.Abs(f(c)) < eps){
                        return c; //НАйден корень с достаточной точностью
                    }else if(f(a)*f(c) < 0){ 
                        b = c;
                    }else{
                        a = c;
                    }
                }
                return (a + b) / 2; 
            }
    }

class Rezult{
     public static void Main(string[] args){
        Func<double, double> f = x => x*x*x - x - 2; 
        double rezut = Roat.MethodPolovinogoDelenia(f);
        Console.WriteLine(rezut);
    }
}