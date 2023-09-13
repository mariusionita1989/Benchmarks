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
        private string a = string.Empty;
        private string b = string.Empty;

        [GlobalSetup]
        public void GlobalSetup()
        {
            input = RandomStringGenerator.GenerateRandomString(length).ToCharArray();
            a = RandomStringGenerator.GenerateRandomString(length);
            b = RandomStringGenerator.GenerateRandomString(length);
        }

        [Benchmark]
        public void GetCharsCount()
        {
            Counter.CharsCount(input);
        }

        [Benchmark]
        public void EqualOperatorStringCompare()
        {
            Comparison.EqualOperatorComparison(a,b);
        }

        [Benchmark]
        public void EqualsStringCompare()
        {
            Comparison.EqualsComparison(a,b);
        }

        [Benchmark]
        public void StringCompare()
        {
            Comparison.StringCompare(a,b);
        }
    }
}
