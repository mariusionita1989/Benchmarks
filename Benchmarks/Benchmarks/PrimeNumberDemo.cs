using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.PrimeNumbersGenerator;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class PrimeNumberDemo
    {
        private const long length = 500 * 1024;

        [Benchmark(Baseline=true)]
        public void GetPrimesSieveOfEratosthenes() 
        {
            Prime.CalculatePrimesUsingSieveOfEratosthenes(length);
        }

        [Benchmark]
        public void GetPrimesNaiveVersion()
        {
            Prime.CalculatePrimes(length);
        }
    }
}
