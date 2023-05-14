using System.Runtime.CompilerServices;
using System.Text;

namespace Benchmarks.Compression
{
    public static class LZWCompression
    {
        //[MethodImpl(MethodImplOptions.AggressiveOptimization)]
        //public static List<ushort> Compress(byte[] data)
        //{
        //    Dictionary<string, ushort> dictionary = new Dictionary<string, ushort>();
        //    for (int i = 0; i < 256; i++)
        //    {
        //        dictionary.Add(((char)i).ToString(), (ushort)i);
        //    }

        //    List<ushort> compressedData = new List<ushort>();
        //    string currentString = string.Empty;

        //    foreach (byte b in data)
        //    {
        //        string nextString = currentString + (char)b;
        //        if (dictionary.ContainsKey(nextString))
        //        {
        //            currentString = nextString;
        //        }
        //        else
        //        {
        //            compressedData.Add(dictionary[currentString]);
        //            dictionary.Add(nextString, (ushort)dictionary.Count);
        //            currentString = ((char)b).ToString();
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(currentString))
        //    {
        //        compressedData.Add(dictionary[currentString]);
        //    }

        //    //List<byte> compressedBytes = new List<byte>();
        //    //for (int i = 0; i < compressedData.Count; i++)
        //    //{
        //    //    if (i % 2 == 0)
        //    //    {
        //    //        compressedBytes.Add((byte)(compressedData[i] >> 8));
        //    //        compressedBytes.Add((byte)(compressedData[i] & 0xFF));
        //    //    }
        //    //    else
        //    //    {
        //    //        compressedBytes[compressedBytes.Count - 1] |= (byte)(compressedData[i] >> 8);
        //    //        compressedBytes.Add((byte)(compressedData[i] & 0xFF));
        //    //    }
        //    //}

        //    return compressedData;
        //}

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static List<ushort> Compress(byte[] data)
        {
            var dictionary = new Dictionary<string, ushort>(256, new FastStringComparer());
            for (ushort i = 0; i < 256; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            var compressedData = new List<ushort>(data.Length / 2);
            var sb = new StringBuilder();
            foreach (var b in data.AsSpan())
            {
                var nextString = sb.Append((char)b).ToString();
                if (dictionary.TryGetValue(nextString, out var code))
                {
                    sb.Clear();
                    sb.Append(nextString[0]);
                }
                else
                {
                    dictionary.Add(nextString, (ushort)dictionary.Count);
                    compressedData.Add(dictionary[sb.ToString()]);
                    sb.Clear();
                    sb.Append((char)b);
                }
            }

            if (sb.Length > 0)
            {
                compressedData.Add(dictionary[sb.ToString()]);
            }

            return compressedData;
        }

        private class FastStringComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x.AsSpan().SequenceEqual(y.AsSpan());
            }

            public int GetHashCode(string obj)
            {
                int hash = 5381;
                foreach (byte b in obj.AsSpan())
                {
                    hash = ((hash << 5) + hash) + b;
                }
                return hash;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static byte[] Decompress(byte[] data)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(i, ((char)i).ToString());
            }

            List<int> compressedData = new List<int>();
            for (int i = 0; i < data.Length; i += 2)
            {
                int value = (data[i] << 8) | data[i + 1];
                compressedData.Add(value);
            }

            List<byte> decompressedBytes = new List<byte>();
            string currentString = dictionary[compressedData[0]];
            decompressedBytes.AddRange(currentString.ToByteArray());

            for (int i = 1; i < compressedData.Count; i++)
            {
                string nextString = string.Empty;
                if (dictionary.ContainsKey(compressedData[i]))
                {
                    nextString = dictionary[compressedData[i]];
                }
                else if (compressedData[i] == dictionary.Count)
                {
                    nextString = currentString + currentString[0];
                }

                decompressedBytes.AddRange(nextString.ToByteArray());

                dictionary.Add(dictionary.Count, currentString + nextString[0]);
                currentString = nextString;
            }

            return decompressedBytes.ToArray();
        }

        private static byte[] ToByteArray(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }
    }
}
