using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание массива

            int n = 100;
            bool ok = false;
            while (!ok)
            {
                Console.WriteLine("Введите количество элементов в массиве:");
                try
                {
                    n = Convert.ToInt32(Console.ReadLine());
                    if (n <= 0)
                    {
                        continue;
                    }
                    ok = true;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            int[] a = new int[n];
            a = MakeArray(n);
            PrintArray(a, "Исходный массив:");

            // Меню

            MakeMenu(a);

        }

        private static void MakeMenu(int[] a)
        {
            int menuItem = 0;
            int countOfAvailableElementsToInsert = 0;
            bool exit = false;
            do
            {
                do
                {
                    Console.WriteLine("Введите номер требуемого пункта меню: ");
                    Console.WriteLine("1 - удаление минимального элемента из массива");
                    Console.WriteLine("2 - добавление N элементов после элемента с номером K");
                    Console.WriteLine("3 - перестановка элементов с четными и нечетными номерами");
                    Console.WriteLine("4 - поиск элемента с заданным ключом (значением)");
                    Console.WriteLine("5 - сортировка простым выбором");
                    Console.WriteLine("0 - выход");

                    try
                    {
                        menuItem = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        continue;
                    }


                    switch (menuItem)
                    {
                        case 1:
                            //Удаление минимального элемента из массива

                            int indexMin = FindIndexOfMinElement(a);
                            if (countOfAvailableElementsToInsert <= a.Length - countOfAvailableElementsToInsert)
                            {
                                a = DeleteMinElement(a, indexMin);
                                countOfAvailableElementsToInsert++;
                                PrintDeletedArray(a, countOfAvailableElementsToInsert);

                            }
                            else
                            {
                                Console.WriteLine("Всё удалено");
                            }
                            break;
                        case 2:
                            //Добавление N элементов, после элемента с номером K

                            Console.WriteLine("Введите количество элементов для добавления в массив:");
                            int N = 0;
                            try
                            {
                                N = Convert.ToInt32(Console.ReadLine());
                                if (N <= 0)
                                {
                                    continue;
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }

                            if (N <= 0 || N > countOfAvailableElementsToInsert)
                            {
                                Console.WriteLine("Невозможно вставить! Нет места или количество элементов не положительное.");
                                break;
                            }

                            int K = 0;

                            //обработка массива с количеством элементов больше 1
                            if (a.Length - countOfAvailableElementsToInsert >= 1)
                            {
                                Console.WriteLine("Введите номер элемента, с которого начать добавление:");
                                try
                                {
                                    K = Convert.ToInt32(Console.ReadLine());
                                    if (K < 0 || K > a.Length - countOfAvailableElementsToInsert - 1)
                                    {
                                        Console.WriteLine("Нет элемента с таким номером");
                                        continue;
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            //обработка массива с 1 элементом
                            else if (a.Length - countOfAvailableElementsToInsert == 1)
                            {
                                K = 0;
                            }
                            //обработка пустого массива
                            else if (a.Length - countOfAvailableElementsToInsert <= 0)
                            {
                                Console.WriteLine("Вы удалили все элементы в массиве. Для вставки нужен хотя бы один элемент, так как вставка осуществляется после него.");
                            }

                            a = InsertElements(N, K, a, ref countOfAvailableElementsToInsert);
                            PrintArray(a, $"Массив с добавлением {N} элемента(ов) после элемента с номером {K}:");
                            break;
                        case 3:
                            //перестановка элементов с четными и нечетными номерами

                            a = ChangeEvenAndOddElements(a);
                            PrintArray(a, "Массив, в котором переставлены элементы с четными и нечетными номерами:");
                            break;
                        case 4:
                            //поиск элемента с заданным ключом (значением)

                            bool ok = false;
                            int key = 0;
                            while (!ok)
                            {
                                Console.WriteLine("Введите ключ (значение) элемента, который требуется найти");
                                try
                                {
                                    key = Convert.ToInt32(Console.ReadLine());
                                    ok = true;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            bool FoundElement = false;
                            int indexOfFoundElement = -1;
                            FoundElement = FindElement(a, key, ref indexOfFoundElement);
                            if (FoundElement)
                            {
                                Console.WriteLine($"Элемент {key} найден на позиции {indexOfFoundElement}");
                            }
                            else
                            {
                                Console.WriteLine($"Элемент {key} найден на позиции {indexOfFoundElement}");
                            }
                            break;
                        case 5:
                            //сортировка простым выбором

                            a = SelectionSort(a);
                            PrintArray(a, "Отсортированный массив:");
                            break;
                        case 0:
                            //Выход

                            exit = true;
                            break;
                    }
                } while (menuItem < 0 || menuItem > 5);
            } while (!exit);
        }

        private static int[] SelectionSort(int[] a)
        {
            int min, n_min, j;
            for (int i = 0; i < a.Length - 1; i++)
            {
                min = a[i]; n_min = i;
                for (j = i + 1; j < a.Length; j++)
                    if (a[j] < min)
                    {
                        min = a[j];
                        n_min = j;
                    }
                a[n_min] = a[i];
                a[i] = min;
            }
            return a;
        }

        private static bool FindElement(int[] a, int key, ref int indexOfFoundElement)
        {

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == key)
                {
                    indexOfFoundElement = i;
                    return true;
                }
            }
            return false;

        }

        private static int[] ChangeEvenAndOddElements(int[] a)
        {
            int temp;
            for (int i = 0; i < a.Length - 1; i += 2)
            {
                temp = a[i];
                a[i] = a[i + 1];
                a[i + 1] = temp;
            }

            return a;
        }

        private static int[] InsertElements(int n, int k, int[] a, ref int countOfAvailableElementsToInsert)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (i == k)
                {
                    for (int count = 0; count < n; count++)
                    {
                        for (int j = a.Length - 1; j > k; j--)
                        {
                            a[j] = a[j - 1];
                        }
                        int number = GenerateNumber();
                        a[k + 1] = number;
                        countOfAvailableElementsToInsert--;
                    }
                }
            }
            return a;
        }

        private static int GenerateNumber()
        {
            int number = 0;
            do
            {
                Console.WriteLine("Введите значение добавляемого элемента (больше или равно 0 и меньше 100)");
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    continue;
                }

            } while (number < 0 || number >= 100);
            return number;
        }

        private static int[] MakeArray(int n)
        {
            int[] arr = new int[n];
            Random rnd = new Random();
            int number = rnd.Next(100);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = number;
                number = rnd.Next(100);
            }
            return arr;
        }

        private static void PrintArray(int[] a, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }

        private static void PrintDeletedArray(int[] a, int countOfDeletedElements)
        {
            Console.WriteLine("Массив с удаленным минимальным элементом:");
            for (int i = 0; i < a.Length - countOfDeletedElements; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }

        private static int FindIndexOfMinElement(int[] a)
        {
            int currentMin = 100;
            int indexMin = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < currentMin)
                {
                    currentMin = a[i];
                    indexMin = i;
                }
            }
            return indexMin;
        }

        private static int[] DeleteMinElement(int[] a, int indexMin)
        {
            for (int i = indexMin; i < a.Length - 1; i++)
            {
                a[i] = a[i + 1];
            }
            return a;
        }

    }
}
