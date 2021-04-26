using System;
using System.Collections.Generic;

namespace FractalRecursive.FractalClasses
{
    /// <summary>
    /// Experimental FractalDictionary Class. Not implemented completely
    /// </summary>
    public class FractalDictionary
    {
        public static int totalInnerDictionaries = 0;
        public object content { get; set; }
        public Dictionary<object, FractalDictionary> innerDictionary { get; set; }

        public FractalDictionary()
        {
            innerDictionary = new Dictionary<object, FractalDictionary>();
            totalInnerDictionaries++;
        }

        public void Add(List<object> coordinates, object value)
        {
            RecursiveAdd(coordinates.Count, coordinates, this, value);
        }

        private void RecursiveAdd(int depth, List<object> coordinates, FractalDictionary instance, object value)
        {
            if (depth == 0)
            {
                instance.content = value;
                /*
                if (instance.innerDictionary != null)
                {
                    instance.innerDictionary = null;
                    totalInnerDictionaries--;
                }
                */
            }
            else
            {
                FractalDictionary iteration;
                instance.innerDictionary.TryGetValue(coordinates[coordinates.Count - depth], out iteration);

                if (iteration == null)
                {
                    iteration = new FractalDictionary();
                    instance.innerDictionary[coordinates[coordinates.Count - depth]] = iteration;
                }

                RecursiveAdd(depth - 1, coordinates, iteration, value);
            }
        }

        public object TryGet(List<object> coordinates)
        {
            return RecursiveTryGet(coordinates.Count, coordinates, this);
        }

        private object RecursiveTryGet(int depth, List<object> coordinates, FractalDictionary instance)
        {
            if (depth == 0)
            {
                return instance.content;
            }
            else
            {
                FractalDictionary iteration;
                instance.innerDictionary.TryGetValue(coordinates[coordinates.Count - depth], out iteration);

                if (iteration != null)
                {
                    return RecursiveTryGet(depth - 1, coordinates, iteration);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
