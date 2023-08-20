using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.Reflection;
using System.Reflection;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ReflectionOperationsDemo
    {
        private string AssemblyPath = @"C:\Users\Marius\source\repos\Test\Test\bin\Release\net7.0\Test.dll";
        private Assembly? MyAssembly = null;
        private string ClassType = "Test.MyDummyClass";
        private string CacheKey = string.Empty;
        private Type? SearchedType = null;

        [GlobalSetup]
        public void GlobalSetup()
        {
            MyAssembly = Assembly.LoadFile(AssemblyPath);
            SearchedType = MyAssembly.GetType(ClassType);
            CacheKey = AssemblyPath + ClassType;
        }

        [Benchmark(Baseline = true)]
        public void DisplayClassMetadata()
        {
            GetObjectInformation.GetMetadata(ref SearchedType, CacheKey);
        }

        [Benchmark]
        public void DisplayClassMetadataUsingParallelProcessing()
        {
            GetObjectInformation.GetMetadataParallelProcessing(ref SearchedType, CacheKey);
        }
    }
}
