using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReadDocumentAndGenerateInByteArray.Services
{
    public static class StringFromArrayGenerator<T>
    {
        public static string GenerateStringFromArray(T[] array)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Array.ForEach(array, (item) => { stringBuilder.Append(item); stringBuilder.Append(", "); });
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            return stringBuilder.ToString();
        }
    }
    public static class Helper
    {
        public async static Task<byte[]> GenerateByteArrayFromFileUsingPath(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}
