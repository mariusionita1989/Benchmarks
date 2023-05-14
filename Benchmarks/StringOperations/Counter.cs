using System.Runtime.CompilerServices;

namespace Benchmarks.StringOperations
{
    public static class Counter
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static int[] CharsCount(char[] input)
        {
            int length = 256;
            int[] charCounts = new int[length];
            int inputLength = input.Length;
            for (int i = 0; i < inputLength; i++)
            {
                char c = input[i];
                charCounts[c]++;
            }

            return charCounts;
        }
    }
}
