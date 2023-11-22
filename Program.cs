using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2
{
    class Values
    {
        public int Begin { get; set; }
        public int End { get; set; }
    }

    class Program
    {
        static bool Prime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static bool Fib(int a)
        {
            if (Math.Sqrt(5 * a * a + 4) % 1 == 0 || Math.Sqrt(5 * a * a - 4) % 1 == 0) return true;
            else return false;
        }

        static void GeneratePrimes1(object obj)
        {
            Values values = obj as Values;
            for (int i = values.Begin; i <= values.End; i++)
            {
                if (Prime(i))
                {
                    Console.WriteLine(i);
                }
            }
        }

        static void GeneratePrimes2(object obj)
        {
            Values values = obj as Values;
            while (values.Begin >= 2)
            {
                if (Prime(values.Begin))
                {
                    Console.WriteLine(values.Begin);
                }
                values.Begin++;
            }
        }

        static void GenerateFib1(object obj)
        {
            Values values = obj as Values;
            for (int i = values.Begin; i < values.End; i++)
            {
                if (Fib(i))
                {
                    Console.WriteLine("\t" + i);
                }
            }
        }

        static void GenerateFib2(object obj)
        {
            Values values = obj as Values;
            while (values.Begin >= 2)
            {
                if (Fib(values.Begin))
                {
                    Console.WriteLine("\t" + values.Begin);
                }
                values.Begin++;
            }
        }

        static void Main(string[] args)
        {
            int choice1, choice2, choice3 = 1;
            Values values = new Values();
            ParameterizedThreadStart ts1 = new ParameterizedThreadStart(GeneratePrimes1);
            ParameterizedThreadStart ts2 = new ParameterizedThreadStart(GenerateFib1);
            ParameterizedThreadStart ts3 = new ParameterizedThreadStart(GeneratePrimes2);
            ParameterizedThreadStart ts4 = new ParameterizedThreadStart(GenerateFib2);
            Thread thread1 = new Thread(ts1);
            Thread thread2 = new Thread(ts2);
            do
            {
                Console.Write("Будете ли вы выбирать нижнюю границу диапазона (1, 0)? ");
                choice1 = Convert.ToInt32(Console.ReadLine());

                if (choice1 == 0)
                {
                    values.Begin = 2;
                }
                else if (choice1 == 1)
                {
                    Console.Write("Выберите нижнее крайнее значение: ");
                    values.Begin = Convert.ToInt32(Console.ReadLine());
                    if (values.Begin < 2) values.Begin = 2;
                }

                Console.Write("Будете ли вы выбирать верхнюю границу диапазона (1, 0)? ");
                choice2 = Convert.ToInt32(Console.ReadLine());

                if (choice2 == 0)
                {
                    thread1 = new Thread(ts3);
                    thread2 = new Thread(ts4);
                }
                else if (choice2 == 1)
                {
                    Console.Write("Выберите верхнее крайнее значение: ");
                    values.End = Convert.ToInt32(Console.ReadLine());
                }

                Console.Write("\n\n");
                thread1.Start(values);
                thread2.Start(values);

                ConsoleKeyInfo Selects = Console.ReadKey(true);
                if (Selects.Key == ConsoleKey.S)
                {
                    thread1.Abort();
                    thread2.Abort();
                }

                if (thread1.ThreadState == ThreadState.Aborted || thread2.ThreadState == ThreadState.Aborted)
                {
                    Console.Write("\n\nХотите запустить потоки заново (1, 0)? ");
                    choice3 = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine();
            } while (choice3 == 1);
        }
    }
}