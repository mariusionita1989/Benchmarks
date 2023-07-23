using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.RandomGenerator;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class RandomGeneratorDemo
    {
        private const int length = 1024 * 1024;

        [Benchmark(Baseline=true)]
        public void GetRandomString()
        {
            RandomStringGenerator.GenerateRandomString(length);
        }

        [Benchmark]
        public void GetHexadecimalRandomString()
        {
            RandomStringGenerator.GenerateHexadecimalRandomString(length);
        }

        [Benchmark]
        public void GetRandomByteArray()
        {
            RandomByteArrayGenerator.GenerateRandomByteArray(length);
        }

        [Benchmark]
        public void GetRandomIntegerArray()
        {
            RandomByteArrayGenerator.GenerateRandomIntArray(length);
        }
    }
}
