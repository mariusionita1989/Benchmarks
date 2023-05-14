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
            int length = 500000000;
            byte[] input = RandomByteArrayGenerator.GenerateRandomByteArray(length);
            Span<byte> span = input.AsSpan();
            Stopwatch sw = Stopwatch.StartNew();
            FNVHash64.Hash(span);
            sw.Stop();
            Console.WriteLine("Operation took " + sw.ElapsedTicks + " ticks");
        }
    }
}