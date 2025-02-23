using System;
class vvod_matrix{
    public static void matrix_in_keyboards(){
    int shirina = Convert.ToInt32(Console.ReadLine());
    int dlina = Convert.ToInt32(Console.ReadLine());
    int[,]matrix = new int[shirina, dlina];
    for(int i = 0; i < dlina ;i++){
        for(int j = 0; j < shirina ;j++){
            matrix[i, j] = Convert.ToInt32(Console.ReadLine());
        }
    }
}
//Миноры матрицы будут рандомные
public static void matrix_random(){
    int shirina = Convert.ToInt32(Console.ReadLine());
    int dlina = Convert.ToInt32(Console.ReadLine());
    int[,]matrix = new int[shirina, dlina];
    Random random = new Random();
    Console.WriteLine("Вы хотите ввести диапазон чисел, еоторые будут стоять в минорах матриц: Да или Нет");
    string otvet = Console.ReadLine();
    if(otvet == "Да"){
        for(int i = 0; i < dlina ;i++){
            for(int j = 0; j < shirina ;j++){
                matrix[i, j] = random.Next();
        }
    }
    }else if(otvet == "Нет"){
        for(int i = 0; i < dlina ;i++){
            for(int j = 0; j < shirina ;j++){
                matrix[i, j] = random.Next(); // .Next будет положительным от 0 до ToInt32
        }
    }
    }
}
}
class Program{
    public static void Main(){
        Console.WriteLine(vvod_matrix.matrix_in_keyboards);
        Console.WriteLine(vvod_matrix.matrix_random);
    }
}