using System.Collections.Concurrent;
using System.IO.MemoryMappedFiles;
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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void ReadStringFile(string filePath)
        {
            try 
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void ReadMemoryMappedFile(string filePath, int bufferSize = 4096)
        {
            try
            {
                byte[] buffer = new byte[bufferSize];
                long fileSize = new FileInfo(filePath).Length;

                using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, "myMappedFile", fileSize))
                {
                    using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                    {
                        Parallel.ForEach(Partitioner.Create(0, fileSize, bufferSize), range =>
                        {
                            for (long i = range.Item1; i < range.Item2; i += bufferSize)
                            {
                                int bytesToRead = (int)Math.Min(bufferSize, fileSize - i);
                                accessor.ReadArray(i, buffer, 0, bytesToRead);
                            }
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void ParallelReadMemoryMappedFile(string filePath, int bufferSize = 4096)
        {
            try
            {
                byte[] buffer = new byte[bufferSize];
                long fileSize = new FileInfo(filePath).Length;
                using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, "myMappedFile", fileSize))
                {
                    using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                    {
                        Parallel.ForEach(Partitioner.Create(0, fileSize, bufferSize), range =>
                        {
                            for (long i = range.Item1; i < range.Item2; i += bufferSize)
                            {
                                int bytesToRead = (int)Math.Min(bufferSize, fileSize - i);
                                accessor.ReadArray(i, buffer, 0, bytesToRead);
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static async Task AsyncReadMemoryMappedFile(string filePath, int bufferSize = 4096)
        {
            try
            {
                byte[] buffer = new byte[bufferSize];
                long fileSize = new FileInfo(filePath).Length;
                long bytesRead = 0;

                using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, "myMappedFile", fileSize))
                {
                    using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                    {
                        List<Task> tasks = new List<Task>();

                        while (bytesRead < fileSize)
                        {
                            int bytesToRead = (int)Math.Min(bufferSize, fileSize - bytesRead);
                            var bytesToReadCopy = bytesToRead; // Capture the local variable for the lambda

                            tasks.Add(Task.Run(() =>
                            {
                                byte[] chunkBuffer = new byte[bytesToReadCopy];
                                accessor.ReadArray(bytesRead, chunkBuffer, 0, bytesToReadCopy);
                            }));

                            bytesRead += bytesToRead;
                        }
                        await Task.WhenAll(tasks);
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
