using System.Runtime.CompilerServices;

namespace Benchmarks.Hash
{
    public static class FNVHash64
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ulong Hash(Span<byte> data)
        {
            const ulong fnvOffsetBasis = 0xcbf29ce484222325;
            const ulong fnvPrime = 0x100000001b3;
            int inputLength = data.Length;
            int SIXTEENTH = inputLength >> 4;
            ulong hash = fnvOffsetBasis;

            unsafe
            {
                fixed (byte* pData = data)
                {
                    byte* pCurrentData = pData;
                    byte* pEndData = pData + inputLength;
                    byte* pLastSixteenth = pData + (SIXTEENTH << 4);

                    for (; pCurrentData < pLastSixteenth; pCurrentData += 384)
                    {
                        hash ^= (*(ulong*)(pCurrentData + 0) * fnvPrime) ^ (*(ulong*)(pCurrentData + 8) * fnvPrime) ^ (*(ulong*)(pCurrentData + 16) * fnvPrime) ^ (*(ulong*)(pCurrentData + 24) * fnvPrime) ^ (*(ulong*)(pCurrentData + 32) * fnvPrime) ^ (*(ulong*)(pCurrentData + 40) * fnvPrime) ^ (*(ulong*)(pCurrentData + 48) * fnvPrime) ^ (*(ulong*)(pCurrentData + 56) * fnvPrime) ^ (*(ulong*)(pCurrentData + 64) * fnvPrime) ^ (*(ulong*)(pCurrentData + 72) * fnvPrime) ^ (*(ulong*)(pCurrentData + 80) * fnvPrime) ^ (*(ulong*)(pCurrentData + 88) * fnvPrime) ^ (*(ulong*)(pCurrentData + 96) * fnvPrime) ^ (*(ulong*)(pCurrentData + 104) * fnvPrime) ^ (*(ulong*)(pCurrentData + 112) * fnvPrime) ^ (*(ulong*)(pCurrentData + 120) * fnvPrime) ^ (*(ulong*)(pCurrentData + 128) * fnvPrime) ^ (*(ulong*)(pCurrentData + 136) * fnvPrime) ^ (*(ulong*)(pCurrentData + 144) * fnvPrime) ^ (*(ulong*)(pCurrentData + 152) * fnvPrime) ^ (*(ulong*)(pCurrentData + 160) * fnvPrime) ^ (*(ulong*)(pCurrentData + 168) * fnvPrime) ^ (*(ulong*)(pCurrentData + 176) * fnvPrime) ^ (*(ulong*)(pCurrentData + 184) * fnvPrime) ^ (*(ulong*)(pCurrentData + 192) * fnvPrime) ^ (*(ulong*)(pCurrentData + 200) * fnvPrime) ^ (*(ulong*)(pCurrentData + 208) * fnvPrime) ^ (*(ulong*)(pCurrentData + 216) * fnvPrime) ^ (*(ulong*)(pCurrentData + 224) * fnvPrime) ^ (*(ulong*)(pCurrentData + 232) * fnvPrime) ^ (*(ulong*)(pCurrentData + 240) * fnvPrime) ^ (*(ulong*)(pCurrentData + 248) * fnvPrime) ^ (*(ulong*)(pCurrentData + 256) * fnvPrime) ^ (*(ulong*)(pCurrentData + 264) * fnvPrime) ^ (*(ulong*)(pCurrentData + 272) * fnvPrime) ^ (*(ulong*)(pCurrentData + 280) * fnvPrime) ^ (*(ulong*)(pCurrentData + 288) * fnvPrime) ^ (*(ulong*)(pCurrentData + 296) * fnvPrime) ^ (*(ulong*)(pCurrentData + 304) * fnvPrime) ^ (*(ulong*)(pCurrentData + 312) * fnvPrime) ^ (*(ulong*)(pCurrentData + 320) * fnvPrime) ^ (*(ulong*)(pCurrentData + 328) * fnvPrime) ^ (*(ulong*)(pCurrentData + 336) * fnvPrime) ^ (*(ulong*)(pCurrentData + 344) * fnvPrime) ^ (*(ulong*)(pCurrentData + 352) * fnvPrime) ^ (*(ulong*)(pCurrentData + 360) * fnvPrime) ^ (*(ulong*)(pCurrentData + 368) * fnvPrime) ^ (*(ulong*)(pCurrentData + 376) * fnvPrime);
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
