using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Repository.Parsers
{
    public class CnFileParser : IDataParser
    {
        /// <summary>
        /// Generic wrapper for CSV reader that works for CN files
        /// </summary>
        public async IAsyncEnumerable<T> Parse<T>(Stream dataStream)
        {
            using (var reader = new StreamReader(dataStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "|" }))
            {
                await foreach (var nationRecord in csv.GetRecordsAsync<T>())
                    yield return nationRecord;
            }
        }
    }
}
