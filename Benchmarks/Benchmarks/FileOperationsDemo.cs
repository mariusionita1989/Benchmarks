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
        private string filePath = @"C:\Temp\nugets.zip";
        private string fileStringPath = @"C:\Temp\cars.txt";

        [Benchmark]
        public void ReadBinaryFileWithBuffer()
        {
            ReadFile.ReadBinaryFile(filePath, length);
        }

        [Benchmark]
        public void WriteBinaryFileWithBuffer()
        {
            WriteFile.WriteBinaryFile(filePath, length);
        }

        [Benchmark]
        public void ReadStringFile()
        {
            ReadFile.ReadStringFile(fileStringPath);
        }

        [Benchmark]
        public void ReadMemoryMappedFile()
        {
            ReadFile.ReadMemoryMappedFile(fileStringPath,length);
        }

        [Benchmark]
        public void ParallelReadMemoryMappedFile()
        {
            ReadFile.ParallelReadMemoryMappedFile(fileStringPath,length);
        }

        [Benchmark]
        public async Task AsyncMemoryMappedFile()
        {
            await ReadFile.AsyncReadMemoryMappedFile(fileStringPath,length);
        }
    }
}
