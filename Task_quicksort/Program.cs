using System;
using System.Collections.Generic;

namespace Task_quicksort
{
    class Program
    {
        static int count = 0;
        static int comp = 0;
        // Метод генерирует массив
        static int[] generateArr(int size)
        {
            Random rnd = new Random();
            int[] arr = new int[size];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(10,100);
            }
            return arr;
        }
        // Метод выводит массив в форматированном виде в консоль
        static void printArr(int[] arr, string s)
        {
            Console.Write("Array " + s + ": ");
            foreach (var n in arr)
            {

                Console.Write(n + " ");
            }
            Console.WriteLine(";");
        }
        // Метод возвращает опорную точку и перемещает эл-ты
        static int PartSort(int[] array, int start, int end)
        {
            int marker = start;
            for (int i = start; i <= end; i++)
            {
                comp++; // Подсчет сравнений
                if (array[i] <= array[end])
                {
                    int temp = array[marker]; // обмен
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                    count++; // Подсчет обменов
                }
            }
            return marker - 1; // Возврат опорной точки
        }
        // Метод быстрой сортировки с рекурсией
        static void QuickSortRecursion(int[] array, int left, int right)
        {
            if (left >= right) // Прекращение рекурсии
            {
                return;
            }
            int index = PartSort(array, left, right); // Опорная точка
            QuickSortRecursion(array, left, index - 1); // Меньшая(левая) подпоследовательность
            QuickSortRecursion(array, index + 1, right); // Большая(правая) подпоследовательность
        }
        // Метод быстрой сортировки без рекурсии
        static void QuickSortNotR(int[] array, int left, int right)
        {
            Stack<int> s = new Stack<int>(); // Одним их способов реализовать эту сортировку без рекурсии является использование структуры Stack
            s.Push(left);  
            s.Push(right); // Право после входа, так что сначала возьмите направо
            while (s.Count != 0) // Стек не пуст
            {
                right = s.Pop();
                left = s.Pop();

                int index = PartSort(array, left, right);
                if ((index - 1) > left) // левая подпоследовательность
                {
                    s.Push(left);
                    s.Push(index - 1);
                }
                if ((index + 1) < right) // правая подпоследовательность
                {
                    s.Push(index + 1);
                    s.Push(right);
                }
            }
        }
        // Гибридная сортировка (быстрая + вставками)
        static void IntroSortRecursion(int[] array, int left, int right)
        {
            if (left >= right) // Прекращение рекурсии
            {
                return;
            }
            int index = PartSort(array, left, right); // Опорная точка
            if(right - left < 16) // Если подпоследовательность становиться меньше 16 эл-ов, то сортируем вставками
            {
                InsertionSort(array, left, right);
            }
            else // Иначе продолжаем быстрой
            {
                IntroSortRecursion(array, left, index - 1); // Меньшая(левая) подпоследовательность
                IntroSortRecursion(array, index + 1, right); // Большая(правая) подпоследовательность
            }
        }
        // Метод сортировки вставками подпоследовательности
        static void InsertionSort(int[] inArray, int left, int right)
        {
            for (int i = left; i < right + 1; i++)
            {
                int x = inArray[i];
                int j = i;
                while (j > left && inArray[j - 1] > x)
                {
                    comp++;
                    count++;
                    int temp = inArray[j];
                    inArray[j] = inArray[j - 1];
                    inArray[j - 1] = temp;
                    j -= 1;
                }
                inArray[j] = x;
            }
        }
        static void Main(string[] args)
        {
            int count1, count2, count3;
            int comp1, comp2, comp3;
            int[] arr = generateArr(100);
            int[] arr1 = new int[100];
            Array.Copy(arr, arr1, 100);
            int[] arr2 = new int[100];
            Array.Copy(arr, arr2, 100);
            
            printArr(arr1, "before sorting          ");
            Console.WriteLine();
            QuickSortRecursion(arr1, 0, arr1.Length - 1);
            count1 = count;
            comp1 = comp;
            count = 0;
            comp = 0;
            printArr(arr1, "sorted with Recursion   ");
            Console.WriteLine("Число обменов: " + count1);
            Console.WriteLine("Число сравнений: " + comp1 + "\n");

            QuickSortNotR(arr2, 0, arr2.Length - 1);
            count2 = count;
            comp2 = comp;
            count = 0;
            comp = 0;
            printArr(arr2, "sorted without Recursion");
            Console.WriteLine("Число обменов: " + count2);
            Console.WriteLine("Число сравнений: " + comp2 + "\n");

            IntroSortRecursion(arr, 0, arr.Length - 1);
            count3 = count;
            comp3 = comp;
            printArr(arr, "sorted gibrid introsort  ");
            Console.WriteLine("Число обменов: " + count3);
            Console.WriteLine("Число сравнений: " + comp3 + "\n");
        }
    }
}
