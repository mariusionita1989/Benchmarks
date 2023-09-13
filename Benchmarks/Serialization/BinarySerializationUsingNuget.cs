using MessagePack;
using System.Runtime.CompilerServices;

namespace Benchmarks.Serialization
{
    public static class BinarySerializationUsingNuget
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static byte[] SerializeObject<T>(ref T obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T DeserializeObject<T>(ref byte[] data)
        {
            return MessagePackSerializer.Deserialize<T>(data);
        }
    }
}
