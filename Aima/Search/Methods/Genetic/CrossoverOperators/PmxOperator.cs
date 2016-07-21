using System;
using System.Collections.Generic;

namespace Aima.Search.Methods.Genetic.CrossoverOperators
{
    /// <summary>
    /// Partially-mapped crossover operator
    /// </summary>
    public class PmxOperator<T> : ICrossoverOperator<T>
    {
        private readonly Random _random = new Random();

        public T[] Apply(T[] x, T[] y)
        {
            var p1 = _random.Next(1, x.Length - 1);
            var p2 = _random.Next(p1, x.Length);

            // assume that result is the first offspring
            var result = new T[x.Length];
            x.CopyTo(result, 0);

            var mapping = new Dictionary<T, T>();

            // copy y mapping to result
            for (var i = p1; i < p2; i++)
            {
                mapping.Add(y[i], x[i]);
                result[i] = y[i];
            }

            var mappingOccured = true;
            while (mappingOccured)
            {
                mappingOccured = false;
                for (var i = 0; i < p1; i++)
                {
                    if (mapping.ContainsKey(result[i]))
                    {
                        result[i] = mapping[result[i]];
                        mappingOccured = true;
                    }
                }

                for (var i = p2; i < x.Length; i++)
                {
                    if (mapping.ContainsKey(result[i]))
                    {
                        result[i] = mapping[result[i]];
                        mappingOccured = true;
                    }
                }
            }

            return result;
        }
    }
}