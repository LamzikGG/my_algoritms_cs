using System;
using System.Security.Cryptography.X509Certificates;
//Нахождения корня скалярного уравнения
//Сделать метод перестановки 
class Roat{
Func<double, double>f = x => x * x - 4;
    public static double method_polovinogo_delenia(Func<double, double>f, double a, double b, double eps = 1e-6){
        double fa = f(a);
        double fb = f(b);
        double c = 0;
        while((b - a) > eps){
            c = ((b + a) / 2);
            double fc = f(c);
            if(fc*fa <= 0){
                b = c;
                fb = fc;
            }else{
                a = c;
                fa = fc;
            }
        }
        return c;
    }
}
 //   public static void method_nuiton(Func<double, double>f, Func<double>df, double x0){
   //     double eps = 0;
     //   double x = x0;
       // while(Math.Abs(f(x)) > eps){
         //  double dfValue = df(x);
            
        //}
    //}
   // public static void method_posledovatelnih_priblishenie(double leftBorder,double rightBorder,double eps = 1e-6,Func<double,double>func){
        
    //}
//}
class Rezult(){
     public static void Main(string[] args){
        Func<double, double>f = x => x * x - 4;
        double a = Convert.ToDouble(Console.ReadLine());
        double b = Convert.ToDouble(Console.ReadLine());
        Roat.method_polovinogo_delenia(f, a, b);
    }
}

