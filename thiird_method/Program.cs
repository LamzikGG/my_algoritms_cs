using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
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
                        return c; //Найден корень с достаточной точностью
                    }else if(f(a)*f(c) < 0){ 
                        b = c;
                    }else{
                        a = c;
                    }
                }
                return (a + b) / 2; 
    }
        public static (double, int) method_nuiton(Func<double, double> f, double eps = 1e-6){
        Console.WriteLine("Введите начальное предположение:");
        double x = Convert.ToDouble(Console.ReadLine());
        double h = eps / 2;
        double xPrev = x + 10 * eps;
        int k = 0;
        while (true){
            k++;
            double fx = f(x);
            if (Math.Abs(x - xPrev) < eps)
                return (x, k);
            double dfx = (f(x + h) - fx) / h; // Численная производная
            if (Math.Abs(dfx) < 1e-12)
                throw new InvalidOperationException("Производная близка к нулю решения нету");
            xPrev = x;
            x -= fx / dfx;
        }
    }
    //public static double method_posledovatelnih_priblishenie(){
        
   // }
}
class Rezult{
     public static void Main(string[] args){
        Func<double, double> f = x => x*x*x - x - 2; 
        Console.WriteLine("Введите, какой метод протестить");
        int viborka = Convert.ToInt32(Console.ReadLine());
        switch(viborka){
            case 1:
                Console.WriteLine(Roat.MethodPolovinogoDelenia(f));
                break;
            case 2:
                Console.WriteLine(Roat.method_nuiton(f));
                break;
          //  case 3:
            //    Console.WriteLine(Roat.method_posledovatelnih_priblishenie(f));
        }
    }
}