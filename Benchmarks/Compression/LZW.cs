//using Benchmarks.Hash;
//using System.Runtime.CompilerServices;
//using System.Text;

//namespace Benchmarks.Compression
//{
//    public static class LZWCompression
//    {
//        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
//        public static unsafe List<ushort> Compress(Span<byte> data)
//        {
//            const ushort dictionarySize = 256;
//            const int dictionaryMaxSize = 65536;
//            Span<byte> initialDictionary = new byte[] { 
//                                                          0,   1,   2,   3,   4,   5,   6,   7,   8,   9,  10,  11,  12,  13,  14,  15,
//                                                         16,  17,  18,  19,  20,  21,  22,  23,  24,  25,  26,  27,  28,  29,  30,  31,
//                                                         32,  33,  34,  35,  36,  37,  38,  39,  40,  41,  42,  43,  44,  45,  46,  47,
//                                                         48,  48,  50,  51,  52,  53,  54,  55,  56,  57,  58,  59,  60,  61,  62,  63,
//                                                         64,  65,  66,  67,  68,  69,  70,  71,  72,  73,  74,  75,  76,  77,  78,  79,
//                                                         80,  81,  82,  83,  84,  85,  86,  87,  88,  89,  90,  91,  92,  93,  94,  95,
//                                                         96,  97,  98,  99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111,
//                                                        112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127,
//                                                        128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143,
//                                                        144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
//                                                        160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175,
//                                                        176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191,
//                                                        192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207,
//                                                        208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223,
//                                                        224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
//                                                        240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255
//                                                      }.AsSpan();

//            ushort[] dictionary = new ushort[dictionaryMaxSize];
//            int length = data.Length;
//            int compressionListlength = length >> 1;
//            int dictionaryCount = dictionary.Length;

//            for (ushort i = 0; i < dictionarySize; i += 4)
//            {
//                dictionary[i  ] = ByteArrayHash.FastUShortHash(initialDictionary.Slice(i  ,1));
//                dictionary[i+1] = ByteArrayHash.FastUShortHash(initialDictionary.Slice(i+1,1));
//                dictionary[i+2] = ByteArrayHash.FastUShortHash(initialDictionary.Slice(i+2,1));
//                dictionary[i+3] = ByteArrayHash.FastUShortHash(initialDictionary.Slice(i+3,1));
//            }

//            var compressedData = new List<ushort>(compressionListlength);
//            var sb = new StringBuilder();
//            fixed (byte* dataPtr = data)
//            {
//                byte* ptr = dataPtr;
//                byte* endPtr = dataPtr + length;

//                while (ptr < endPtr)
//                {
//                    byte b = *ptr;
//                    var nextString = sb.Append((char)b).ToString();
//                    if (dictionary.TryGetValue(nextString, out var code))
//                    {
//                        sb.Clear();
//                        sb.Append(nextString[0]);
//                    }
//                    else
//                    {
//                        dictionary.Add(nextString, dictionaryCount);
//                        compressedData.Add(dictionary[sb.ToString()]);
//                        sb.Clear();
//                        sb.Append((char)b);
//                    }
//                    ptr++;
//                }
//            }

//            if (sb.Length > 0)
//            {
//                compressedData.Add(dictionary[sb.ToString()]);
//            }

//            return compressedData;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
//        public static byte[] Decompress(byte[] data)
//        {
//            Dictionary<int, string> dictionary = new Dictionary<int, string>();
//            for (int i = 0; i < 256; i++)
//            {
//                dictionary.Add(i, ((char)i).ToString());
//            }

//            List<int> compressedData = new List<int>();
//            for (int i = 0; i < data.Length; i += 2)
//            {
//                int value = (data[i] << 8) | data[i + 1];
//                compressedData.Add(value);
//            }

//            List<byte> decompressedBytes = new List<byte>();
//            string currentString = dictionary[compressedData[0]];
//            decompressedBytes.AddRange(currentString.ToByteArray());

//            for (int i = 1; i < compressedData.Count; i++)
//            {
//                string nextString = string.Empty;
//                if (dictionary.ContainsKey(compressedData[i]))
//                {
//                    nextString = dictionary[compressedData[i]];
//                }
//                else if (compressedData[i] == dictionary.Count)
//                {
//                    nextString = currentString + currentString[0];
//                }

//                decompressedBytes.AddRange(nextString.ToByteArray());

//                dictionary.Add(dictionary.Count, currentString + nextString[0]);
//                currentString = nextString;
//            }

//            return decompressedBytes.ToArray();
//        }

//        private static byte[] ToByteArray(this string str)
//        {
//            return System.Text.Encoding.UTF8.GetBytes(str);
//        }
//    }
//}
