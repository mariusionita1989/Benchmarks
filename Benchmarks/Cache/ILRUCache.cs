namespace Benchmarks.Cache
{
    public interface ILRUCache
    {
        uint Get(string key);
        void Add(string key, uint value);
    }
}
