using System;

namespace FractalRecursive.FractalClasses
{
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
                return instance;
            }
            else
            {
                //Creamos un objeto en cada iteración
                PseudoFractal iteration = new PseudoFractal();

                //Llamada recursiva.
                //Asociamos recursivamente cada objeto al de la iteración anterior
                //El resultado es un conjunto de objetos con referencias al siguiente
                //Se puede imaginar como anidados uno dentro del otro.
                instance.inner = RecursiveBuild(n - 1, iteration);
            }

            return instance;
        }
    }
}
