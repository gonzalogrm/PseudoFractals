using System;

namespace FractalRecursive
{
    class Program
    {
        static void Main(string[] args)
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
            Console.ReadLine();
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

    //PseudoFractal
    public class PseudoFractal
    {
        //Propiedades
        public int num { get; set; }

        //El objecto incluye una instancia de su misma clase
        public PseudoFractal inner { get; set; }

        //Constructor
        public PseudoFractal()
        {
            num = 1;
            //Por defecto, el objeto no contiene instancias internas.
            inner = null;
        }

        //Método para calcular la suma de toda la estructura recursiva.
        public int TotalSum(int res, PseudoFractal instance)
        {
            //Caso base
            //Si es null, la instancia ya no contiene más instancias
            if (instance.inner == null)
            {
                return res += instance.num;
            }
            else
            {
                //Sumamos el num de la instancia
                res += instance.num;

                //Pasamos la instancia interna a la próxima llamada recursiva
                PseudoFractal iteration = instance.inner;
                return TotalSum(res, iteration);
            }
        }
    }

    public class FractalFactory
    {
        //Función pública para recibir request
        public PseudoFractal BuildToOrder(int order)
        {
            return RecursiveBuild(order, new PseudoFractal());
        }

        //Función recursiva para construir objetos pseudofractales a profundidad n. 
        private PseudoFractal RecursiveBuild(int n, PseudoFractal instance)
        {
            if (n == 1)
            {
                return new PseudoFractal();
            }
            else
            {
                //Creamos un objeto en cada iteración
                PseudoFractal iteration = new PseudoFractal();

                //Llamada recursiva.
                //Asociamos recursivamente cada objeto al de la iteración anterior
                //El resultado es un conjunto de objetos anidados uno dentro del otro.
                instance.inner = RecursiveBuild(n - 1, iteration);
            }

            return instance;
        }
    }
}
