using System.Runtime.CompilerServices;

namespace Benchmarks.Hash
{
    public static  class FNVHash32
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe uint Hash(Span<byte> data)
        {
            const uint fnvPrime = 16777619;
            const uint fnvOffsetBasis = 2166136261;

            uint hash = fnvOffsetBasis;
            fixed (byte* pData = data)
            {
                ulong* pCurrent = (ulong*)pData;
                ulong* pEnd = (ulong*)(pData + data.Length);

                // Process 8 octo-words at a time
                for (; pCurrent + 7 < pEnd; pCurrent += 8)
                {
                    ulong value1 = pCurrent[0];
                    ulong value2 = pCurrent[1];
                    ulong value3 = pCurrent[2];
                    ulong value4 = pCurrent[3];
                    ulong value5 = pCurrent[4];
                    ulong value6 = pCurrent[5];
                    ulong value7 = pCurrent[6];
                    ulong value8 = pCurrent[7];

                    hash ^= (uint)value1;
                    hash *= fnvPrime;
                    hash ^= (uint)(value1 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value2;
                    hash *= fnvPrime;
                    hash ^= (uint)(value2 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value3;
                    hash *= fnvPrime;
                    hash ^= (uint)(value3 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value4;
                    hash *= fnvPrime;
                    hash ^= (uint)(value4 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value5;
                    hash *= fnvPrime;
                    hash ^= (uint)(value5 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value6;
                    hash *= fnvPrime;
                    hash ^= (uint)(value6 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value7;
                    hash *= fnvPrime;
                    hash ^= (uint)(value7 >> 32);
                    hash *= fnvPrime;

                    hash ^= (uint)value8;
                    hash *= fnvPrime;
                    hash ^= (uint)(value8 >> 32);
                    hash *= fnvPrime;
                }

                // Process any remaining quad-words and/or bytes
                for (; pCurrent < pEnd; pCurrent++)
                {
                    ulong value = *pCurrent;
                    hash ^= (uint)value;
                    hash *= fnvPrime;
                    hash ^= (uint)(value >> 32);
                    hash *= fnvPrime;
                }

                byte* pByteCurrent = (byte*)pCurrent;
                byte* pByteEnd = pData + data.Length;

                for (; pByteCurrent < pByteEnd; pByteCurrent++)
                {
                    hash ^= *pByteCurrent;
                    hash *= fnvPrime;
                }
            }

            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static uint SpanHash(ReadOnlySpan<byte> data)
        {
            const uint fnvOffsetBasis = 0x811C9DC5;
            const uint fnvPrime = 0x01000193;
            const uint firstPrimeNumber = 0x1a99f8;
            const uint secondPrimeNumber = 0x165669;
            const uint thirdPrimeNumber = 0x173f6b;
            const uint fourthPrimeNumber = 0x1aec27;
            const uint fifthPrimeNumber = 0x15f90f3d;
            const uint sixthPrimeNumber = 0x16f8b79d;
            const uint seventhPrimeNumber = 0x13e6746b;
            const uint eighthPrimeNumber = 0x10f26c49;

            int inputLength = data.Length;
            int FIRST_EIGHTH = inputLength >> 3;
            int SECOND_EIGHTH = inputLength >> 2;
            int THIRD_EIGHTH = SECOND_EIGHTH + FIRST_EIGHTH;
            int FOURTH_EIGHTH = inputLength >> 1;
            int FIFTH_EIGHTH = FOURTH_EIGHTH + FIRST_EIGHTH;
            int SIXTH_EIGHTH = FOURTH_EIGHTH + SECOND_EIGHTH;
            int SEVENTH_EIGHTH = FOURTH_EIGHTH + THIRD_EIGHTH;

            uint hash = fnvOffsetBasis;

            unsafe
            {
                fixed (byte* pData = data)
                {
                    byte* pFirstEighth = pData;
                    byte* pSecondEighth = pData + FIRST_EIGHTH;
                    byte* pThirdEighth = pData + SECOND_EIGHTH;
                    byte* pFourthEighth = pData + THIRD_EIGHTH;
                    byte* pFifthEighth = pData + FOURTH_EIGHTH;
                    byte* pSixthEighth = pData + FIFTH_EIGHTH;
                    byte* pSeventhEighth = pData + SIXTH_EIGHTH;
                    byte* pLastEighth = pData + SEVENTH_EIGHTH;

                    for (int i = 0; i < FIRST_EIGHTH; i+=14)
                    {
                        hash *= fnvPrime;
                        hash = pFirstEighth[i] & firstPrimeNumber;
                        hash = pSecondEighth[i] & secondPrimeNumber;
                        hash = pThirdEighth[i] & thirdPrimeNumber;
                        hash = pFourthEighth[i] & fourthPrimeNumber;
                        hash = pFifthEighth[i] & fifthPrimeNumber;
                        hash = pSixthEighth[i] & sixthPrimeNumber;
                        hash = pSeventhEighth[i] & seventhPrimeNumber;
                        hash = pLastEighth[i] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 1] & firstPrimeNumber;
                        hash = pSecondEighth[i + 1] & secondPrimeNumber;
                        hash = pThirdEighth[i + 1] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 1] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 1] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 1] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 1] & seventhPrimeNumber;
                        hash = pLastEighth[i + 1] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 2] & firstPrimeNumber;
                        hash = pSecondEighth[i + 2] & secondPrimeNumber;
                        hash = pThirdEighth[i + 2] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 2] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 2] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 2] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 2] & seventhPrimeNumber;
                        hash = pLastEighth[i + 2] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 3] & firstPrimeNumber;
                        hash = pSecondEighth[i + 3] & secondPrimeNumber;
                        hash = pThirdEighth[i + 3] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 3] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 3] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 3] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 3] & seventhPrimeNumber;
                        hash = pLastEighth[i + 3] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 4] & firstPrimeNumber;
                        hash = pSecondEighth[i + 4] & secondPrimeNumber;
                        hash = pThirdEighth[i + 4] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 4] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 4] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 4] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 4] & seventhPrimeNumber;
                        hash = pLastEighth[i + 4] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 5] & firstPrimeNumber;
                        hash = pSecondEighth[i + 5] & secondPrimeNumber;
                        hash = pThirdEighth[i + 5] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 5] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 5] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 5] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 5] & seventhPrimeNumber;
                        hash = pLastEighth[i + 5] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 6] & firstPrimeNumber;
                        hash = pSecondEighth[i + 6] & secondPrimeNumber;
                        hash = pThirdEighth[i + 6] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 6] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 6] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 6] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 6] & seventhPrimeNumber;
                        hash = pLastEighth[i + 6] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 7] & firstPrimeNumber;
                        hash = pSecondEighth[i + 7] & secondPrimeNumber;
                        hash = pThirdEighth[i + 7] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 7] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 7] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 7] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 7] & seventhPrimeNumber;
                        hash = pLastEighth[i + 7] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 8] & firstPrimeNumber;
                        hash = pSecondEighth[i + 8] & secondPrimeNumber;
                        hash = pThirdEighth[i + 8] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 8] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 8] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 8] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 8] & seventhPrimeNumber;
                        hash = pLastEighth[i + 8] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 9] & firstPrimeNumber;
                        hash = pSecondEighth[i + 9] & secondPrimeNumber;
                        hash = pThirdEighth[i + 9] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 9] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 9] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 9] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 9] & seventhPrimeNumber;
                        hash = pLastEighth[i + 9] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 10] & firstPrimeNumber;
                        hash = pSecondEighth[i + 10] & secondPrimeNumber;
                        hash = pThirdEighth[i + 10] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 10] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 10] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 10] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 10] & seventhPrimeNumber;
                        hash = pLastEighth[i + 10] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 11] & firstPrimeNumber;
                        hash = pSecondEighth[i + 11] & secondPrimeNumber;
                        hash = pThirdEighth[i + 11] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 11] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 11] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 11] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 11] & seventhPrimeNumber;
                        hash = pLastEighth[i + 11] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 12] & firstPrimeNumber;
                        hash = pSecondEighth[i + 12] & secondPrimeNumber;
                        hash = pThirdEighth[i + 12] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 12] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 12] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 12] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 12] & seventhPrimeNumber;
                        hash = pLastEighth[i + 12] & eighthPrimeNumber;

                        hash = pFirstEighth[i + 13] & firstPrimeNumber;
                        hash = pSecondEighth[i + 13] & secondPrimeNumber;
                        hash = pThirdEighth[i + 13] & thirdPrimeNumber;
                        hash = pFourthEighth[i + 13] & fourthPrimeNumber;
                        hash = pFifthEighth[i + 13] & fifthPrimeNumber;
                        hash = pSixthEighth[i + 13] & sixthPrimeNumber;
                        hash = pSeventhEighth[i + 13] & seventhPrimeNumber;
                        hash = pLastEighth[i + 13] & eighthPrimeNumber;
                    }
                }
            }

            return hash;
        }
    }
}
