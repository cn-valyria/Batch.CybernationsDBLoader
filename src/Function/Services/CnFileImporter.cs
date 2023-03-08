using AutoMapper;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.DataObjects;
using Repository.Parsers;
using Repository.Parsers.DataObjects;

namespace Function.Services;

public class CnFileImporter : IFileImporter
{
    private readonly IDataParser _dataParser;
    private readonly ICnDbRepository _cnDbRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CnFileImporter> _logger;

    public CnFileImporter(IDataParser dataParser, ICnDbRepository cnDbRepository, IMapper mapper, ILogger<CnFileImporter> logger) =>
        (_dataParser, _cnDbRepository, _mapper, _logger) = (dataParser, cnDbRepository, mapper, logger);

    public async Task ImportAidAsync(Stream fileBlob, string fileName)
    {
        // Convert CSV to data
        var allAidData = new List<CnAid>();
        try
        {
            await foreach (var fileRecord in _dataParser.Parse<CnAid>(fileBlob))
                allAidData.Add(fileRecord);

            _logger.LogInformation($"File successfully parsed. {allAidData.Count} records found.");
        }
        catch (Exception e)
        {
            _logger.LogError($"Critical error encountered while parsing blob. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
            return;
        }

        // Upload data to DB
        try
        {
            await _cnDbRepository.UpsertAid(allAidData.Select(_mapper.Map<Aid>).ToList(), $"{fileName}.txt");

            _logger.LogInformation($"Data uploaded successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError($"Critical error encountered while upserting aid data to the DB. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
            return;
        }
    }
}