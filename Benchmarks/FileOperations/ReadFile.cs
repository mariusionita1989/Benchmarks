using System.Runtime.CompilerServices;

namespace Benchmarks.FileOperations
{
    public static class ReadFile
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void ReadBinaryFile(string filePath, int bufferSize = 4096) 
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                { 
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
