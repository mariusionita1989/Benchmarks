using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.Cache;

namespace Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class CacheOperationsDemo
    {
        private const int length = 1024 * 1024;
        private LRUCache cache = null;
        private const int firstItem = 599;
        private const int secondItem = 2323232;

        [GlobalSetup]
        public void GlobalSetup()
        {
            cache = new LRUCache(length);
        }

        [Benchmark(Baseline = true)]
        public void AddItemsToCache()
        {
            cache.Add("1",firstItem);
            cache.Add("2", secondItem);
        }

        [Benchmark]
        public void GetItemsFromCache()
        {
            cache.Get("2");
            cache.Get("1");
        }
    }
}
