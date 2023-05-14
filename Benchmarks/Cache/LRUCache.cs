using System.Runtime.CompilerServices;

namespace Benchmarks.Cache
{
    public class LRUCache : ILRUCache
    {
        private int capacity;
        private int count;
        private readonly LinkedList<string> linkedList;
        private readonly Dictionary<string,uint> map;

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public LRUCache(int _capacity)
        {
            capacity = _capacity;
            count = 0;
            linkedList = new LinkedList<string>();
            map = new Dictionary<string,uint>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public uint Get(string key)
        {
            if (!map.TryGetValue(key, out var node)) return uint.MaxValue;
            linkedList.Remove(key);
            linkedList.AddFirst(key);
            return node;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void Add(string key, uint value)
        {
            if (map.TryGetValue(key, out var node))
            {
                linkedList.Remove(key);
                map[key] = value;
                linkedList.AddFirst(key);
            }
            else
            {
                if (count == capacity)
                {
                    var lru = linkedList.Last;
                    if (lru != null)
                    {
                        map.Remove(lru.Value, out _);
                        linkedList.RemoveLast();
                        count--;
                    }
                }

                linkedList.AddFirst(key);
                map[key] = value;
                count++;
            }
        }
    }
}
