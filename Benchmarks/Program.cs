using BenchmarkDotNet.Running;
using Benchmarks.Benchmarks;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<PrimeNumberDemo>();
            //BenchmarkRunner.Run<StringOperationsDemo>();
            BenchmarkRunner.Run<RandomGeneratorDemo>();
        }
    }
}