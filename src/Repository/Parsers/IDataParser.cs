using System.Collections.Generic;
using System.IO;

namespace Repository.Parsers
{
    public interface IDataParser
    {
        IAsyncEnumerable<T> Parse<T>(Stream dataStream);
    }
}
