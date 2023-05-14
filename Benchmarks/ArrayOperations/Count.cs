using System.Runtime.CompilerServices;

namespace Benchmarks.ArrayOperations
{
    public static class Count
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static int[] BytesCount(Span<byte> input)
        {
            int length = 256;
            int[] byteCounts = new int[length];
            int inputLength = input.Length;
            int i = 0;
            int HALF = inputLength >> 1;
            
            Span<byte> firstHalf = input.Slice(i,HALF);
            Span<byte> secondHalf = input.Slice(HALF);

            for (i = 0; i < HALF; i++) 
            {
                byteCounts[firstHalf[i]]++;
                byteCounts[secondHalf[i]]++;
            }

            if ((inputLength & 1) == 1)
                byteCounts[secondHalf[HALF]]++;

            return byteCounts;
        }
    }
}
