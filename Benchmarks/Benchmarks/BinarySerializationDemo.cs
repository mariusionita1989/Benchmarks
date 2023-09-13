using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.Serialization;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BinarySerializationDemo
    {
        private NugetPerson? nugetPackPerson;
        private byte[]? serializedOutput;

        [GlobalSetup]
        public void GlobalSetup()
        {
            nugetPackPerson = new NugetPerson { Name = "Alice", Age = 30 };
        }

        [Benchmark(Baseline = true)]
        public void MessagePackBinarySerialization()
        {
            if (nugetPackPerson != null)
                serializedOutput = BinarySerializationUsingNuget.SerializeObject(ref nugetPackPerson);
        }

        [Benchmark]
        public void MessagePackBinaryDeserialization()
        {
            if (serializedOutput != null)
                nugetPackPerson = BinarySerializationUsingNuget.DeserializeObject<NugetPerson>(ref serializedOutput);
        }
    }
}
