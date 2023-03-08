using Function.Services;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Repository.Grabbers;
using Repository.Infrastructure;

namespace Function;

public class TimerEntryPoint
{
    private readonly IDataGrabber _cnFileGrabber;
    private readonly IFileImporter _cnFileImporter;
    private readonly IBlobManager _blobManager;

    public TimerEntryPoint(IDataGrabber cnFileGrabber, IFileImporter cnFileImporter, IBlobManager blobManager) 
        => (_cnFileGrabber, _cnFileImporter, _blobManager) = (cnFileGrabber, cnFileImporter, blobManager);

    [FunctionName(nameof(CnAlliancesFileGrabber)), Disable]
    public async Task CnAlliancesFileGrabber(
        [TimerTrigger("0 0 1,13 * * *")] TimerInfo myTimer,
        [Blob("alliances", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(CnAlliancesFileGrabber)} function started execution at: {DateTime.Now}");

        await outputContainer.CreateIfNotExistsAsync();

        var cnResponse = await _cnFileGrabber.GetTodaysFileAsync(CnFileType.Alliances, log);

        var cloudBlockBlob = outputContainer.GetBlockBlobReference($"{cnResponse.FileName}.txt");
        await cloudBlockBlob.UploadFromStreamAsync(cnResponse.DataStream);

        log.LogInformation($"{nameof(CnAlliancesFileGrabber)} function completed execution at: {DateTime.Now}");
    }

    [FunctionName(nameof(CnNationsFileGrabber)), Disable]
    public async Task CnNationsFileGrabber(
        [TimerTrigger("0 5 1,13 * * *")] TimerInfo myTimer,
        [Blob("nations", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(CnNationsFileGrabber)} function started execution at: {DateTime.Now}");

        await outputContainer.CreateIfNotExistsAsync();

        var cnResponse = await _cnFileGrabber.GetTodaysFileAsync(CnFileType.Nations, log);

        var cloudBlockBlob = outputContainer.GetBlockBlobReference($"{cnResponse.FileName}.txt");
        await cloudBlockBlob.UploadFromStreamAsync(cnResponse.DataStream);

        log.LogInformation($"{nameof(CnNationsFileGrabber)} function completed execution at: {DateTime.Now}");
    }

    [FunctionName(nameof(CnAidFileGrabber))]
    public async Task CnAidFileGrabber(
        [TimerTrigger("0 10 1,13 * * *", RunOnStartup = true)] TimerInfo myTimer,
        [Blob("aid", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(CnAidFileGrabber)} function started execution at: {DateTime.Now}");

        var (fileName, dataStream) = await _cnFileGrabber.GetTodaysFileAsync(CnFileType.Aid, log);
        log.LogInformation($"{fileName}.txt downloaded from CN.");

        // Upload the file first. If anything goes wrong with the import, we want the file preserved in blob storage
        await _blobManager.UploadFileAsync(outputContainer, fileName, await dataStream.CopyAsync());
        log.LogInformation($"{fileName}.txt uploaded to {outputContainer.Name} Azure blob");

        await _cnFileImporter.ImportAidAsync(await dataStream.CopyAsync(), fileName);
        log.LogInformation($"{fileName}.txt successfully imported to cybernations_db");

        log.LogInformation($"{nameof(CnAidFileGrabber)} function completed execution at: {DateTime.Now}");
    }

    [FunctionName(nameof(CnWarFileGrabber)), Disable]
    public async Task CnWarFileGrabber(
        [TimerTrigger("0 15 1,13 * * *")] TimerInfo myTimer,
        [Blob("war", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(CnWarFileGrabber)} function started execution at: {DateTime.Now}");

        await outputContainer.CreateIfNotExistsAsync();

        var cnResponse = await _cnFileGrabber.GetTodaysFileAsync(CnFileType.War, log);

        var cloudBlockBlob = outputContainer.GetBlockBlobReference($"{cnResponse.FileName}.txt");
        await cloudBlockBlob.UploadFromStreamAsync(cnResponse.DataStream);

        log.LogInformation($"{nameof(CnWarFileGrabber)} function completed execution at: {DateTime.Now}");
    }
}
