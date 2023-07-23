using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.RandomGenerator;
using Benchmarks.StringOperations;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class StringOperationsDemo
    {
        private const int length = 1024 * 1024;
        private char[] input = null;

        [GlobalSetup]
        public void GlobalSetup()
        {
            input = RandomStringGenerator.GenerateRandomString(length).ToCharArray();
        }

        [Benchmark]
        public void GetCharsCount()
        {
            Counter.CharsCount(input);
        }
    }
}
