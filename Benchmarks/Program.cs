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
            //BenchmarkRunner.Run<RandomGeneratorDemo>();
            //BenchmarkRunner.Run<MemoryCopyDemo>();
            //BenchmarkRunner.Run<HashFunctionsDemo>();
            //BenchmarkRunner.Run<ArrayOperationsDemo>();
            //BenchmarkRunner.Run<CacheOperationsDemo>();
            BenchmarkRunner.Run<FileOperationsDemo>();
        }
    }
}