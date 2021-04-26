using System;
using System.Collections.Generic;
using System.Diagnostics;

using FractalRecursive.FractalClasses;

namespace FractalRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            FractalBasicTest();
            //TupleDictionaryTest_1();
            Console.ReadLine();
        }

        public static void FractalBasicTest()
        {
            //Factorial de 3:
            Console.WriteLine($"Factorial de 3: {FactorialOneLine(3)}");
            Console.WriteLine("\n-------------\n");

            //Sucesión de Fibonacci:
            Console.WriteLine("10 términos de la Sucesión de Fibonacci:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(FibonacciOneLine(i));
            }
            Console.WriteLine("\n-------------\n");


            //Objectos recursivamente compuestos
            FractalFactory factory = new FractalFactory();
            PseudoFractal composite =
                factory.BuildToOrder(3);

            Console.WriteLine($"Total Sum: {composite.TotalSum(0, composite)}");
        }

        public static void FractalDictionaryTest_0()
        {
            FractalDictionary fd = new FractalDictionary();

            List<object> coordinates = new List<object>();
            coordinates = new List<object> { 1, 2, 3 };
            fd.Add(coordinates, "Value_123");
            coordinates = new List<object> { 1, 2, 4 };
            fd.Add(coordinates, "Value_124");

            Console.WriteLine("TryGet: " + fd.TryGet(new List<object> { 1, 2, 4 }));
            Console.WriteLine("TryGet: " + fd.TryGet(new List<object> { 2, 2, 4 }));
            Console.WriteLine("TryGet: " + fd.TryGet(new List<object> { 1, 2, 6 }));

            Console.WriteLine($"Total Dict: {FractalDictionary.totalInnerDictionaries}");
        }

        public static void FractalDictionaryTest_1()
        {
            FractalDictionary fd = new FractalDictionary();
            Stopwatch sw = new Stopwatch();

            List<object> coordinates;

            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            coordinates = new List<object> { i, j, k, m };
                            fd.Add(coordinates, $"Value{i}{j}{k}{m}");
                        }
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Add ms: " + sw.ElapsedTicks);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            coordinates = new List<object> { i, j, k, m };
                            sw.Start();
                            object o = fd.TryGet(new List<object> { i, j, k, m });
                            sw.Stop();
                            Console.WriteLine($"TryGet: {i}{j}{k}{m} | " + o);
                            Console.WriteLine("ms: " + sw.ElapsedTicks);
                            sw.Reset();
                        }
                    }
                }
            }

            Console.WriteLine($"Total Dict: {FractalDictionary.totalInnerDictionaries}");
        }

        public static void TupleDictionaryTest_1()
        {
            // Declare
            Stopwatch sw = new Stopwatch();
            var test = new Dictionary<(object, object, object, object), object>();
            List<object> coordinates;

            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            test.Add((i, j, k, m), $"Value{i}{j}{k}{m}");
                        }
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Add ms: " + sw.ElapsedTicks);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            coordinates = new List<object> { i, j, k, m };
                            sw.Start();
                            object o = test[(i, j, k, m)];
                            sw.Stop();
                            Console.WriteLine($"TryGet: {i}{j}{k}{m} | " + o);
                            Console.WriteLine("ms: " + sw.ElapsedTicks);
                            sw.Reset();
                        }
                    }
                }
            }

            Console.WriteLine($"Total Dict: {FractalDictionary.totalInnerDictionaries}");
        }


        //Recursion
        /*
            //En cada return haremos una llamada recursiva num * Factorial(num - 1);
            Factorial(3) -> 3*Factorial(2)
            Factorial(2) -> 2*Factorial(1)
            Factorial(1) -> 1*Factorial(0)
            Factorial(0) -> 1;
 
            //Al llegar al caso base, el programa devuelve en orden inverso las llamadas del return al caso anterior:
            Factorial(0) -> 1
            Factorial(1) -> 1*Factorial(0) -> 1
            Factorial(2) -> 2*Factorial(1) -> 2*1*Factorial(0) -> 2*1
            Factorial(3) -> 3*Factorial(2) -> 3*Factorial(2) -> 3*2*Factorial(1) -> 2*1*Factorial(0) -> 3*2*1
 
            //Por tanto
            Factorial(3) ->  3*2*1
         */
        public static long Factorial(int num)
        {
            //Caso base.
            //Debemos definir un punto de parada para evitar un Stack Overflow.
            if (num == 0)
            {
                return 1;
            }
            //La función se llama recursivamente a sí misma
            //La siguiente llamada utiliza number-1
            return num * Factorial(num - 1);
        }

        //El operador ternario permite hacer toda la función recursiva en una única línea
        public static long FactorialOneLine(int num)
        {
            return (num == 1 || num == 0) ? 1 : num * FactorialOneLine(num - 1);
        }

        //Sucesión de Fibonacci
        public static int Fibonacci(int n)
        {
            //Casos base
            if (n == 0)
                return n;
            if (n == 1)
                return n;

            //La función se llama recursivamente a sí misma
            return (Fibonacci(n - 1) + Fibonacci(n - 2));
        }

        //El operador ternario permite hacer toda la función recursiva en una única línea
        public static int FibonacciOneLine(int n)
        {
            return (n == 1 || n == 0) ? n : (FibonacciOneLine(n - 1) + FibonacciOneLine(n - 2));
        }
    }
}


    
