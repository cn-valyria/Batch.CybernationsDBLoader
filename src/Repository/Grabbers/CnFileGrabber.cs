using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Repository.Grabbers
{
    public class CnFileGrabber : IDataGrabber
    {
        const string cnUrl = "https://www.cybernations.net";

        public async Task<(string FileName, Stream DataStream)> GetTodaysFileAsync(CnFileType fileType, ILogger logger)
        {
            var dataStream = new MemoryStream();

            logger.LogInformation($"Looking for a {fileType} CN file at UTC {DateTime.UtcNow}");

            var now = GetExpectedFileDay();
            logger.LogInformation($"UTC time converted to {now} CST");

            var fileName = $"{GetCnFileName(fileType)}{now.Month}{now.Day}{now.Year}{GetCnFileExtension(fileType, now)}";

            logger.LogInformation($"File name: {fileName}");

            using (var client = new HttpClient())
            {
                var message = new HttpRequestMessage(HttpMethod.Get, $"{cnUrl}/assets/{fileName}.zip");
                var response = await client.SendAsync(message);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError(await response.Content.ReadAsStringAsync());
                    throw new Exception("CN file could not be accessed.");
                }

                using (var responseData = await response.Content.ReadAsStreamAsync())
                using (var zip = new ZipArchive(responseData, ZipArchiveMode.Read))
                {
                    var usefulFile = zip.Entries[0].Open();
                    await usefulFile.CopyToAsync(dataStream);
                    dataStream.Position = 0;
                }
            }

            return (fileName, dataStream);
        }

        /// <summary>
        /// Factory method to get desired file name by desired file type
        /// </summary>
        private string GetCnFileName(CnFileType fileType) => fileType switch
        {
            CnFileType.Alliances => "CyberNations_SE_Alliance_Stats_",
            CnFileType.Nations => "CyberNations_SE_Nation_Stats_",
            CnFileType.Aid => "CyberNations_SE_Aid_Stats_",
            CnFileType.War => "CyberNations_SE_War_Stats_",
            _ => string.Empty
        };

        /// <summary>
        /// Factory method to get the day that we expect the file to have been uploaded on. 
        /// </summary>
        private DateTime GetExpectedFileDay()
        {
            // Make sure we're using the CST representation of "now"
            var cstTimeZone = TZConvert.GetTimeZoneInfo("Central Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstTimeZone);

            // If "now" is actually before 6am, then we want yesterday's file instead of today
            return now.Hour < 6 ? now.Date.AddDays(-1) : now.Date;
        }

        /// <summary>
        /// Factory method to get the "timestamp" extension based on the current time (i.e. hour)
        /// </summary>
        private string GetCnFileExtension(CnFileType fileType, DateTime now)
        {
            var firstPart = fileType switch
            {
                CnFileType.Alliances => "515",
                CnFileType.Nations => "510",
                CnFileType.Aid => "520",
                CnFileType.War => "525",
                _ => string.Empty
            };

            var lastPart = now switch
            {
                // "morning" file (i.e. after 6am CST but before 6pm CST)
                _ when now.Hour > 6 && now.Hour < 18 => "001",

                // if it's not the "morning" file then it has to be the "evening" file
                _ => "002"
            };

            return firstPart + lastPart;
        }
    }

    public enum CnFileType
    {
        Alliances,
        Nations,
        Aid,
        War
    }
}
