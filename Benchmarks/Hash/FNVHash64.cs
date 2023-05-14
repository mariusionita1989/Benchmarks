using System.Runtime.CompilerServices;

namespace Benchmarks.Hash
{
    public static class FNVHash64
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe ulong Hash(Span<byte> data)
        {
            const ulong FNV_offset_basis = 14695981039346656037;
            const ulong FNV_prime = 1099511628211;
            const int bytesPerUInt = sizeof(uint);

            ulong hash = FNV_offset_basis;
            fixed (byte* pData = &data.GetPinnableReference())
            {
                byte* pByteCurrent = pData;
                byte* pByteEnd = pData + data.Length;
                uint* pCurrent = (uint*)pData;
                uint* pEnd = (uint*)(pData + (data.Length & ~(bytesPerUInt - 1)));

                while (pCurrent < pEnd)
                {
                    hash = unchecked(hash ^ *pCurrent);
                    hash = unchecked(hash * FNV_prime);
                    pCurrent++;
                    pByteCurrent += bytesPerUInt;
                }

                if (pByteCurrent < pByteEnd)
                {
                    do
                    {
                        hash = unchecked(hash ^ *pByteCurrent);
                        hash = unchecked(hash * FNV_prime);
                        pByteCurrent++;
                    } while (pByteCurrent < pByteEnd);
                }
            }

            return hash;
        }
    }
}
