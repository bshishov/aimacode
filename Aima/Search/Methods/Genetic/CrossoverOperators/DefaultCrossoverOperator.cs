using System;
using System.Linq;

namespace Aima.Search.Methods.Genetic.CrossoverOperators
{
    public class DefaultCrossoverOperator<TAlphabet> : ICrossoverOperator<TAlphabet>
    {
        private readonly Random _random = new Random();

        public TAlphabet[] Apply(TAlphabet[] x, TAlphabet[] y)
        {
            var c = _random.Next(x.Length - 1);
            var result = new TAlphabet[x.Length];
            for (var i = 0; i < c; i++)
                result[i] = x[i];
            for (var i = c; i < x.Length; i++)
                result[i] = y[i];
            return result;
        }
    }

    public class DistinctCrossoverOperator<TAlphabet> : ICrossoverOperator<TAlphabet>
    {
        private readonly Random _random = new Random();

        public TAlphabet[] Apply(TAlphabet[] x, TAlphabet[] y)
        {
            var c = _random.Next(x.Length - 1);
            var result = new TAlphabet[x.Length];
            for (var i = 0; i < c; i++)
                result[i] = x[i];

            for (var i = c; i < x.Length; i++)
            {
                if (result.Contains(y[i]))
                {
                    const int MaxAttempts = 20;
                    var attempts = 0;
                    while (attempts < MaxAttempts)
                    {
                        var candidate = y[_random.Next(y.Length)];
                        if (!result.Contains(candidate))
                        {
                            result[i] = candidate;
                            break;
                        }
                        attempts++;
                    }

                    if (attempts > MaxAttempts)
                        return Apply(x, y);
                }
                else
                {
                    result[i] = y[i];
                }
            }
            return result;
        }
    }
}