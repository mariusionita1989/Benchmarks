using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.Hash;
using Benchmarks.MemoryCopy;
using Benchmarks.RandomGenerator;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class HashFunctionsDemo
    {
        private const int length = 1024 * 1024;
        private byte[] input = null;

        [GlobalSetup]
        public void GlobalSetup()
        {
            input = RandomByteArrayGenerator.GenerateRandomByteArray(length);
        }

        [Benchmark(Baseline = true)]
        public void GetULongHash()
        {
            Span<byte> span = new Span<byte>(input);
            ByteArrayHash.ULongHash(span);
        }

        [Benchmark]
        public void GetUIntHash()
        {
            Span<byte> span = new Span<byte>(input);
            ByteArrayHash.UIntHash(span);
        }

        [Benchmark]
        public void GetUShortHash()
        {
            Span<byte> span = new Span<byte>(input);
            ByteArrayHash.UShortHash(span);
        }

        [Benchmark]
        public void GetFastUShortHash()
        {
            Span<byte> span = new Span<byte>(input);
            ByteArrayHash.FastUShortHash(span);
        }
    }
}
