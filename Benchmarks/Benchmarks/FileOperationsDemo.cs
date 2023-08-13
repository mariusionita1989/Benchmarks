using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.FileOperations;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class FileOperationsDemo
    {
        private const int length = 1024*64;
        private string filePath = @"D:\nugets.zip";

        [Benchmark]
        public void ReadBinaryFileWithBuffer()
        {
            ReadFile.ReadBinaryFile(filePath, length);
        }

        [Benchmark]
        public void WriteBinaryFileWithBuffer()
        {
            WriteFile.WriteBinaryFile(filePath,length);
        }
    }
}
