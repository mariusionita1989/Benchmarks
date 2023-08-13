namespace Benchmarks.FileOperations
{
    public static class WriteFile
    {
        public static void WriteBinaryFile(string filePath, int bufferSize = 4096)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.None, bufferSize, FileOptions.SequentialScan))
                {
                    byte[] buffer = new byte[bufferSize];
                    long remainingBytes = new FileInfo(filePath).Length;

                    while (remainingBytes > 0)
                    {
                        int bytesToWrite = (int)Math.Min(remainingBytes, bufferSize);
                        fileStream.Write(buffer, 0, bytesToWrite);
                        remainingBytes -= bytesToWrite;
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
