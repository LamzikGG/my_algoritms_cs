using System;

class Sorted
{
    // Пузырьковая сортировка (Bubble Sort)
    public static void Bubble_Sort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Меняем элементы местами
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    // Сортировка вставками (Insertion Sort)
    public static void Sorted_so_Vstavkami(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;

            // Сдвигаем элементы, которые больше key, на одну позицию вправо
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
    }

    // Сортировка выбором (Selection Sort)
    public static void Viborka(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            // Находим минимальный элемент в оставшейся части массива
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            // Меняем найденный минимальный элемент с текущим элементом
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }

    // Сортировка слиянием (Merge Sort)
    public static void Slianie(int[] arr)
    {
        MergeSort(arr, 0, arr.Length - 1);
    }

    private static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            // Рекурсивно сортируем две половины
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);

            // Сливаем отсортированные половины
            Merge(arr, left, mid, right);
        }
    }

    private static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] leftArr = new int[n1];
        int[] rightArr = new int[n2];

        // Копируем данные во временные массивы
        for (int i = 0; i < n1; i++)
            leftArr[i] = arr[left + i];
        for (int j = 0; j < n2; j++)
            rightArr[j] = arr[mid + 1 + j];

        // Сливаем временные массивы обратно в arr
        int i1 = 0, i2 = 0;
        int k = left;
        while (i1 < n1 && i2 < n2)
        {
            if (leftArr[i1] <= rightArr[i2])
            {
                arr[k] = leftArr[i1];
                i1++;
            }
            else
            {
                arr[k] = rightArr[i2];
                i2++;
            }
            k++;
        }

        // Копируем оставшиеся элементы leftArr, если они есть
        while (i1 < n1)
        {
            arr[k] = leftArr[i1];
            i1++;
            k++;
        }

        // Копируем оставшиеся элементы rightArr, если они есть
        while (i2 < n2)
        {
            arr[k] = rightArr[i2];
            i2++;
            k++;
        }
    }

    // Быстрая сортировка (Quick Sort)
    public static void Fast_Sorted(int[] arr)
    {
        QuickSort(arr, 0, arr.Length - 1);
    }

    private static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // Находим индекс опорного элемента
            int pi = Partition(arr, low, high);

            // Рекурсивно сортируем элементы до и после опорного
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high]; // Опорный элемент
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                // Меняем элементы местами
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // Меняем опорный элемент с элементом на позиции i+1
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }

    // Сортировка кучей (Heap Sort)
    public static void Sorted_Kuchei(int[] arr)
    {
        int n = arr.Length;

        // Построение кучи (перегруппировка массива)
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        // Извлекаем элементы из кучи по одному
        for (int i = n - 1; i > 0; i--)
        {
            // Перемещаем текущий корень в конец
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // Вызываем heapify на уменьшенной куче
            Heapify(arr, i, 0);
        }
    }

    private static void Heapify(int[] arr, int n, int i)
    {
        int largest = i; // Инициализируем наибольший элемент как корень
        int left = 2 * i + 1; // Левый дочерний элемент
        int right = 2 * i + 2; // Правый дочерний элемент

        // Если левый дочерний элемент больше корня
        if (left < n && arr[left] > arr[largest])
            largest = left;

        // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
        if (right < n && arr[right] > arr[largest])
            largest = right;

        // Если самый большой элемент не корень
        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            // Рекурсивно преобразуем в двоичную кучу затронутое поддерево
            Heapify(arr, n, largest);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[] arr = { 64, 34, 25, 12, 22, 11, 90 };

        Console.WriteLine("Исходный массив:");
        PrintArray(arr);

        // Пример использования сортировок
        Sorted.Bubble_Sort(arr);
        Console.WriteLine("После пузырьковой сортировки:");
        PrintArray(arr);

        int[] arr2 = { 64, 34, 25, 12, 22, 11, 90 };
        Sorted.Sorted_so_Vstavkami(arr2);
        Console.WriteLine("После сортировки вставками:");
        PrintArray(arr2);

        int[] arr3 = { 64, 34, 25, 12, 22, 11, 90 };
        Sorted.Viborka(arr3);
        Console.WriteLine("После сортировки выбором:");
        PrintArray(arr3);

        int[] arr4 = { 64, 34, 25, 12, 22, 11, 90 };
        Sorted.Slianie(arr4);
        Console.WriteLine("После сортировки слиянием:");
        PrintArray(arr4);

        int[] arr5 = { 64, 34, 25, 12, 22, 11, 90 };
        Sorted.Fast_Sorted(arr5);
        Console.WriteLine("После быстрой сортировки:");
        PrintArray(arr5);

        int[] arr6 = { 64, 34, 25, 12, 22, 11, 90 };
        Sorted.Sorted_Kuchei(arr6);
        Console.WriteLine("После сортировки кучей:");
        PrintArray(arr6);
    }

    static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}