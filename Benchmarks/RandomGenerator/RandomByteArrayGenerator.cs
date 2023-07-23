using System.Runtime.CompilerServices;

namespace Benchmarks.RandomGenerator
{
    public static class RandomByteArrayGenerator
    {
        private static readonly Random random = new Random((int)DateTime.UtcNow.Ticks);

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static byte[] GenerateRandomByteArray(int length)
        {
            byte[] byteArray = new byte[length];
            random.NextBytes(byteArray);
            return byteArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static int[] GenerateRandomIntArray(int length)
        {
            int[] intArray = new int[length];
            for (int i = 0; i < length; i++)
                intArray[i] = random.Next();
            
            return intArray;
        }
    }
}
