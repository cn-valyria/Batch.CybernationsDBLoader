using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repository.Infrastructure
{
    public static class CommonExtensions
    {
        public static bool TryFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> filter, out T result)
        {
            result = default;

            foreach (var item in enumerable)
                if (filter(item))
                {
                    result = item;
                    return true;
                }

            return false;
        }

        public static int? ToNullableInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            // At this point I'm fine with whatever default the out param gives me
            int.TryParse(value, out var parsedValue);
            return parsedValue;
        }

        public static IEnumerable<T> MergeWith<T>(this IEnumerable<T> currentList, IEnumerable<T> otherList)
        {
            var result = currentList.ToList();
            result.AddRange(otherList);
            return result;
        }

        public static bool IsUnauthorizedStatusCode(this HttpResponseMessage responseMessage) => responseMessage.StatusCode == HttpStatusCode.Unauthorized;

        public static bool IsNotFoundStatusCode(this HttpResponseMessage responseMessage) => responseMessage.StatusCode == HttpStatusCode.NotFound;

        public static async Task<Stream> CopyAsync(this Stream stream)
        {
            var newStream = new MemoryStream();
            await stream.CopyToAsync(newStream);
            stream.Position = 0;
            newStream.Position = 0;

            return newStream;
        }
    }
}
