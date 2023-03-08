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

    public async Task ImportNationsAsync(Stream fileBlob, string fileName)
    {
        var parsedData = await RunDataParser<CnNation>(fileBlob);
        var dataForUpsert = parsedData.Select(_mapper.Map<Nation>).ToList();
        await UpsertDataToDb(dataForUpsert, fileName, _cnDbRepository.UpsertNations);
    }

    public async Task ImportAlliancesAsync(Stream fileBlob, string fileName)
    {
        var parsedData = await RunDataParser<CnAlliance>(fileBlob);
        var dataForUpsert = parsedData.Select(_mapper.Map<Alliance>).ToList();
        await UpsertDataToDb(dataForUpsert, fileName, _cnDbRepository.UpsertAlliances);
    }

    public async Task ImportAidAsync(Stream fileBlob, string fileName)
    {
        var parsedData = await RunDataParser<CnAid>(fileBlob);
        var dataForUpsert = parsedData.Select(_mapper.Map<Aid>).ToList();
        await UpsertDataToDb(dataForUpsert, fileName, _cnDbRepository.UpsertAid);
    }

    public async Task ImportWarsAsync(Stream fileBlob, string fileName)
    {
        var parsedData = await RunDataParser<CnWar>(fileBlob);
        var dataForUpsert = parsedData.Select(_mapper.Map<War>).ToList();
        await UpsertDataToDb(dataForUpsert, fileName, _cnDbRepository.UpsertWar);
    }

    private async Task<List<T>> RunDataParser<T>(Stream fileBlob)
    {
        var allData = new List<T>();
        try
        {
            await foreach (var fileRecord in _dataParser.Parse<T>(fileBlob))
                allData.Add(fileRecord);

            _logger.LogInformation($"File successfully parsed. {allData.Count} records found.");
        }
        catch (Exception e)
        {
            _logger.LogError($"Critical error encountered while parsing blob. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
            throw;
        }

        return allData;
    }

    private async Task UpsertDataToDb<T>(List<T> allData, string fileName, Func<IReadOnlyCollection<T>, string, Task> upsertFunc)
    {
        try
        {
            await upsertFunc(allData, $"{fileName}.txt");
            _logger.LogInformation($"Data uploaded successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError($"Critical error encountered while upserting aid data to the DB. \n Message: {e.Message}\n StackTrace: {e.StackTrace}");
            throw;
        }
    }
}