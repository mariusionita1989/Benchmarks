using System.Runtime.CompilerServices;

namespace Benchmarks.MemoryCopy
{
    public static class ArrayCopy
    {
        public static void SpanMemoryCopy(ReadOnlySpan<byte> source, Span<byte> destination) 
        {
            source.CopyTo(destination);
        }

        public static void SpanMemoryCopy(ReadOnlySpan<byte> source, Span<byte> destination, int length)
        {
            source.CopyTo(destination.Slice(0,length));
        }

        public static unsafe void UnsafeSpanMemoryCopy<T>(ReadOnlySpan<T> source, Span<T> destination) where T : struct
        {
            if (destination.Length != source.Length)
                throw new ArgumentException("Source and destination spans must have the same length.");
            
            var sizeOfT = Unsafe.SizeOf<T>();
            var sourcePtr = Unsafe.AsPointer(ref Unsafe.AsRef(source[0]));
            var destinationPtr = Unsafe.AsPointer(ref destination[0]);

            Unsafe.CopyBlock(destinationPtr, sourcePtr, (uint)(sizeOfT * source.Length));
        }
    }
}
