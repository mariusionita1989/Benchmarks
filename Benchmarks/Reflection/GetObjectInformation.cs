using System.Runtime.CompilerServices;
using System.Text;

namespace Benchmarks.Reflection
{
    public static class GetObjectInformation
    {
        private static readonly Dictionary<string, List<string>> Cache = new Dictionary<string, List<string>>();
        private static readonly object CacheLock = new object();

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static List<string> GetMetadata(ref Type? SearchedType, string cacheKey)
        {
            if (Cache.TryGetValue(cacheKey, out var cachedMetadata))
                return cachedMetadata;

            var metadataList = new List<string>();
            if (SearchedType != null)
            {
                var members = SearchedType.GetMembers();
                int length = members.Length;
                var sb = new StringBuilder(length * 50);

                foreach (var memberInfo in members)
                {
                    sb.Append(memberInfo.Name+Environment.NewLine);
                    metadataList.Add(memberInfo.Name); 
                }
            }

            Cache.TryAdd(cacheKey, metadataList);
            return metadataList;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static List<string> GetMetadataParallelProcessing(ref Type? SearchedType, string cacheKey)
        {
            if (Cache.TryGetValue(cacheKey, out var cachedMetadata))
                return cachedMetadata;

            var metadataList = new List<string>();
            if (SearchedType != null)
            {
                var members = SearchedType.GetMembers();
                int length = members.Length;
                var sb = new StringBuilder(length * 50);
                Parallel.ForEach(members, memberInfo =>
                {
                    string memberName = memberInfo.Name;
                    sb.Append(memberName + Environment.NewLine);
                    metadataList.Add(memberName);
                });
            }

            lock (CacheLock)
            {
                if (!Cache.ContainsKey(cacheKey))
                {
                    Cache.Add(cacheKey, metadataList);
                }
            }

            return metadataList;
        }
    }
}
