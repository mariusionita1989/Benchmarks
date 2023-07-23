using System.Runtime.CompilerServices;

namespace Benchmarks.Hash
{
    public static class ByteArrayHash
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe ulong ULongHash(Span<byte> data) 
        {
            const ulong hashOffset = 0xcbf29ce484222325;
            ulong hash = hashOffset;
            int inputLength = data.Length>>3;

            fixed (byte* bytePtr = data)
            {
                ulong* ulongPtr = (ulong*)bytePtr;
                for(int i = 0; i < inputLength; i++) 
                    hash ^= ulongPtr[i];
            }

            hash *= hashOffset;
            hash ^= hash >> 16;
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe uint UIntHash(Span<byte> data)
        {
            const uint hashOffset = 0x811c9dc5u;
            uint hash = hashOffset;
            int inputLength = data.Length >> 2;

            fixed (byte* bytePtr = data)
            {
                uint* uintPtr = (uint*)bytePtr;
                for (int i = 0; i < inputLength; i++)
                    hash ^= uintPtr[i];
            }

            hash *= hashOffset;
            hash ^= hash >> 16;
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe ushort UShortHash(Span<byte> data)
        {
            const ushort hashOffset = 0xfffb;
            ushort hash = hashOffset;
            int inputLength = data.Length >> 1;

            fixed (byte* bytePtr = data)
            {
                ushort* ushortPtr = (ushort*)bytePtr;
                for (int i = 0; i < inputLength; i++)
                    hash ^= ushortPtr[i];
            }

            hash *= hashOffset;
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe ushort FastUShortHash(Span<byte> data)
        {
            ushort hash = 0;
            const ushort modulo = 0xffff;
            int inputLength = data.Length >> 1;

            fixed (byte* bytePtr = data)
            {
                ushort* ushortPtr = (ushort*)bytePtr;
                for (int i = 0; i < inputLength; i++)
                    hash ^= ushortPtr[i];
            }

            hash &= modulo;
            return hash;
        }
    }
}
