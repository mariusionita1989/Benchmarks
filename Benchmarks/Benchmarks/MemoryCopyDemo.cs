using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.MemoryCopy;
using Benchmarks.RandomGenerator;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class MemoryCopyDemo
    {
        private const int length = 1024 * 1024;
        private byte[] input = null;

        [GlobalSetup]
        public void GlobalSetup()
        {
            input = RandomByteArrayGenerator.GenerateRandomByteArray(length);
        }

        [Benchmark(Baseline = true)]
        public void GetSpanMemoryCopy()
        {
            ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(input);
            Span<byte> span = new Span<byte>(input);
            ArrayCopy.SpanMemoryCopy(readOnlySpan,span);
        }

        [Benchmark]
        public void GetSpanMemoryCopyWithLength()
        {
            ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(input);
            Span<byte> span = new Span<byte>(input);
            ArrayCopy.SpanMemoryCopy(readOnlySpan,span,length);
        }

        [Benchmark]
        public void GetUnsafeSpanMemoryCopy()
        {
            ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(input);
            Span<byte> span = new Span<byte>(input);
            ArrayCopy.UnsafeSpanMemoryCopy(readOnlySpan,span);
        }
    }
}
