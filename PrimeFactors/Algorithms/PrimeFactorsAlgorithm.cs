using PrimeFactors.Interfaces;
using PrimeFactors.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PrimeFactors.Algorithms
{
    public class PrimeFactorsAlgorithm : ICalculation
    {
        Eratosthenes _Eratosthenes = new Eratosthenes();

        public Utils.CalculationResult Calculate(string input)
        {
            long toFactorise = 0;
            if (long.TryParse(input, out toFactorise))
            {
                if (toFactorise > 1)
                {
                    StringBuilder factorList = new StringBuilder();

                    foreach (long factor in GetPrimeFactors(toFactorise, _Eratosthenes))
                    {
                        if (factorList.Length == 0)
                        {
                            factorList.Append("Prime factor(s): ");
                        }
                        else
                        {
                            factorList.Append(", ");
                        }
                        factorList.Append(factor);
                    }

                    return new Utils.CalculationResult(true, factorList.ToString());
                }
                else
                {
                    return new Utils.CalculationResult(false, Utils.Message_GreaterThanOne);
                }
            }
            else
            {
                return new Utils.CalculationResult(false, Utils.Message_NonNegativeInteger);
            }
        }

        private static IEnumerable<long> GetPrimeFactors(long value, Eratosthenes eratosthenes)
        {
            List<long> factors = new List<long>();

            foreach (int prime in eratosthenes)
            {
                while (value % prime == 0)
                {
                    value /= prime;
                    factors.Add(prime);
                }

                if (value == 1)
                    break;
            }

            return factors;
        }
    }

    public class Eratosthenes : IEnumerable<int>
    {
        private static List<int> _primes = new List<int>();
        private int _lastChecked;

        public Eratosthenes()
        {
            _primes.Add(2);
            _lastChecked = 2;
        }

        private bool IsPrime(int checkValue)
        {
            bool isPrime = true;

            foreach (int prime in _primes)
            {
                if ((checkValue % prime) == 0 && prime <= Math.Sqrt(checkValue))
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime;
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (int prime in _primes)
                yield return prime;

            while (_lastChecked < int.MaxValue)
            {
                _lastChecked++;

                if (IsPrime(_lastChecked))
                {
                    _primes.Add(_lastChecked);
                    yield return _lastChecked;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
