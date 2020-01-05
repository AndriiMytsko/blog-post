using Microsoft.AspNetCore.Http;
using System.IO;

namespace BlogPost.Web.Infrastructure.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
