using System;
using System.IO;
using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Repository.Parsers;
using Repository;
using Repository.DataObjects;
using Repository.Parsers.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Function
{
    public class BlobEntryPoint
    {
        private readonly IDataParser _dataParser;
        private readonly ICnDbRepository _cnDbRepository;
        private readonly IMapper _mapper;

        public BlobEntryPoint(IDataParser dataParser, ICnDbRepository cnDbRepository, IMapper mapper) =>
            (_dataParser, _cnDbRepository, _mapper) = (dataParser, cnDbRepository, mapper);

        /// <summary>
        /// Blob-triggered job for uploading Nations file to CN DB
        /// </summary>
        [FunctionName(nameof(CnNationsImporter))]
        public async Task CnNationsImporter([BlobTrigger("nations/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"CnNationsImporter trigger function processing blob... \n Name:{name} \n Size: {myBlob.Length} Bytes");

            // Convert CSV to data
            var allNationData = new List<CnNation>();
            try
            {
                await foreach (var fileRecord in _dataParser.Parse<CnNation>(myBlob))
                    allNationData.Add(fileRecord);

                log.LogInformation($"File successfully parsed. {allNationData.Count} records found.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while parsing blob. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            // Upload data to DB
            try
            {
                await _cnDbRepository.UpsertNations(allNationData.Select(_mapper.Map<Nation>).ToList(), name);

                log.LogInformation($"Data uploaded successfully.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while upserting nation data to the DB. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            log.LogInformation($"{nameof(CnNationsImporter)} trigger function completed successfully!");
        }

        /// <summary>
        /// Blob-triggered job for uploading Aid file to CN DB
        /// </summary>
        [FunctionName(nameof(CnAidImporter))]
        public async Task CnAidImporter([BlobTrigger("aid/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"CnAidImporter trigger function processing blob... \n Name:{name} \n Size: {myBlob.Length} Bytes");

            // Convert CSV to data
            var allAidData = new List<CnAid>();
            try
            {
                await foreach (var fileRecord in _dataParser.Parse<CnAid>(myBlob))
                    allAidData.Add(fileRecord);

                log.LogInformation($"File successfully parsed. {allAidData.Count} records found.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while parsing blob. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            // Upload data to DB
            try
            {
                await _cnDbRepository.UpsertAid(allAidData.Select(_mapper.Map<Aid>).ToList(), name);

                log.LogInformation($"Data uploaded successfully.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while upserting aid data to the DB. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            log.LogInformation($"{nameof(CnAidImporter)} trigger function completed successfully!");
        }

        /// <summary>
        /// Blob-triggered job for uploading Aid file to CN DB
        /// </summary>
        [FunctionName(nameof(CnWarImporter))]
        public async Task CnWarImporter([BlobTrigger("war/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"CnAidImporter trigger function processing blob... \n Name:{name} \n Size: {myBlob.Length} Bytes");

            // Convert CSV to data
            var allWarData = new List<CnWar>();
            try
            {
                await foreach (var fileRecord in _dataParser.Parse<CnWar>(myBlob))
                    allWarData.Add(fileRecord);

                log.LogInformation($"File successfully parsed. {allWarData.Count} records found.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while parsing blob. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            // Upload data to DB
            try
            {
                await _cnDbRepository.UpsertWar(allWarData.Select(_mapper.Map<War>).ToList(), name);

                log.LogInformation($"Data uploaded successfully.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while upserting war data to the DB. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            log.LogInformation($"{nameof(CnWarImporter)} trigger function completed successfully!");
        }

        /// <summary>
        /// Blob-triggered job for uploading Aid file to CN DB
        /// </summary>
        [FunctionName(nameof(CnAlliancesImporter))]
        public async Task CnAlliancesImporter([BlobTrigger("alliances/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"CnAlliancesImporter trigger function processing blob... \n Name:{name} \n Size: {myBlob.Length} Bytes");

            // Convert CSV to data
            var allAllianceData = new List<CnAlliance>();
            try
            {
                await foreach (var fileRecord in _dataParser.Parse<CnAlliance>(myBlob))
                    allAllianceData.Add(fileRecord);

                log.LogInformation($"File successfully parsed. {allAllianceData.Count} records found.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while parsing blob. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            // Upload data to DB
            try
            {
                await _cnDbRepository.UpsertAlliances(allAllianceData.Select(_mapper.Map<Alliance>).ToList(), name);

                log.LogInformation($"Data uploaded successfully.");
            }
            catch (Exception e)
            {
                log.LogError($"Critical error encountered while upserting alliance data to the DB. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
                return;
            }

            log.LogInformation($"{nameof(CnAlliancesImporter)} trigger function completed successfully!");
        }
    }
}
