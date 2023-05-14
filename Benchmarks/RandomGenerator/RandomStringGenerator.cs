using System.Runtime.CompilerServices;

namespace Benchmarks.RandomGenerator
{
    public static class RandomStringGenerator
    {
        private static readonly Random random = new Random((int)DateTime.UtcNow.Ticks);

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static string GenerateRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int charactersLength = characters.Length;
            Span<char> randomChars = new char[length];
            Span<byte> randomBytes = new byte[length];
            random.NextBytes(randomBytes);
            for (int i = 0; i < length; i++)
                randomChars[i] = characters[randomBytes[i] % charactersLength];

            return new string(randomChars);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static string GenerateHexadecimalRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOP";
            Span<char> randomChars = new char[length];
            Span<byte> randomBytes = new byte[length];
            random.NextBytes(randomBytes);
            for (int i = 0; i < length; i++)
                randomChars[i] = characters[randomBytes[i] & 0x0f];

            return new string(randomChars);
        }
    }
}
