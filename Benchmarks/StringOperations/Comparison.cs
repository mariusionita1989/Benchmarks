namespace Benchmarks.StringOperations
{
    public static class Comparison
    {
        public static void EqualOperatorComparison(string a, string b) 
        {
            bool status = a == b;
        }

        public static void EqualsComparison(string a, string b)
        {
            bool status = a.Equals(b);
        }

        public static void StringCompare(string a, string b)
        {
            bool status = string.Compare(a,b) == 0;
        }
    }
}
