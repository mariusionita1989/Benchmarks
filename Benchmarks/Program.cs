using Benchmarks.Compression;
using Benchmarks.Hash;
using Benchmarks.RandomGenerator;
using System.Diagnostics;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int length = 50000000;
            byte[] input = RandomByteArrayGenerator.GenerateRandomByteArray(length);
            Span<byte> span = input.AsSpan();
            Stopwatch sw = Stopwatch.StartNew();
            FNVHash32.SpanHash(span);
            sw.Stop();
            Console.WriteLine("Operation took " + sw.ElapsedTicks + " ticks");
        }
    }
}