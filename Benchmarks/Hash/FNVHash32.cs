using System.Runtime.CompilerServices;

namespace Benchmarks.Hash
{
    public static  class FNVHash32
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static uint Hash(Span<byte> data)
        {
            const uint fnvOffsetBasis = 0x811C9DC5u;
            const uint fnvPrime = 0x01000193u;
            int inputLength = data.Length;
            int SIXTEENTH = inputLength >> 4;
            uint hash = fnvOffsetBasis;

            unsafe
            {
                fixed (byte* pData = data)
                {
                    byte* pCurrentData = pData;
                    byte* pEndData = pData + inputLength;
                    byte* pLastSixtennth = pData + (SIXTEENTH << 4);

                    for (; pCurrentData < pLastSixtennth; pCurrentData += 128)
                    {
                        hash ^= (*(uint*)(pCurrentData + 0) * fnvPrime) ^ (*(uint*)(pCurrentData + 4) * fnvPrime) ^ (*(uint*)(pCurrentData + 8) * fnvPrime) ^ (*(uint*)(pCurrentData + 12) * fnvPrime) ^ (*(uint*)(pCurrentData + 16) * fnvPrime) ^ (*(uint*)(pCurrentData + 20) * fnvPrime) ^ (*(uint*)(pCurrentData + 24) * fnvPrime) ^ (*(uint*)(pCurrentData + 28) * fnvPrime) ^ (*(uint*)(pCurrentData + 32) * fnvPrime) ^ (*(uint*)(pCurrentData + 36) * fnvPrime) ^ (*(uint*)(pCurrentData + 40) * fnvPrime) ^ (*(uint*)(pCurrentData + 44) * fnvPrime) ^ (*(uint*)(pCurrentData + 48) * fnvPrime) ^ (*(uint*)(pCurrentData + 52) * fnvPrime) ^ (*(uint*)(pCurrentData + 56) * fnvPrime) ^ (*(uint*)(pCurrentData + 60) * fnvPrime) ^ (*(uint*)(pCurrentData + 64) * fnvPrime) ^ (*(uint*)(pCurrentData + 68) * fnvPrime) ^ (*(uint*)(pCurrentData + 72) * fnvPrime) ^ (*(uint*)(pCurrentData + 76) * fnvPrime) ^ (*(uint*)(pCurrentData + 80) * fnvPrime) ^ (*(uint*)(pCurrentData + 84) * fnvPrime) ^ (*(uint*)(pCurrentData + 88) * fnvPrime) ^ (*(uint*)(pCurrentData + 92) * fnvPrime) ^ (*(uint*)(pCurrentData + 96) * fnvPrime) ^ (*(uint*)(pCurrentData + 100) * fnvPrime) ^ (*(uint*)(pCurrentData + 104) * fnvPrime) ^ (*(uint*)(pCurrentData + 108) * fnvPrime) ^ (*(uint*)(pCurrentData + 112) * fnvPrime) ^ (*(uint*)(pCurrentData + 116) * fnvPrime) ^ (*(uint*)(pCurrentData + 120) * fnvPrime) ^ (*(uint*)(pCurrentData + 124) * fnvPrime);
                    }

                    for (; pCurrentData < pEndData; pCurrentData++)
                    {
                        hash *= fnvPrime;
                        hash ^= *pCurrentData;
                    }
                }
            }

            hash *= fnvPrime;
            hash ^= hash >> 16;
            hash *= fnvPrime;
            hash ^= hash >> 16;
            hash *= fnvPrime;
            return hash;
        }
    }
}
