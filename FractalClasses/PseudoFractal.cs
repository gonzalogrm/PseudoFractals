using System;
using System.Runtime.InteropServices;

namespace FractalRecursive.FractalClasses
{
    public class PseudoFractal
    {
        //Propiedades
        private static int count = 0;
        public int ID { get; set; }
        public int num { get; set; }
        public int Hash { get; set; }
        public int Address { get; set; }

        //El objecto incluye una referencia a una instancia de su misma clase
        public PseudoFractal inner { get; set; }

        //Constructor
        public PseudoFractal()
        {
            //Instance ID
            ID = count++;
            //Variable
            num = 1;
            //Por defecto, el objeto no contiene instancias internas.
            inner = null;
            //Guardar el HashCode
            Hash = GetHashCode();
            //Guardar el Memory Address
            Address = MemoryAddress();
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

        public int MemoryAddress()
        {
            GCHandle objHandle = GCHandle.Alloc(this, GCHandleType.WeakTrackResurrection);
            int address = GCHandle.ToIntPtr(objHandle).ToInt32();
            return address;
        }
    }
}
