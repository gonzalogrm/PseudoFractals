using System;

namespace FractalRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            FractalFactory factory = new FractalFactory();
            PseudoFractal composite = 
                factory.BuildToOrder(3);
            
            Console.WriteLine($"Total Sum: {composite.TotalSum(0, composite)}");
            Console.ReadLine();
        }

        //Recursion
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
