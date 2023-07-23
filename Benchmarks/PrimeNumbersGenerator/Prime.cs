using System.Collections;
using System.Runtime.CompilerServices;

namespace Benchmarks.PrimeNumbersGenerator
{
    public static class Prime
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static bool IsPrime(long number)
        {
            if (number < 2)
                return false;

            if (number < 4)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            long upper = number >> 1;
            for (long factor = 5; factor <= upper; factor += 6)
            {
                if (number % factor == 0)
                    return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static List<long> CalculatePrimes(long maxValue)
        {
            List<long> primes = new List<long>();
            if (maxValue < 2)
                return primes;

            if (maxValue == 2)
                primes.Add(2);

            for (long i = 3; i <= maxValue; i += 2)
            {
                if (IsPrime(i))
                    primes.Add(i);
            }

            return primes;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static List<long> CalculatePrimesUsingSieveOfEratosthenes(long maxValue)
        {
            BitArray isPrime = new BitArray((int)maxValue / 2 + 1, true);
            List<long> primes = new List<long>();

            long sqrt = (long)Math.Sqrt(maxValue);
            for (long p = 3; p <= sqrt; p += 2)
            {
                if (isPrime[(int)(p / 2)])
                {
                    for (long i = p * p; i <= maxValue; i += 2 * p)
                        isPrime[(int)(i / 2)] = false;
                }
            }

            primes.Add(2);

            for (long i = 3; i <= maxValue; i += 2)
            {
                if (isPrime[(int)(i / 2)])
                    primes.Add(i);
            }

            return primes;
        }
    }
}
