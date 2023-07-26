using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.ArrayOperations;
using Benchmarks.RandomGenerator;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ArrayOperationsDemo
    {
        private const int length = 1024 * 1024;
        private int searchedItem = 1002334;
        private byte[] byteInput = null;
        private int[] intInput = null;
        
        [GlobalSetup]
        public void GlobalSetup()
        {
            byteInput = RandomByteArrayGenerator.GenerateRandomByteArray(length);
            intInput = RandomByteArrayGenerator.GenerateRandomIntArray(length);
        }

        [Benchmark(Baseline = true)]
        public void GetBytesCount()
        {
            Span<byte> span = new Span<byte>(byteInput);
            Count.BytesCount(span);
        }

        [Benchmark]
        public void GetBinarySearch()
        {
            Search.BinarySearch(ref intInput, ref searchedItem);
        }
    }
}
