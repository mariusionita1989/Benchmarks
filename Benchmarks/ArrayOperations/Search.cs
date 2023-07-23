namespace Benchmarks.ArrayOperations
{
    public static class Search
    {
        public static int BinarySearch(ref int[] array, ref int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + ((right - left) & 1);
                if (array[mid] == target)
                    return mid;
                else if (array[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }
    }
}
